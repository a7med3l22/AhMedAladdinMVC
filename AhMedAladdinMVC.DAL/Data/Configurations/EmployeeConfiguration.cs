using AhMedAladdinMVC.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhMedAladdinMVC.DAL.Data.Configurations
{
	public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.Property(e => e.Name).HasColumnType("varchar").HasMaxLength(50);
			builder.Property(e => e.Salary).HasColumnType("decimal(12,2)");
			builder.Property(e => e.Gender).HasConversion<string>();
			builder.Property(e => e.Name).HasMaxLength(50);

		}
	}
}
