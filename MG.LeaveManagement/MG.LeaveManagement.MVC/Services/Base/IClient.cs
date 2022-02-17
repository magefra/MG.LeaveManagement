using System.Net.Http;

namespace MG.LeaveManagement.MVC.Services
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }

    }
}
