using Newtonsoft.Json;

namespace BitbucketCoveralls.Coveralls
{
    public class Build
    {
        [JsonProperty("badge_url")]
        public string BadgeUrl { get; set; }

        [JsonProperty("covered_percent")]
        public float CoveredPercent { get; set; }

        [JsonProperty("commit_sha")]
        public string CommitSha { get; set; }
    }
}
