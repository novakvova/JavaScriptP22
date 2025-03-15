using Newtonsoft.Json;

namespace LohikaBackend.Models
{
    public class RecaptchaResponseDTO
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}
