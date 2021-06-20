using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.Api.Business.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.Api.Infra.Data.Mappings
{
    public class CourseMapping : IEntityTypeConfiguration<CourseEntity>
    {
        public void Configure(EntityTypeBuilder<CourseEntity> builder)
        {
            builder.ToTable("courses");
            builder.HasKey(c => c.Code);
            builder.Property(c => c.Code).ValueGeneratedOnAdd();
            builder.Property(c => c.Name);
            builder.Property(c => c.Description);
            builder.HasOne(c => c.User).WithMany().HasForeignKey(fk => fk.UserCode);
        }
    }
}
