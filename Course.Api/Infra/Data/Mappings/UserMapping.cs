using Course.Api.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Infra.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(u => u.Code);
            builder.Property(u => u.Code).ValueGeneratedOnAdd();
            builder.Property(u => u.Email);
            builder.Property(u => u.Login);
            builder.Property(u => u.Password);
        }
    }
}
