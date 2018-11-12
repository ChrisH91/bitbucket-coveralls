using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BitbucketCoveralls.Bitbucket;
using BitbucketCoveralls.Coveralls;
using Newtonsoft.Json;
using Serilog;

namespace BitbucketCoveralls
{
    class Program
    {
        private readonly BitbucketRequestor bitbucket = new BitbucketRequestor();
        private readonly CoverallsRequestor coveralls = new CoverallsRequestor();

        private readonly Config config;

        private readonly int PollerDelay = 60000;

        public static void Main(string[] args)
        {
            var config = JsonConvert.DeserializeObject<Config>(
                File.ReadAllText("config.json"));
            var program = new Program(config);
            program.Run().Wait();
            program.ReportCoverage().Wait();
        }

        public ILogger Logger { get; set; }

        public Program(Config config)
        {
            this.config = config;
            this.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        public async Task Run()
        {

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
                    var coverage = await coveralls.GetCoverage(pr);
                }
                catch (Exception) {}
            }
        }
    }
}
