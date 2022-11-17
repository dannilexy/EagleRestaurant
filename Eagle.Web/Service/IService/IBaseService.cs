using Eagle.Web.Models;
using Eagle.Web.Models.DTO;

namespace Eagle.Web.Service.IService
{
    public interface IBaseService : IDisposable
    {
        ResponseDto responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
