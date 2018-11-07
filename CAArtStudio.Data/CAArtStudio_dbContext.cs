namespace CAArtStudio.Model
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class CAArtStudio_dbContext : DbContext
	{
		public CAArtStudio_dbContext()
			: base("name=CAArtStudio_dbContext")
		{
		}

		public virtual DbSet<Article> Articles { get; set; }
		public virtual DbSet<ArticleCategory> ArticleCategories { get; set; }
		public virtual DbSet<Config> Configs { get; set; }
		public virtual DbSet<Course> Courses { get; set; }
		public virtual DbSet<CourseCategory> CourseCategories { get; set; }
		public virtual DbSet<CourseFeedback> CourseFeedbacks { get; set; }
		public virtual DbSet<CourseSlider> CourseSliders { get; set; }
		public virtual DbSet<CourseTeacher> CourseTeachers { get; set; }
		public virtual DbSet<Group> Groups { get; set; }
		public virtual DbSet<Menu> Menus { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<Registed> Registeds { get; set; }
		public virtual DbSet<Slider> Sliders { get; set; }
		public virtual DbSet<User> Users { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Article>()
				.Property(e => e.MetaTitle)
				.IsUnicode(false);

			modelBuilder.Entity<Article>()
				.Property(e => e.ViewCount)
				.IsFixedLength();

			modelBuilder.Entity<ArticleCategory>()
				.Property(e => e.MetaTitle)
				.IsUnicode(false);

			modelBuilder.Entity<ArticleCategory>()
				.HasMany(e => e.Articles)
				.WithOptional(e => e.ArticleCategory)
				.HasForeignKey(e => e.CategoryID);

			modelBuilder.Entity<Course>()
				.Property(e => e.MetaTitle)
				.IsUnicode(false);

			modelBuilder.Entity<Course>()
				.Property(e => e.ViewCount)
				.IsFixedLength();

			modelBuilder.Entity<Course>()
				.HasMany(e => e.CourseFeedbacks)
				.WithRequired(e => e.Course)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Course>()
				.HasMany(e => e.CourseSliders)
				.WithRequired(e => e.Course)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Course>()
				.HasMany(e => e.CourseTeachers)
				.WithRequired(e => e.Course)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<CourseCategory>()
				.Property(e => e.MetaTitle)
				.IsUnicode(false);

			modelBuilder.Entity<CourseCategory>()
				.HasMany(e => e.Courses)
				.WithOptional(e => e.CourseCategory)
				.HasForeignKey(e => e.CategoryID);

			modelBuilder.Entity<CourseCategory>()
				.HasMany(e => e.CourseCategory1)
				.WithOptional(e => e.CourseCategory2)
				.HasForeignKey(e => e.ParentID);

			modelBuilder.Entity<Group>()
				.HasMany(e => e.Users)
				.WithOptional(e => e.Group)
				.HasForeignKey(e => e.GroupID);

			modelBuilder.Entity<Menu>()
				.Property(e => e.Target)
				.IsUnicode(false);

			modelBuilder.Entity<User>()
				.Property(e => e.UserName)
				.IsUnicode(false);

			modelBuilder.Entity<User>()
				.Property(e => e.Password)
				.IsUnicode(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Articles)
				.WithRequired(e => e.User)
				.HasForeignKey(e => e.CreatedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Articles1)
				.WithRequired(e => e.User1)
				.HasForeignKey(e => e.ModifiedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.ArticleCategories)
				.WithRequired(e => e.User)
				.HasForeignKey(e => e.CreatedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.ArticleCategories1)
				.WithRequired(e => e.User1)
				.HasForeignKey(e => e.ModifiedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Configs)
				.WithRequired(e => e.User)
				.HasForeignKey(e => e.CreatedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Configs1)
				.WithRequired(e => e.User1)
				.HasForeignKey(e => e.ModifiedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Configs2)
				.WithRequired(e => e.User2)
				.HasForeignKey(e => e.CreatedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Configs3)
				.WithRequired(e => e.User3)
				.HasForeignKey(e => e.ModifiedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Courses)
				.WithRequired(e => e.User)
				.HasForeignKey(e => e.CreatedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Courses1)
				.WithRequired(e => e.User1)
				.HasForeignKey(e => e.ModifiedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.CourseCategories)
				.WithRequired(e => e.User)
				.HasForeignKey(e => e.CreatedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.CourseCategories1)
				.WithRequired(e => e.User1)
				.HasForeignKey(e => e.ModifiedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.CourseFeedbacks)
				.WithOptional(e => e.User)
				.HasForeignKey(e => e.StudentID);

			modelBuilder.Entity<User>()
				.HasMany(e => e.CourseFeedbacks1)
				.WithRequired(e => e.User1)
				.HasForeignKey(e => e.CreatedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.CourseFeedbacks2)
				.WithRequired(e => e.User2)
				.HasForeignKey(e => e.ModifiedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.CourseSliders)
				.WithRequired(e => e.User)
				.HasForeignKey(e => e.CreatedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.CourseSliders1)
				.WithRequired(e => e.User1)
				.HasForeignKey(e => e.ModifiedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.CourseTeachers)
				.WithOptional(e => e.User)
				.HasForeignKey(e => e.TeacherID);

			modelBuilder.Entity<User>()
				.HasMany(e => e.CourseTeachers1)
				.WithRequired(e => e.User1)
				.HasForeignKey(e => e.CreatedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.CourseTeachers2)
				.WithRequired(e => e.User2)
				.HasForeignKey(e => e.ModifiedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Groups)
				.WithRequired(e => e.User)
				.HasForeignKey(e => e.CreatedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Groups1)
				.WithRequired(e => e.User1)
				.HasForeignKey(e => e.ModifiedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Menus)
				.WithRequired(e => e.User)
				.HasForeignKey(e => e.CreatedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Menus1)
				.WithRequired(e => e.User1)
				.HasForeignKey(e => e.ModifiedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Products)
				.WithRequired(e => e.User)
				.HasForeignKey(e => e.CreatedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Products1)
				.WithRequired(e => e.User1)
				.HasForeignKey(e => e.ModifiedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Products2)
				.WithOptional(e => e.User2)
				.HasForeignKey(e => e.UserID);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Registeds)
				.WithRequired(e => e.User)
				.HasForeignKey(e => e.CreatedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Registeds1)
				.WithRequired(e => e.User1)
				.HasForeignKey(e => e.ModifiedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Sliders)
				.WithRequired(e => e.User)
				.HasForeignKey(e => e.CreatedBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Sliders1)
				.WithRequired(e => e.User1)
				.HasForeignKey(e => e.ModifiedBy)
				.WillCascadeOnDelete(false);
		}
	}
}
