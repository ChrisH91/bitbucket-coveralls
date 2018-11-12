using System.Collections.Generic;

namespace BitbucketCoveralls.Bitbucket
{
    public class PullRequestActivityList
    {
        public int Size { get; set; }

        public int Limit { get; set; }

        public bool IsLastPage { get; set; }

        public IEnumerable<PullRequestActivity> Values { get; set; }
    }
}
