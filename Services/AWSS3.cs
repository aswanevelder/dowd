using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace dowd.Services
{
    public class AWSS3 : IAWSS3
    {
        private readonly string _awsAccessKey;
        private readonly string _awsSecretKey;
        private readonly string _awsS3Bucket;
        private readonly string _awsRegion;

        public AWSS3()
        {
            _awsAccessKey = Environment.GetEnvironmentVariable("AWS_ACCESSKEY");
            _awsSecretKey = Environment.GetEnvironmentVariable("AWS_SECRETKEY");
            _awsS3Bucket = Environment.GetEnvironmentVariable("AWS_S3_BUCKET");
            _awsRegion = Environment.GetEnvironmentVariable("AWS_S3_REGION");
        }

        public async Task<List<string>> Write(IEnumerable<IFormFile> files)
        {
            List<string> fileNames = new List<string>();
            using (var _client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, new AmazonS3Config
            {
                RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(_awsRegion)
            }))
            {
                foreach (var file in files)
                {
                    if (file != null && !String.IsNullOrEmpty(file.FileName))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var key = $"{Guid.NewGuid()}-{file.FileName.Replace(" ", "-").ToLower()}";
                            var request = new PutObjectRequest
                            {
                                BucketName = _awsS3Bucket,
                                Key = $"{key}",
                                ContentType = file.ContentType
                            };
                            request.InputStream = ms;
                            var response = await _client.PutObjectAsync(request);
                            if (response.HttpStatusCode == System.Net.HttpStatusCode.Accepted
                            || response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                            {
                                fileNames.Add($"https://s3.{_awsRegion}.amazonaws.com/{_awsS3Bucket}/{key}");
                            }
                        }
                    }
                }
            }
            return fileNames;
        }
    }
}
