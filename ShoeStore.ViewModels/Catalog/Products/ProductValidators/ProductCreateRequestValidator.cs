using FluentValidation;
using SmartPhoneStore.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPhoneStore.ViewModels.Catalog.Products.ProductValidators
{
    public class ProductImageCreateRequestValidator : AbstractValidator<ProductCreateRequest>
    {
        public ProductImageCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is Required")
                    .MaximumLength(1000).WithMessage("Description is at least 1000 characters");

            RuleFor(x => x.Stock).NotEmpty().WithMessage("Stock is Required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is Required");
        }

    }
}
