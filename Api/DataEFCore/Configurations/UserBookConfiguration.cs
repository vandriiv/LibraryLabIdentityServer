using DataEFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEFCore.Configurations
{
    public class UserBookConfiguration
    {
        public UserBookConfiguration(EntityTypeBuilder<UserBook> entity)
        {
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.BookId).HasColumnName("book_id");

            entity.Property(e => e.Count).HasColumnName("count");

            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Book)
                .WithMany(p => p.UserBook)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserBook__book_i__45F365D3");

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserBook)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserBook__user_i__44FF419A");
        }
    }
}
