using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace Example.Infra.Data.Mapping
{

    public class PessoaContext : DbContext
    {
        public DbSet<Domain.ExampleAggregate.Pessoa> Pessoa { get; set; }
        public PessoaContext(DbContextOptions<PessoaContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PessoaEntityTypeConfiguration());
            modelBuilder.Entity<Domain.ExampleAggregate.Pessoa>();
        }
    } 

    public class PessoaEntityTypeConfiguration : IEntityTypeConfiguration<Domain.ExampleAggregate.Pessoa>
    {
        public void Configure(EntityTypeBuilder<Domain.ExampleAggregate.Pessoa> builder)
        {
            builder.ToTable("Pessoa", "dbo");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).UseIdentityColumn();
            builder.Property(o => o.Nome).IsRequired().HasColumnType("varchar").HasMaxLength(300);
            builder.Property(o => o.CPF).IsRequired().HasColumnType("varchar").HasMaxLength(11);
            builder.Property(o => o.Idade).IsRequired();
            builder.Property(b => b.Id_Cidade).IsRequired();




        }
    }       
}
