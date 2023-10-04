using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartPhoneStore.Data.Entities;
using SmartPhoneStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPhoneStore.Data.Configurations 
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.Thumbnail).HasMaxLength(300).IsRequired(false);

            builder.Property(x => x.ProductImage).HasMaxLength(300).IsRequired(false);


            // Cấu hình mối quan hệ một-nhiều với OrderDetail
            builder.HasMany(p => p.OrderDetails)
                .WithOne(od => od.Product)
                .HasForeignKey(od => od.productId);

        }
    }
}
