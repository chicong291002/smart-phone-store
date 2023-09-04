using FluentValidation;
using ShoeStore.Application.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Common
{
    public class PagingRequestBaseValidator : AbstractValidator<PagingRequestBase>
    {
        public PagingRequestBaseValidator()
        {
            RuleFor(x => x.pageIndex).NotEmpty().WithMessage("pageIndex is Required");

            RuleFor(x => x.pageSize).NotEmpty().WithMessage("pageSize is Required");
        }
    }
}
