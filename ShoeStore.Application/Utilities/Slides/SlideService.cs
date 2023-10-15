using Microsoft.EntityFrameworkCore;
using SmartPhoneStore.Application.Common;
using SmartPhoneStore.Data.EF;
using SmartPhoneStore.ViewModels.Utilities.Slides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPhoneStore.Application.Utilities.Slides
{
    public class SlideService : ISlideService
    {
        private readonly SmartPhoneStoreDbContext _context; //readonly la chi gan 1 lan
        public SlideService(SmartPhoneStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<SlideViewModel>> GetAll()
        {
            var slides = await _context.Slides.OrderBy(x => x.SortOrder)
                .Select
                (x => new SlideViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Url = x.Url,
                    Image = x.Image
                }).ToListAsync();
            return slides;
        }
    }
}
