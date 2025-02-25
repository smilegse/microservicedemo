using MongoRepo.Context;

namespace Catalog.API.Context
{
    public class CatalogDbContext : ApplicationDbContext
    {
        private static IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
        private static string connectionString = Configuration.GetConnectionString("Catalog.API");
        private static string databaseName = Configuration.GetValue<string>("DatabaseName");

        public CatalogDbContext() : base(connectionString, databaseName)
        {

        }
    }
}
