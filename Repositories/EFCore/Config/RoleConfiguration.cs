using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Config
{
	public class RoleConfiguration : IEntityTypeConfiguration<IdentityUserRole>
	{
		public void Configure(EntityTypeBuilder<IdentityUserRole> builder)
		{
			builder.HasData(
				new IdentityUserRole
				{
					Name = "Admin",
					NormalizedName = "ADMIN"
				},
				new IdentityUserRole
				{
					Name = "Educator",
					NormalizedName = "EDUCATOR"
				},
				new IdentityUserRole
				{
					Name = "Student",
					NormalizedName="STUDENT"
				}
				);
		}
	}
}
