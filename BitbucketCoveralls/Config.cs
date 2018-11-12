using System.Collections.Generic;

namespace BitbucketCoveralls
{
    public class Config
    {
        public string BitubcketUrlBase { get; set; }

        public string BitbucketUsername { get; set; }

        public string BitbucketPassword { get; set; }

        public string CoverallsUrlBase { get; set; }

        public IEnumerable<Repository> Repositories { get; set; }
    }
}
