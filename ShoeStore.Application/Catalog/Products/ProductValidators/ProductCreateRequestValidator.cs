using FluentValidation;
using ShoeStore.Application.Catalog.Products.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Catalog.Products.ProductValidators
{
    public class ProductImageCreateRequestValidator : AbstractValidator<ProductCreateRequest>
    {
        public ProductImageCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is Required")
                    .MinimumLength(1000).WithMessage("Description is at least 1000 characters");

            RuleFor(x => x.Thumbnail).NotEmpty().WithMessage("Thumbnail is Required");

            RuleFor(x => x.Stock).NotEmpty().WithMessage("Stock is Required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is Required");

            RuleFor(x => x.OriginalPrice).NotEmpty().WithMessage("OriginalPrice is Required");

            RuleFor(x => x.Image).NotEmpty().WithMessage("Image is Required");

        }

    }
}
