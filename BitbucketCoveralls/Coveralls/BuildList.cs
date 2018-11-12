using System.Collections.Generic;

namespace BitbucketCoveralls.Coveralls
{
    public class BuildList {
        public int Page { get; set; }

        public int Pages { get; set; }

        public int Total { get; set; }

        public IEnumerable<Build> Builds { get; set; }
    }
}
