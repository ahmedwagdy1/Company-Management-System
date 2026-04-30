namespace Application.DAL.Data.Configurations
{
    internal class DepartmentConfiguration : BaseEntityConfiguration<Department>, IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Id).UseIdentityColumn(10, 10);
            builder.Property(d => d.Name).HasColumnType("varchar(20)");
            builder.Property(d => d.Code).HasColumnType("varchar(20)");
            builder.HasMany(d => d.Employees)
                .WithOne(d => d.Department)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
            //builder.Property(d => d.CreatedOn).HasDefaultValueSql("GETDATE()");
            //builder.Property(d => d.ModifiedOn).HasComputedColumnSql("GETDATE()");
            base.Configure(builder);
        }
    }
}
