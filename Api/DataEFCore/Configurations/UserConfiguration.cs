using DataEFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEFCore.Configurations
{
    public class UserConfiguration
    {
        public UserConfiguration(EntityTypeBuilder<User> entity)
        {
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasColumnName("phone_number")
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.Property(e => e.Role).HasColumnName("role")
                .HasMaxLength(60)
                .IsUnicode(false);
        }
    }
}
