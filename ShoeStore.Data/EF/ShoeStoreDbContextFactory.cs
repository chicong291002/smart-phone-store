using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ShoeStore.Data.EF
{
    public class ShoeStoreDbContextFactory : IDesignTimeDbContextFactory<ShoeStoreDbContext>
    {
        public ShoeStoreDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "ShoeStore.Data"))
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ShoeStoreDb");

            var optionsBuilder = new DbContextOptionsBuilder<ShoeStoreDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ShoeStoreDbContext(optionsBuilder.Options);
        }
    }
}
