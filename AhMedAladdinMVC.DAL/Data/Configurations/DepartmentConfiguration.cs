using AhMedAladdinMVC.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhMedAladdinMVC.DAL.Data.Configurations
{
	internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			builder.Property(d => d.Id).UseIdentityColumn(10,10);
			builder.Property(d => d.Code).HasColumnType("varchar").HasMaxLength(50);
			builder.Property(d => d.Name).HasColumnType("varchar").HasMaxLength(50);


			builder.HasMany(d => d.Employees).WithOne(e => e.Department).HasForeignKey(e => e.DepartmentId).OnDelete(DeleteBehavior.Cascade);
		}

	
	}
	
	}

