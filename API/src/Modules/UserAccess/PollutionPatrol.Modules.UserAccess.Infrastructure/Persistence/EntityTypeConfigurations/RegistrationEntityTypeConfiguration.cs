namespace PollutionPatrol.Modules.UserAccess.Infrastructure.Persistence.EntityTypeConfigurations;

internal sealed class RegistrationEntityTypeConfiguration : IEntityTypeConfiguration<Registration>
{
    public void Configure(EntityTypeBuilder<Registration> builder)
    {
        builder.ToTable("Registrations");

        builder.OwnsOne(r => r.Status, statusBuilder =>
            statusBuilder.Property(s => s.Value).HasColumnName("Status").IsRequired());

        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.PasswordHash).IsRequired();
        builder.Property(x => x.Salt).IsRequired();
        builder.Property(x => x.ConfirmationToken).IsRequired();
        builder.Property(x => x.RegisterDate).IsRequired();
    }
}