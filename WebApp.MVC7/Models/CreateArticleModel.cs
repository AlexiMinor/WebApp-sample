using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.MVC7.Models;

public class CreateArticleModel
{
    [Required(ErrorMessage = "Bla bla bla")]
    //[RegularExpression()]
    public string Title { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 10)]
    public string Description { get; set; }

    //[Range(0, 1000)]
    //[Required]
    ////Only client validation. As all client validation required jquery + jquery validation libraries
    //[EmailAddress]
    //[CreditCard]
    //[Remote(action:"CheckEmail", controller:"User", ErrorMessage = "Email already used", HttpMethod = "Get")]
    //public string Email { get; set; }

    //[Compare(nameof(Email))]
    //public int ValueConfirmation { get; set; }

    [Required]
    public Guid SourceId { get; set; }

    public List<SelectListItem>? AvailableSources { get; set;}
}