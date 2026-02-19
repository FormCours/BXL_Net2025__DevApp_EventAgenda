using Demo_WebAPI_EventAgenda.Domain.Enums;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database.Configs
{
    internal class MemberConfig : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            // Table
            builder.ToTable("Members");

            // Clef
            builder.HasKey(m => m.Id)
                .HasName("PK_Member")
                .IsClustered();

            // Colonnes
            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd();

            builder.Property(m => m.Email)
                .HasMaxLength(320)
                .IsUnicode()
                .IsRequired();

            builder.Property(m => m.Pseudo)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired(false);

            builder.Property(m => m.Role)
                .HasConversion<string>()
                .HasDefaultValue(MemberRoleEnum.Peon)
                .HasSentinel(0)
                .IsRequired();

            builder.Property(m => m.HashPwd)
                .HasColumnName("Hash_Pwd")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(m => m.AllowNewsletter)
                .HasColumnName("Allow_Newsletter")
                .IsRequired();

            // Index
            builder.HasIndex(m => m.Email)
                .IsUnique()
                .HasDatabaseName("IDX_Members__email");
        }
    }
}
