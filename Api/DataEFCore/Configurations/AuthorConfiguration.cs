using DataEFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEFCore.Configurations
{
    public class AuthorConfiguration
    {
        public AuthorConfiguration(EntityTypeBuilder<Author> entity)
        {
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnName("first_name")
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasColumnName("last_name")
                .HasMaxLength(30)
                .IsUnicode(false);
        }
    }
}
