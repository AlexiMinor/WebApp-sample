using System.ServiceModel.Syndication;
using Riok.Mapperly.Abstractions;
using WebApp.Core;
using WebApp.Data.Entities;
using WebApp.Models;

namespace WebApp.Mappers;

[Mapper]
public partial class ArticleMapper
{
    public partial ArticleDto ArticleToArticleDto(Article article);
    public partial Article ArticleDtoToArticle(ArticleDto article);
    public partial ArticleModel ArticleDtoToArticleModel(ArticleDto article);
    public partial ArticleDto ArticleModelToArticleDto(ArticleModel article);

}