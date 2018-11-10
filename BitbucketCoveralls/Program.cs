using System;
using System.Linq;
using System.Threading.Tasks;
using BitbucketCoveralls.Bitbucket;
using BitbucketCoveralls.Coveralls;

namespace BitbucketCoveralls
{
    class Program
    {
        private readonly BitbucketRequestor bitbucket = new BitbucketRequestor();
        private readonly CoverallsRequestor coveralls = new CoverallsRequestor();

        public static void Main(string[] args)
        {
            var program = new Program();
            program.ReportCoverage().Wait();
        }

        public async Task ReportCoverage()
        {
            var prs = await bitbucket.GetOpenPullRequests();
            SourceFileList first;
            SourceFileList second;

            foreach (var pr in prs.Values)
            {
                try
                {
                    await coveralls.GetCoverage(pr);
                    await coveralls.GetCoverage(pr);
                }
                catch (Exception) {}
            }
        }
    }
}
