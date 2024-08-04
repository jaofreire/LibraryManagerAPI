using LibraryManager.Application.DTOs.Author.Input;
using LibraryManager.Application.DTOs.Author.Output;
using LibraryManager.Application.Responses;
using LibraryManager.Application.Services.Interfaces;
using LibraryManager.Domain.Interfaces;
using LibraryManager.Application.Utils.Mappings;
using LibraryManager.Application.Enums;
using LibraryManager.Application.Helper;

namespace LibraryManager.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<APIResponse<CreateAuthorDTO>> RegisterAuthor(CreateAuthorDTO createAuthorDTO)
        {
            var model = createAuthorDTO.ToModel();
            await _authorRepository.Register(model);
  
            return new APIResponse<CreateAuthorDTO>(
                operationType: EOperationType.Create.ToString(),
                isSuccessfully: true,
                codeReponse: 200,
                message: "Author registered successfully!",
                dataResponse: createAuthorDTO,
                dataResponseList: null
            );
        }

        public async Task<APIResponse<CreateAuthorDTO>> RegisterAuthors(List<CreateAuthorDTO> DTOs)
        {
            var models = DTOs.ToModelList();
            await _authorRepository.RegisterMany(models);

            return new APIResponse<CreateAuthorDTO>(
               operationType: EOperationType.CreateMany.ToString(),
               isSuccessfully: true,
               codeReponse: 200,
               message: "Authors registered successfully!",
               dataResponse: null,
               dataResponseList: DTOs!
           );
        }

        public async Task<APIResponse<ViewAuthorDTO>> GetAllAuthors()
        {
            var models = await _authorRepository.GetAll();

            var DTOs = models.ToViewDTOList();

            return new APIResponse<ViewAuthorDTO>(
                operationType: EOperationType.Get.ToString(),
                true,
                200,
                message: "Listing all authors registered successfully!",
                dataResponse: null,
                dataResponseList: DTOs!
                );
        }

        public async Task<APIResponse<ViewAuthorDTO>> GetAuthorById(long id)
        {
            var model = await _authorRepository.GetByIdAsNoTracking(id);

            if (model is null) return new APIResponse<ViewAuthorDTO>(
                 operationType: EOperationType.GetById.ToString(),
                 false,
                 404,
                 message: "The author is not found"
                 );

            var DTO = model.ToViewDTO();

            return new APIResponse<ViewAuthorDTO>(
                 operationType: EOperationType.GetById.ToString(),
                 true,
                 200,
                 message: "Listing authors with specify id successfully!",
                 dataResponse: DTO,
                 dataResponseList: null
                 );
        }

        public async Task<APIResponse<UpdateAuthorDTO>> UpdateAuthor(UpdateAuthorDTO DTO)
        {
            var model = await _authorRepository.GetById(DTO.Id);

            if (model is null) return new APIResponse<UpdateAuthorDTO>(
                 operationType: EOperationType.GetById.ToString(),
                 false,
                 404,
                 message: "The author is not found"
                 );

            var updatedDTO = Validations.UpdateValidation(DTO, model);
            var updatedModel = updatedDTO.ToModel();

            await _authorRepository.Update(updatedModel);

            return new APIResponse<UpdateAuthorDTO>(
                 operationType: EOperationType.Update.ToString(),
                 true,
                 200,
                 message: "Author updated successfully!",
                 dataResponse: updatedDTO,
                 dataResponseList: null
                 );
        }

        public async Task<APIResponse<ViewAuthorDTO>> DeleteAuthor(long id)
        {
            var model = await _authorRepository.GetById(id);

            if (model is null) return new APIResponse<ViewAuthorDTO>(
                 operationType: EOperationType.GetById.ToString(),
                 false,
                 404,
                 message: "The author is not found"
                 );

            await _authorRepository.Delete(model);

            return new APIResponse<ViewAuthorDTO>(
                  operationType: EOperationType.Delete.ToString(),
                  true,
                  200,
                  message: "Author removed successfully!"
                  );
        }
    }
}
