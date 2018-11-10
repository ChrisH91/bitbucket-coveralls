using Newtonsoft.Json;
using System.Collections.Generic;

namespace LoopUp.BitbucketCoveralls.Coveralls
{
    public class SourceFileList
    {
        public int Total { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }

        [JsonProperty("source_files")]
        private string SourceFilesString { get; set; }

        public IEnumerable<SourceFile> SourceFiles
        {
            get => JsonConvert.DeserializeObject<IEnumerable<SourceFile>>(this.SourceFilesString);
        }
    }
}
