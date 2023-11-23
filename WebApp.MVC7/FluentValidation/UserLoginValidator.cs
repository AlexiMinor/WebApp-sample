using FluentValidation;
using WebApp.MVC7.Models;
using WebApp.Repositories;

namespace WebApp.MVC7.FluentValidation;

public class UserLoginValidator : AbstractValidator<UserLoginModel>
{
    private readonly IUnitOfWork _unitOfWork;
    public UserLoginValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(regUser => regUser.Email)
            .EmailAddress()
            .NotEmpty();

            RuleFor(model => model.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(64);



            
    }
}