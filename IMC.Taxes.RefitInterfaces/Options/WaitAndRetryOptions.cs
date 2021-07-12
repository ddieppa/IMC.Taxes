namespace IMC.Taxes.RefitInterfaces.Options
{
    public class WaitAndRetryOptions
    {
        public const string WaitAndRetry = "WaitAndRetry";
        public int Retry { get; set; }
        public int Wait { get; set; }
        public int Timeout { get; set; }
    }
}