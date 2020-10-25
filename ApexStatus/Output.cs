﻿namespace ApexScheduler.ApexStatus
{
    using global::System.Collections.Generic;
    using global::System.Text.Json.Serialization;

    public class Output
    {
        [JsonPropertyName("did")]
        public string Did { get; set; }

        [JsonPropertyName("gid")]
        public string Gid { get; set; }

        [JsonPropertyName("ID")]
        public int Id { get; set; }

        [JsonPropertyName("intensity")]
        public int? Intensity { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        public List<string> Status { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}