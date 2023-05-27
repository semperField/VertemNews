﻿namespace VertemNews.Application.Models
{
    public class NewsModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string Author { get; set; }
        public string Url { get; set; }
        public string UrlImage { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }
    }
}