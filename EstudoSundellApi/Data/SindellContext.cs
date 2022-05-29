using System;
using EstudoSundellApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EstudoSundellApi.Data
{
	public class SindellContext : DbContext
	{
		public DbSet<Family>? Families { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			optionsBuilder.UseCosmos(System.Configuration.ConfigurationManager.AppSettings["EndPointUri"]!, System.Configuration.ConfigurationManager.AppSettings["PrimaryKey"]!, "ToDoList");
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Family>().ToContainer("Items").HasPartitionKey(e => e.PartitionKey).HasKey(x=> new { x.Id});
        }
	}
}

