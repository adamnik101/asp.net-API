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
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasAlternateKey(x => x.Username);
            builder.Property(x => x.Username).HasMaxLength(20);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(30);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(30);
        }
    }
}
