using System.Collections.Generic;
using BitbucketCoveralls.Coveralls;

namespace BitbucketCoveralls
{
    public class FileCoverageReport
    {
        public IEnumerable<SourceFile> Added { get; set; }

        public IEnumerable<SourceFile> Removed { get; set; }

        public IEnumerable<SourceFile> Increased { get; set; }

        public IEnumerable<SourceFile> Decreased { get; set; }
    }
}
