namespace LoopUp.BitbucketCoveralls.Bitbucket
{
    public class PullRequest
    {
        public int Id { get; set; }

        public Ref FromRef { get; set; }

        public Ref ToRef { get; set; }
    }
}
