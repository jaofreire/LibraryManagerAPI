using Microsoft.Extensions.Configuration;
using Amazon.S3;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Http;
using Amazon.S3.Model;
using Amazon.Runtime;
using Amazon;


namespace Data.Services.APIs
{
    public class AWSS3
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;
        private readonly string _bucketName;
        private readonly string _accessKey;
        private readonly string _secretKey;

        public AWSS3(IConfiguration configuration)
        {
            _configuration = configuration;
            _bucketName = _configuration["S3Service:BucketName"]!;
            _accessKey = _configuration["AWS:AccessKey"]!;
            _secretKey = _configuration["AWS:SecretKey"]!;
            _s3Client = CreateS3Client();
        }

        private IAmazonS3 CreateS3Client()
        {
            var awsCredentials = new BasicAWSCredentials(_accessKey, _secretKey);

            var clientConfig = new AmazonS3Config()
            {
                RegionEndpoint = RegionEndpoint.USEast2
            };

            return new AmazonS3Client(awsCredentials, clientConfig);
        }

        public async Task<string> PutNewS3ImageObject(IFormFile file,string bookName)
        {
            var bucketExiste = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, _bucketName);

            if (bucketExiste == false)
                throw new ArgumentNullException("_bucketName","The bucket is not found");

            using(var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                var putRequest = new PutObjectRequest()
                {
                    BucketName = _bucketName,
                    Key = $"{bookName}.png",
                    InputStream = stream
                };

                await _s3Client.PutObjectAsync(putRequest);
            }

            return $"https://{_bucketName}.s3.us-east-2.amazonaws.com/{bookName}.png";

        }

        public async Task<bool> DeleteS3ImageObject(string imageName)
        {
            var bucketExiste = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, _bucketName);

            if (bucketExiste == false)
                throw new ArgumentNullException("_bucketName", "The bucket is not found");

            var getRequest = new GetObjectRequest()
            {
                BucketName = _bucketName,
                Key = $"{imageName}.png"
            };
            var imageObject = await _s3Client.GetObjectAsync(getRequest);

            var deleteRequest = new DeleteObjectRequest()
            {
                BucketName = _bucketName,
                Key = imageName
            };

            await _s3Client.DeleteObjectAsync(deleteRequest);

            return true;
        }
    }
}
