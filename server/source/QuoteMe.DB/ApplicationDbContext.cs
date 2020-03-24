using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuoteMe.DB.Domain;
using System.Threading.Tasks;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace QuoteMe.DB
{
    public interface IApplicationDbContext
    {
        DbSet<Address> Addresses { get; set; }
        DbSet<AddressType> AddressTypes { get; set; }
        DbSet<Business> Businesses { get; set; }
        DbSet<ContactType> ContactTypes { get; set; }
        DbSet<CountryRegion> CountryRegions { get; set; }
        DbSet<Person> People { get; set; }
        DbSet<Phone> PhoneNumbers { get; set; }
        DbSet<PhoneType> PhoneTypes { get; set; }
        DbSet<ServicesOffered> ServicesOffered { get; set; }
        DbSet<ServiceType> ServiceTypes { get; set; }
        DbSet<StateOrProvince> StateOrProvinces { get; set; }
        Task<int> SaveChangesAsync();
    }
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IConfiguration _config;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<CountryRegion> CountryRegions { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Phone> PhoneNumbers { get; set; }
        public DbSet<PhoneType> PhoneTypes { get; set; }
        public DbSet<ServicesOffered> ServicesOffered { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<StateOrProvince> StateOrProvinces { get; set; }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_config.GetConnectionString("DefaultConnection"));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Address>().ToTable("Addresses");
            builder.Entity<AddressType>().ToTable("AddressTypes");
            builder.Entity<Business>().ToTable("Businesses");
            builder.Entity<ContactType>().ToTable("ContactTypes");
            builder.Entity<CountryRegion>().ToTable("CountryRegions");
            builder.Entity<Person>().ToTable("People");
            builder.Entity<Phone>().ToTable("PhoneNumbers");
            builder.Entity<PhoneType>().ToTable("PhoneTypes");
            builder.Entity<ServiceType>().ToTable("ServiceTypes");
            builder.Entity<StateOrProvince>().ToTable("StateOrProvinces");
        }
    }
}