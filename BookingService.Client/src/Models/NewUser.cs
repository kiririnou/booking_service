using Newtonsoft.Json;

namespace BookingService.Client.Models
{
    public class NewUser
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        
        [JsonProperty(PropertyName = "tgUid")]
        public string TgUid { get; set; }
    }
}