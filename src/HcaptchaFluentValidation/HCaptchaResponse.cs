using System;
using System.Text.Json.Serialization;

namespace HcaptchaFluentValidation
{
    public class HCaptchaResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("credit")]
        public bool Credit { get; set; }
        [JsonPropertyName("hostname")]
        public string Hostname { get; set; }
        [JsonPropertyName("challenge_ts")]
        public DateTime Challenge_ts { get; set; }
        [JsonPropertyName("error-codes")]
        public string[] Error_Codes { get; set; }
    }
}
