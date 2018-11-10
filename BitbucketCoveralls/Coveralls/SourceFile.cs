using Newtonsoft.Json;

namespace LoopUp.BitbucketCoveralls.Coveralls
{
    public class SourceFile
    {
        public string Name { get; set; }

        [JsonProperty("relevant_line_count")]
        public int RelevantLineCount { get; set; }

        [JsonProperty("covered_line_count")]
        public int CoveredLineCount { get; set; }

        [JsonProperty("missed_lint_count")]
        public int MissedLineCount { get; set; }

        [JsonProperty("covered_percent")]
        public float CoveredPercent { get; set; }
    }
}
