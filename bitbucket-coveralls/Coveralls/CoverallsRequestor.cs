using LoopUp.BitbucketCoveralls.Bitbucket;
using LoopUp.BitbucketCoveralls.Coveralls;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LoopUp.BitbucketCoveralls
{
    public class CoverallsRequestor
    {
        public async Task GetCoverage(PullRequest pullRequest)
        {
            var fromBuild = await this.GetBuildCoverage(pullRequest.FromRef.LatestCommit);
            var toBuild = await this.GetBuildCoverage(pullRequest.ToRef.LatestCommit);
            var fromFiles = await this.GetFileCoverage(pullRequest.FromRef.LatestCommit);
            var toFiles = await this.GetFileCoverage(pullRequest.ToRef.LatestCommit);
        }

        private async Task<Build> GetBuildCoverage(string commit) =>
            await this.SendRequestAsync<Build>($"https://coveralls.engr.loopup.com/builds/3.json");

        private async Task<SourceFileList> GetFileCoverage(string commit)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"https://coveralls.engr.loopup.com/builds/{commit}/source_files.json");

                var response = await client.SendAsync(request);
                return JsonConvert.DeserializeObject<SourceFileList>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<T> SendRequestAsync<T>(string url)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{url}");
                var response = await client.SendAsync(request);
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
