using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace Example.Infra.Data.Mapping
{

    public class CidadeContext : DbContext
    {
        public DbSet<Domain.ExampleAggregate.Cidade> Cidade { get; set; }
        public CidadeContext(DbContextOptions<CidadeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CidadeEntityTypeConfiguration());
            modelBuilder.Entity<Domain.ExampleAggregate.Cidade>();
        }
    }
    public class CidadeEntityTypeConfiguration : IEntityTypeConfiguration<Domain.ExampleAggregate.Cidade>
    {
        public void Configure(EntityTypeBuilder<Domain.ExampleAggregate.Cidade> orderConfiguration)
        {
            orderConfiguration.ToTable("Cidade", "dbo");

            orderConfiguration.HasKey(o => o.Id);
            orderConfiguration.Property(o => o.Id).UseIdentityColumn();
            orderConfiguration.Property(o => o.Nome).IsRequired().HasColumnType("varchar").HasMaxLength(200);
            orderConfiguration.Property(o => o.UF).IsRequired().HasColumnType("varchar").HasMaxLength(2);
        }
    }
}
