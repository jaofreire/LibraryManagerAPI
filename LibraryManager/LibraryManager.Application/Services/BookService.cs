using LibraryManager.Data.Helper.AWS;
using LibraryManager.Application.DTOs.Book.Input;
using LibraryManager.Application.DTOs.Book.Output;
using LibraryManager.Application.Responses;
using LibraryManager.Application.Services.Interfaces;
using LibraryManager.Domain.Interfaces;
using LibraryManager.Application.Utils.Mappings;
using LibraryManager.Data.Helper;
using LibraryManager.Data;
using LibraryManager.Application.Enums;
using LibraryManager.Data.Utils;
using LibraryManager.Application.Helper;


namespace LibraryManager.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly AWSS3 _s3Service;
        private readonly CacheHandler _cacheHandler;

        public BookService(IBookRepository bookRepository, AWSS3 s3Service, CacheHandler cacheHandler)
        {
            _bookRepository = bookRepository;
            _s3Service = s3Service;
            _cacheHandler = cacheHandler;
        }

        public async Task<APIResponse<CreateBookDTO>> RegisterBook(CreateBookDTO createBookDTO)
        {
            string? photoUrl = string.Empty;

            if (createBookDTO.FileForm is not null)
                photoUrl = await _s3Service.PutNewS3ImageObject(createBookDTO.FileForm, createBookDTO.Title);

            var model = createBookDTO.ToModel();
            model.PhotoUrl = photoUrl;

            await _bookRepository.Register(model);

            await _cacheHandler.RemoveCache(DataConfigurations.SearchBooksCacheFactor);

            return new APIResponse<CreateBookDTO>(
                operationType: EOperationType.Create.ToString(),
                true,
                200,
                message: "Book registered successfully!",
                dataResponse: createBookDTO,
                dataResponseList: null);
        }

        public async Task<APIResponse<CreateBookDTO>> RegisterBooks(List<CreateBookDTO> createBookDTOList)
        {
            var models = createBookDTOList.ToModelList();

            await _bookRepository.RegisterMany(models);

            await _cacheHandler.RemoveCache(DataConfigurations.SearchBooksCacheFactor);

            return new APIResponse<CreateBookDTO>(
                 operationType: EOperationType.CreateMany.ToString(),
                 true,
                 200,
                 message: "Book registered successfully!",
                 dataResponse: null,
                 dataResponseList: createBookDTOList!);
        }

        public async Task<APIResponse<ViewBookDTO>> GetAllBooks()
        {

            var viewBooksCache = await _cacheHandler.GetCacheObject<List<ViewBookDTO>>(DataConfigurations.SearchBooksCacheFactor);

            if (viewBooksCache != default) return new APIResponse<ViewBookDTO>(
                 operationType: EOperationType.Get.ToString(),
                 true,
                 200,
                 message: "Listing all books registered successfully!",
                 dataResponse: null,
                 dataResponseList: viewBooksCache!);


            var models = await _bookRepository.GetAll();

            var viewDTOs = models.ToViewDTOList();

            await _cacheHandler.SetCacheObject<List<ViewBookDTO>>(DataConfigurations.SearchBooksCacheFactor, viewDTOs);

            return new APIResponse<ViewBookDTO>(
                 operationType: EOperationType.Get.ToString(),
                 true,
                 200,
                 message: "Listing all books registered successfully!",
                 dataResponse: null,
                 dataResponseList: viewDTOs!
                 );

        }

        public async Task<APIResponse<ViewBookDTO>> GetBookById(long id)
        {
            var model = await _bookRepository.GetByIdAsNoTracking(id);

            if (model is null) return new APIResponse<ViewBookDTO>(
                 operationType: EOperationType.GetById.ToString(),
                 false,
                 404,
                 message: "The book is not found!"
                 );

            var DTO = model.ToViewDTO();

            return new APIResponse<ViewBookDTO>(
                 operationType: EOperationType.GetById.ToString(),
                 true,
                 200,
                 message: "Listing book with specify id successfully!",
                 dataResponse: DTO,
                 dataResponseList: null);
        }

        public async Task<APIResponse<ViewBookDTO>> GetBookByName(string name)
        {
            var models = await _bookRepository.GetByTerm(x =>
            DatabaseFunctions.FuzzySearch(x.Title) == DatabaseFunctions.FuzzySearch(name));

            var DTOs = models.ToViewDTOList();

            return new APIResponse<ViewBookDTO>(
                 operationType: EOperationType.GetByName.ToString(),
                 true,
                 200,
                 message: "Listing book with specify name successfully!",
                 dataResponse: null,
                 dataResponseList: DTOs!
                 );
        }

        public async Task<APIResponse<ViewBookDTO>> GetBookByAuthor(string authorName)
        {
            var models = await _bookRepository.GetByTermAsNoTracking(x => x.Author.Name == authorName);

            var DTOs = models.ToViewDTOList();

            return new APIResponse<ViewBookDTO>(
                 operationType: EOperationType.GetByAuthor.ToString(),
                 true,
                 200,
                 message: "Listing book with specify author successfully!",
                 dataResponse: null,
                 dataResponseList: DTOs!
                 );
        }

        public async Task<APIResponse<ViewBookDTO>> GetBooksByAuthors(List<string> authorName)
        {
            var models = await _bookRepository.GetByTermAsNoTracking(x => authorName.Contains(x.Author.Name));

            var DTOs = models.ToViewDTOList();

            return new APIResponse<ViewBookDTO>(
                operationType: EOperationType.GetByAuthors.ToString(),
                true,
                200,
                message: "Listing book with specify authors successfully!",
                dataResponse: null,
                dataResponseList: DTOs!
                );
        }

        public async Task<APIResponse<ViewBookDTO>> GetBookByCategory(string category)
        {
            var models = await _bookRepository.GetByTermAsNoTracking(x => x.Category == category);

            var DTOs = models.ToViewDTOList();

            return new APIResponse<ViewBookDTO>(
                 operationType: EOperationType.GetByCategory.ToString(),
                 true,
                 200,
                 message: "Listing book with specify category successfully!",
                 dataResponse: null,
                 dataResponseList: DTOs!
                 );
        }

        public async Task<APIResponse<ViewBookDTO>> GetBooksByCategories(List<string> categorys)
        {
            var models = await _bookRepository.GetByTermAsNoTracking(x => categorys.Contains(x.Category));

            var DTOs = models.ToViewDTOList();

            return new APIResponse<ViewBookDTO>(
                operationType: EOperationType.GetByCategories.ToString(),
                true,
                200,
                message: "Listing book with specify categories successfully!",
                dataResponse: null,
                dataResponseList: DTOs!
                );
        }

        public async Task<APIResponse<UpdateBookDTO>> UpdateBook(UpdateBookDTO updateBookDTO)
        {
            var model = await _bookRepository.GetById(updateBookDTO.Id);

            if (model is null) return new APIResponse<UpdateBookDTO>(
                 operationType: EOperationType.GetById.ToString(),
                 false,
                 404,
                 message: "The book is not found!"
                 );

            var updatededDTO = Validations.UpdateValidation(updateBookDTO, model, out bool isNewFileForm);

            var updatedModel = updatededDTO.ToModel();

            if (isNewFileForm is true)
            {
                var photoUrl = await _s3Service.PutNewS3ImageObject(updateBookDTO.FormFile, updateBookDTO.Title);
                updatedModel = updatededDTO.ToModelWithPhotoUrl(photoUrl);
            }

            await _bookRepository.Update(updatedModel);

            return new APIResponse<UpdateBookDTO>(
                 operationType: EOperationType.Update.ToString(),
                 true,
                 200,
                 message: "Book updated successfully!",
                 dataResponse: updateBookDTO,
                 dataResponseList: null
                 );

        }

        public async Task<APIResponse<ViewBookDTO>> DeleteBook(long id)
        {
            var model = await _bookRepository.GetById(id);

            if (model is null) return new APIResponse<ViewBookDTO>(
                 operationType: EOperationType.GetById.ToString(),
                 false,
                 404,
                 message: "The book is not found!"
                 );

            if (!string.IsNullOrEmpty(model.PhotoUrl))
                await _s3Service.DeleteS3ImageObject(model.Title);

            await _bookRepository.Delete(model);

            await _cacheHandler.RemoveCache(DataConfigurations.SearchBooksCacheFactor);

            return new APIResponse<ViewBookDTO>(
                operationType: EOperationType.Delete.ToString(),
                true,
                200,
                message: "Book removed successfully!"
                );

        }
    }
}
