namespace PollutionPatrol.Modules.Report.Infrastructure.Persistence.EntityTypeConfigurations;

internal sealed class ReportEntityTypeConfiguration : IEntityTypeConfiguration<Domain.ReportAggregate.Report>
{
    public void Configure(EntityTypeBuilder<Domain.ReportAggregate.Report> builder)
    {
        builder.ToTable("Reports");

        builder.OwnsOne(r => r.Status, statusBuilder =>
            statusBuilder.Property(s => s.Value).HasColumnName("Status").IsRequired());

        builder.OwnsOne(r => r.PollutionType, pollutionTypeBuilder =>
            pollutionTypeBuilder.Property(pt => pt.Value).HasColumnName("PollutionType").IsRequired());

        builder.HasIndex(r => r.LocationCoordinates)
            .HasMethod("gist");

        builder.Property(r => r.LocationCoordinates)
            .HasColumnType("geography(Point, 4326)")
            .IsRequired();

        builder.Property(r => r.UserId).IsRequired();
        builder.Property(r => r.ReportDate).IsRequired();
    }
}