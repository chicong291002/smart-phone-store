using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Data.Configurations
{
    public class SubcategoryConfiguration : IEntityTypeConfiguration<Subcategory>
    {
        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {
            builder.ToTable("Subcategories");

            builder.HasKey(x => x.SubcategoryId);
            builder.Property(x => x.SubcategoryId).HasColumnName("SubcategoryId").IsRequired();
            builder.Property(x => x.CategoryId).HasColumnName("CategoryId").IsRequired();
            builder.Property(x => x.SubcategoryName).HasColumnName("SubcategoryName").IsRequired();

            // Relationship with Category
            builder.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
