using System.Net;
using System.Threading.Tasks;

namespace ParkAPIClient
{
    public class ParkApiClient
    {
        private string Host { get; }
        
        public ParkApiClient(string host)
        {
            Host = host;
        }

        public async Task<bool> UpdateCurrentAsync(int id, int current)
        {
            HttpWebRequest request = WebRequest.CreateHttp($"{Host}/api/lots/update/${id}/{current}");
            request.Method = WebRequestMethods.Http.Post;
            HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync();
            
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
