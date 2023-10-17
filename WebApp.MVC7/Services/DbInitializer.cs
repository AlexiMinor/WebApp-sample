//using Microsoft.EntityFrameworkCore;
//using WebApp.Data;
//using WebApp.Data.Entities;
//using WebApp.Repositories;

//namespace WebApp.MVC7.Services;

//public class TestService : IDbInitializer
//{
//    private readonly ArticleRepository _articleRepository;

//    public TestService(ArticleRepository repository)
//    {
//        _articleRepository = repository;
//    }

//    public async Task InitDbWithTestValues()
//    {
//        //if (!_context.ArticleSources.Any())
//        //{
//        //        var articleSource = new ArticleSource()
//        //        {
//        //            Id = Guid.NewGuid(),
//        //            Name = "Onliner",
//        //            Url = "https://onliner.by"
//        //        };

//        //        await _context.ArticleSources.AddAsync(articleSource);
//        //        await _context.SaveChangesAsync();
                


//        //        //await InsertArticles();
//        //}

        
//    }

//    public async Task InsertArticles()
//    {
//        //var articleSource = _context.ArticleSources.AsNoTracking().FirstOrDefault();
//        var articles = new List<Article>()
//            {
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id
//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                },
//                new Article
//                {
//                    Id = Guid.NewGuid(),
//                    Date = DateTime.Now,
//                    Title = $"Article {new Random().Next(0, 10000)}",
//                    Text =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ultrices ante ac nulla scelerisque sodales. Phasellus varius feugiat risus sed finibus. Phasellus elementum elit eget mauris ornare, at tincidunt metus ornare. Donec pulvinar tincidunt purus a imperdiet. Vivamus dapibus, libero nec condimentum fringilla, enim libero tincidunt mi, vitae sodales lacus eros a justo. Donec id nisl in arcu laoreet tristique. Aliquam nec mi molestie, condimentum sem quis, tincidunt nisl. Integer vel dapibus nunc. Pellentesque ornare erat quis sem laoreet feugiat. Nunc auctor leo eget felis porttitor, a dictum massa tempor. Vestibulum nec est nunc. Quisque ex est, accumsan sit amet velit vitae, vehicula facilisis magna. Donec sed consequat ante. Nulla consequat leo in neque viverra porta. Vivamus augue risus, tempus eget elit vitae, mollis placerat felis.",
//                    Description =
//                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur at purus sem. Integer et ultrices.",
//                    ArticleSourceId = articleSource.Id

//                }
//            };
//        await _articleRepository.InsertArticles(articles);
//    }
//}
