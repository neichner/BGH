using Newtonsoft.Json;

namespace NE.DataModels {
    public partial class CardData
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }
}