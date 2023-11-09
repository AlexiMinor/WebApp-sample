using FluentValidation;
using WebApp.MVC7.Models;
using WebApp.Repositories;

namespace WebApp.MVC7.FluentValidation;

public class UserRegisterValidator : AbstractValidator<UserRegisterModel>
{
    private readonly IUnitOfWork _unitOfWork;
    public UserRegisterValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(regUser => regUser.Email)
            .EmailAddress()
            .NotEmpty();
            //.NotEqual(model => _unitOfWork.UserRepository.FindBy().CheckEmail(model.Email));

            RuleFor(model => model.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(64);
                //.Matches("");

            RuleFor(regUser => regUser.PasswordConfirmation)
                .Equal(model => model.Password);


            
    }
}