using BitbucketCoveralls.Bitbucket;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BitbucketCoveralls.Coveralls
{
  public class CoverallsRequestor
    {
        public async Task<CoverageReport> GetCoverage(PullRequest pullRequest)
        {
            var builds = await this.GetBuilds();
            var fromFiles = await this.GetFileCoverage(pullRequest.FromRef.LatestCommit);
            var toFiles = await this.GetFileCoverage(pullRequest.ToRef.LatestCommit);

            var buildCoverage = builds.FirstOrDefault(build => build.CommitSha == pullRequest.FromRef.LatestCommit)?.CoveredPercent;
            var prevCoverage = builds.FirstOrDefault(build => build.CommitSha == pullRequest.ToRef.LatestCommit)?.CoveredPercent;

            return new CoverageReport()
            {
                Coverage = (float)buildCoverage,
                Diff = (float)buildCoverage - (float)prevCoverage,
                FileCoverage = this.CompareFiles(fromFiles, toFiles)
            };
        }

        public FileCoverageReport CompareFiles(SourceFileList from, SourceFileList to)
        {
            var fromFileMap = from.SourceFiles.ToDictionary(file => file.Name, file => file);
            var toFileMap = to.SourceFiles.ToDictionary(file => file.Name, file => file);

            var coverageIncreased = new List<SourceFile>();
            var coverageDecreased = new List<SourceFile>();
            var added = new List<SourceFile>();
            var removed = new List<SourceFile>();

            foreach (var fromFile in fromFileMap)
            {
                if (!toFileMap.ContainsKey(fromFile.Key)) {
                    added.Add(fromFile.Value);
                    continue;
                }

                var toFile = toFileMap[fromFile.Key];
                toFileMap.Remove(fromFile.Key);

                if (fromFile.Value.CoveredPercent > toFile.CoveredPercent)
                {
                    coverageIncreased.Add(fromFile.Value);
                }
                else if (fromFile.Value.CoveredPercent < toFile.CoveredPercent)
                {
                    coverageDecreased.Add(fromFile.Value);
                }
            }

            removed.AddRange(toFileMap.Select(kv => kv.Value));

            return new FileCoverageReport()
            {
                Added = added,
                Removed = removed,
                Increased = coverageIncreased,
                Decreased = coverageDecreased
            };
        }

        public async Task<IEnumerable<Build>> GetBuilds()
        {
            var nextPage = 1;
            var builds = new List<Build>();

            while (true)
            {
                var results = await this.SendRequestAsync<BuildList>(
                    $"https://coveralls.engr.loopup.com/stash/API/api.json?page={nextPage}&repo_token=YXNIP5DtgabSKm0FLEgL62uKmq212wtDx");

                if (results.Builds.Count() == 0)
                {
                    break;
                }

                builds.AddRange(results.Builds);
                nextPage += 1;
            }

            return builds;
        }

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
