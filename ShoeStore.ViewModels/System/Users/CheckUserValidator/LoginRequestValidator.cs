using FluentValidation;
using SmartPhoneStore.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPhoneStore.ViewModels.System.Users.CheckUserValidator
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is Required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is Required")
                .MinimumLength(6).WithMessage("Password is at least 6 characters");
        }
    }
}
