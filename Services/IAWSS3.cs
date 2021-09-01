using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace dowd.Services
{
    public interface IAWSS3
    {
        Task<List<string>> Write(IEnumerable<IFormFile> files);
    }
}
