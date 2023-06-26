using Newtonsoft.Json;
using System;
using UnityEngine;

namespace NE.DataModels {

    [Serializable]
    public class CardData
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("image-card-low-level")]
        public string ImageCardLowLevel;

        [JsonProperty("image-card-high-level")]
        public string ImageCardHighLevel;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("eliteHealthLevels")]
        public int[] EliteHealthLevels;
        [JsonProperty("normalHealthLevels")]
        public int[] Normal;

        public GameObject LowLevelImage;
        public GameObject HighLevelImage;
    }
}