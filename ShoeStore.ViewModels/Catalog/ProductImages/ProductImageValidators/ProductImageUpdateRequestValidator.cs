using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPhoneStore.ViewModels.Catalog.ProductImages.ProductImageValidators
{
    public class ProductImageUpdateRequestValidator : AbstractValidator<ProductImageUpdateRequest>
    {
        public ProductImageUpdateRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is Required");

            RuleFor(x => x.Caption).NotEmpty().WithMessage("Caption is Required");

            RuleFor(x => x.IsDefault).NotEmpty().WithMessage("IsDefault is Required");

            RuleFor(x => x.SortOrder).NotEmpty().WithMessage("SortOrder is Required");

            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is Required");

        }

    }
}
