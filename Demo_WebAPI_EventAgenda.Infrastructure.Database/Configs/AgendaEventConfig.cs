using Demo_WebAPI_EventAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database.Configs
{
    internal class AgendaEventConfig : IEntityTypeConfiguration<AgendaEvent>
    {
        public void Configure(EntityTypeBuilder<AgendaEvent> builder)
        {
            // Table
            builder.ToTable("Agenda_Events");

            // Clef
            builder.HasKey(ae => ae.Id)
                .HasName("PK_Agenda_Events")
                .IsClustered();

            // Colonnes
            builder.Property(ae => ae.Id)
                .ValueGeneratedOnAdd();

            builder.Property(ae => ae.Name)
                .HasMaxLength(500)
                .IsUnicode()    // -> NVARCHAR
                .IsRequired();

            builder.Property(ae => ae.Location)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired(false);

            builder.Property(ae => ae.StartDate)
                .HasColumnName("Start_Date")
                .HasColumnType("DATETIME2")
                .IsRequired();

            builder.Property(ae => ae.EndDate)
                .HasColumnName("End_Date")
                .HasColumnType("DATETIME2")
                .IsRequired(false);

            // Index
            builder.HasIndex(ae => new { ae.Name, ae.Location, ae.StartDate })
                .IsUnique()
                .HasDatabaseName("IDX_Agenda_Events__Name_Loc_Date");
        }
    }
}
