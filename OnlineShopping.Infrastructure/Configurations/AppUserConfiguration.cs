
namespace OnlineShopping.Infrastructure.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Street).IsRequired();
            builder.Property(u => u.City).IsRequired();
            builder.Property(u => u.ZipCode).IsRequired();

            builder.Property(u => u.Email).IsRequired();
        }
    }
}
