using FluentValidation;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HcaptchaFluentValidation
{
    public static class HcaptchaTokenValidatorExtensions
    {
        private const string VERIFY_URL = "https://hcaptcha.com/siteverify";
        public static IRuleBuilderOptions<T, string> HcaptchaTokenValidator<T>(this IRuleBuilder<T, string> ruleBuilder, HttpClient httpClient, string secret= "0x0000000000000000000000000000000000000000", string remoteip = null, string sitekey = null)
        {
            return ruleBuilder.MustAsync(async (token, cancellationToken) =>
            {
                var contents = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("response", token), new KeyValuePair<string, string>("secret", secret) });
                contents.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                var response = await httpClient.PostAsync(VERIFY_URL, contents, cancellationToken);
                if (!response.IsSuccessStatusCode)
                    return false;
                var res = await JsonSerializer.DeserializeAsync<HCaptchaResponse>(await response.Content.ReadAsStreamAsync(), null, cancellationToken);
                return res.Success;
            }).WithMessage("شما فیلد من ربات نیستم را تایید نکرده اید.");
        }
    }
}
