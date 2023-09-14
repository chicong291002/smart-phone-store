using FluentValidation;
using System;

namespace ShoeStore.ViewModels.System.Users.CheckUserValidator
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.firstName).NotEmpty().WithMessage("firstName is Required").
                MaximumLength(200).WithMessage("First name cannot over 200 characters");

            RuleFor(x => x.lastName).NotEmpty().WithMessage("lastName is Required").
                MaximumLength(200).WithMessage("Last Name cannot over 200 characters");

            RuleFor(x => x.Dob).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Birhthday cannot greater than 100 years");

            RuleFor(x => x.email).NotEmpty().WithMessage("Email is Required").
                Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email format not match");

            RuleFor(x => x.phoneNumber).NotEmpty().WithMessage("phoneNumber is Required").
               MaximumLength(10).WithMessage("phoneNumber cannot over 10 numbers");

            RuleFor(x => x.userName).NotEmpty().WithMessage("Username is Required");

            RuleFor(x => x.passWord).NotEmpty().WithMessage("Password is Required")
                .MinimumLength(6).WithMessage("Password is at least 6 characters");

            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.passWord != request.confirmPassword)
                {
                    context.AddFailure("Confirm password is not match");
                }
            });
        }
    }
}
