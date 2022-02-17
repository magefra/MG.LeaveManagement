using System.Net.Http;

namespace MG.LeaveManagement.MVC.Services
{
    public partial  class Client : IClient
    {
        public HttpClient HttpClient
        {
            get
            {
                return _httpClient;
            }
        }
    }
}
