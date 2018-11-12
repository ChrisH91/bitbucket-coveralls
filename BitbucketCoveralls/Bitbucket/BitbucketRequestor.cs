using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BitbucketCoveralls.Bitbucket
{
    public class BitbucketRequestor
    {
        private readonly int LIMIT = 25;

        public async Task<PullRequestList> GetOpenPullRequests() =>
            await this.SendRequestAsync<PullRequestList>("https://stash.ring2.com/rest/api/latest/projects/API/repos/api/pull-requests");

        //public async Task<PullRequestActivityList> GetLatestComments()
        //{
        
        //}

        public async Task<T> SendRequestAsync<T>(string url)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", "Y2hyaXMuaG93YXJkOlkzT2xkM1Bhc3N3b3JkNSU=");

                var response = await client.SendAsync(request);
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
