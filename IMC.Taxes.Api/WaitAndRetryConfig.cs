﻿namespace IMC.Taxes.Api
{
    public class WaitAndRetryConfig
    {
        public int Retry { get; set; }
        public int Wait { get; set; }
        public int Timeout { get; set; }
    }
}