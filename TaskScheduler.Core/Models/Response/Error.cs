using Newtonsoft.Json;

namespace TaskScheduler.Core.Models.Response
{
    public class Error
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
