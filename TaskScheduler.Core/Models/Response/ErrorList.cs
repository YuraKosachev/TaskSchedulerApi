using Newtonsoft.Json;

namespace TaskScheduler.Core.Models.Response
{
    public class ErrorList
    {
        [JsonProperty("errors")]
        public IList<Error> Errors { get; set; }
    }
}
