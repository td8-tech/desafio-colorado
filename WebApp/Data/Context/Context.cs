using Data.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;


namespace Data.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) {
            this.Database.EnsureCreated();
        }
        public DbSet<ClientEntity> Client => Set<ClientEntity>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientEntity>().HasKey(c => c.Id);
        }
    }
}
