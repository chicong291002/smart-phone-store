using Microsoft.EntityFrameworkCore;
using ShoeStore.Application.Common;
using ShoeStore.Data.EF;
using ShoeStore.ViewModels.Utilities.Slides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Utilities.Slides
{
    public class SlideService : ISlideService
    {
        private readonly ShoeStoreDbContext _context; //readonly la chi gan 1 lan

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
