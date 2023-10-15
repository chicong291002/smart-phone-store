using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SmartPhoneStore.Data.EF
{
    public class SmartPhoneStoreDbContextFactory : IDesignTimeDbContextFactory<SmartPhoneStoreDbContext>
    {
        public SmartPhoneStoreDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("SmartPhoneStoreDb");

            var optionsBuilder = new DbContextOptionsBuilder<SmartPhoneStoreDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new SmartPhoneStoreDbContext(optionsBuilder.Options);
        }
    }
}
