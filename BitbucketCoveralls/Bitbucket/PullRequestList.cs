using System;
using System.Collections.Generic;
using System.Text;

namespace BitbucketCoveralls.Bitbucket
{
    public class PullRequestList
    {
        public int Size { get; set; }

        public int Limit { get; set; }

        public bool IsLastPage { get; set; }

        public IEnumerable<PullRequest> Values { get; set; }

        public int Start { get; set; }
    }
}
