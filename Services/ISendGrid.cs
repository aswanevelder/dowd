using System.Collections.Generic;
using System.Threading.Tasks;
using dowd.Models;
using SendGrid;

namespace dowd.Services
{
    public interface ISendGridService
    {
        Task<Response> SendQuote(Quote model, List<string> files);
        Task<Response> SendContact(Contact model);
    }
}