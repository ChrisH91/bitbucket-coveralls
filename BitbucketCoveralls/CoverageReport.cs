namespace BitbucketCoveralls
{
    public class CoverageReport
    {
        public float Coverage { get; set; }

        public float Diff { get; set; }

        public FileCoverageReport FileCoverage { get; set; }
    }
}
