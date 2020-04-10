using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuoteMe.DB.Domain;
using System.Data.Entity;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace QuoteMe.DB
{
    public interface IApplicationDbContext
    {
        IDbSet<Address> Addresses { get; set; }
        IDbSet<AddressType> AddressTypes { get; set; }
        IDbSet<Business> Businesses { get; set; }
        IDbSet<ContactType> ContactTypes { get; set; }
        IDbSet<CountryRegion> CountryRegions { get; set; }
        IDbSet<Client> Clients { get; set; }
        IDbSet<Phone> PhoneNumbers { get; set; }
        IDbSet<PhoneType> PhoneTypes { get; set; }
        IDbSet<ServicesOffered> ServicesOffered { get; set; }
        IDbSet<ServiceType> ServiceTypes { get; set; }
        IDbSet<StateOrProvince> StateOrProvinces { get; set; }
        int SaveChanges();
    }
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IConfiguration _config;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        public IDbSet<Address> Addresses { get; set; }
        public IDbSet<AddressType> AddressTypes { get; set; }
        public IDbSet<Business> Businesses { get; set; }
        public IDbSet<ContactType> ContactTypes { get; set; }
        public IDbSet<CountryRegion> CountryRegions { get; set; }
        public IDbSet<Client> Clients { get; set; }
        public IDbSet<Phone> PhoneNumbers { get; set; }
        public IDbSet<PhoneType> PhoneTypes { get; set; }
        public IDbSet<ServicesOffered> ServicesOffered { get; set; }
        public IDbSet<ServiceType> ServiceTypes { get; set; }
        public IDbSet<StateOrProvince> StateOrProvinces { get; set; }

        public int SaveChanges() => base.SaveChanges();

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
            builder.Entity<Client>().ToTable("Clients");
            builder.Entity<Phone>().ToTable("PhoneNumbers");
            builder.Entity<PhoneType>().ToTable("PhoneTypes");
            builder.Entity<ServiceType>().ToTable("ServiceTypes");
            builder.Entity<StateOrProvince>().ToTable("StateOrProvinces");
        }
    }
}