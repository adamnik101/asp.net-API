using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Termin3.DataAccess.Entities;

namespace Termin3.DataAccess.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(g => g.Users)
                .WithOne(u => u.Group)
                .HasForeignKey(k => k.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
