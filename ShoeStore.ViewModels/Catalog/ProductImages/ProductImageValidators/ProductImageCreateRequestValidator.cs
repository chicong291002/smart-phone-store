using FluentValidation;
using ShoeStore.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.ViewModels.Catalog.ProductImages.ProductImageValidators
{
    public class ProductImageCreateRequestValidator : AbstractValidator<ProductImageCreateRequest>
    {
        public ProductImageCreateRequestValidator()
        {
            RuleFor(x => x.Caption).NotEmpty().WithMessage("Caption is Required");

            RuleFor(x => x.IsDefault).NotEmpty().WithMessage("IsDefault is Required");

            RuleFor(x => x.SortOrder).NotEmpty().WithMessage("SortOrder is Required");

            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is Required");

        }

    }
}
