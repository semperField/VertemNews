using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace VertemNews.Application.Models
{    
    public class NewApiModel
    {
        [JsonProperty("articles")]
        [JsonPropertyName("articles")]
        public List<Articles> Articles { get; set; }
    }

    public class Articles
    {
        [JsonProperty("author")]
        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonProperty("title")]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonProperty("urlImage")]
        [JsonPropertyName("urlImage")]
        public string UrlImage { get; set; }

        [JsonProperty("publishedAt")]
        [JsonPropertyName("publishedAt")]
        public DateTime PublishedAt { get; set; }

        [JsonProperty("content")]
        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonProperty("source")]
        [JsonPropertyName("source")]
        public source Source { get; set; }
    }

    public class source
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}