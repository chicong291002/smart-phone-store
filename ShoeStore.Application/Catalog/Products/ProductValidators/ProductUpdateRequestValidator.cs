using FluentValidation;
using ShoeStore.Application.Catalog.Products.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Catalog.Products.ProductValidators
{
    internal class ProductImageUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
    {
        public ProductImageUpdateRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is Required");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Description is Required");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Thumbnail is Required")
                .MinimumLength(1000).WithMessage("Description not over 1000 characters");

            RuleFor(x => x.Thumbnail).NotEmpty().WithMessage("Thumbnail is Required");

            RuleFor(x => x.Image).NotEmpty().WithMessage("Image is Required");

        }

    }
}
