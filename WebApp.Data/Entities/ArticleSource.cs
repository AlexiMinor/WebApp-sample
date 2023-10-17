namespace WebApp.Data.Entities
{
    public class ArticleSource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Url { get; set; }

        public List<Article> Articles { get; set; }
    }
}