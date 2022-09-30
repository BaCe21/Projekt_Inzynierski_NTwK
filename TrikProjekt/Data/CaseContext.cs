namespace TrikProjekt56.Data
{
    public class CaseContext : IdentityDbContext
    {
        public CaseContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Age> Ages { get; set; }
        public virtual DbSet<Hair> Hairs { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Religion> Religions { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Height> Heights { get; set; }
        public virtual DbSet<Weight> Weights { get; set; }
        public virtual DbSet<Case> Cases { get; set; }
    }
}
