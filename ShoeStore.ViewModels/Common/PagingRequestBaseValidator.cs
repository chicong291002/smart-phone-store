using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPhoneStore.ViewModels.Common
{
    public class PagingRequestBaseValidator : AbstractValidator<PageResultBase>
    {
        public PagingRequestBaseValidator()
        {
            RuleFor(x => x.PageIndex).NotEmpty().WithMessage("pageIndex is Required");

            RuleFor(x => x.PageSize).NotEmpty().WithMessage("pageSize is Required");
        }
    }
}
