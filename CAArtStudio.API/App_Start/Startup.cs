using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CAArtStudio.Data.Infrastructure;
using CAArtStudio.Data.Repositories;
using CAArtStudio.Model;
using CAArtStudio.Service;
using Microsoft.Owin;
using OHTTaxSupportApplication.Web;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace OHTTaxSupportApplication.Web
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
			ConfigAutofac(app);
		}
		private void ConfigAutofac(IAppBuilder app)
		{
			var builder = new ContainerBuilder();
			builder.RegisterControllers(Assembly.GetExecutingAssembly());
			// Register your Web API controllers.
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly()); //Register WebApi Controllers

			builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
			builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

			builder.RegisterType<CAArtStudio_dbContext>().AsSelf().InstancePerRequest();

			// Repositories      
			builder.RegisterAssemblyTypes(typeof(ArticleCategoryRepository).Assembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(ArticleRepository).Assembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(ConfigRepository).Assembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(CourseCategoryRepository).Assembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(CourseFeedbackRepository).Assembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(CourseRepository).Assembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(CourseSliderRepository).Assembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(CourseTeacherRepository).Assembly)
				 .Where(t => t.Name.EndsWith("Repository"))
				 .AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(ErrorRepository).Assembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(GroupRepository).Assembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(MenuRepository).Assembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(ProductRepository).Assembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(RegistedRepository).Assembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(SliderRepository).Assembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces().InstancePerRequest();

			// Services                
			builder.RegisterAssemblyTypes(typeof(ArticleCategoryService).Assembly)
				.Where(t => t.Name.EndsWith("Service"))
				.AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(ArticleService).Assembly)
				.Where(t => t.Name.EndsWith("Service"))
				.AsImplementedInterfaces().InstancePerRequest();
			//builder.RegisterAssemblyTypes(typeof(ConfigService).Assembly)
			//    .Where(t => t.Name.EndsWith("Service"))
			//    .AsImplementedInterfaces().InstancePerRequest();
			//builder.RegisterAssemblyTypes(typeof(CourseCategoryService).Assembly)
			//    .Where(t => t.Name.EndsWith("Service"))
			//    .AsImplementedInterfaces().InstancePerRequest();
			//builder.RegisterAssemblyTypes(typeof(CourseFeedbackService).Assembly)
			//    .Where(t => t.Name.EndsWith("Service"))
			//    .AsImplementedInterfaces().InstancePerRequest();
			//builder.RegisterAssemblyTypes(typeof(CourseService).Assembly)
			//    .Where(t => t.Name.EndsWith("Service"))
			//    .AsImplementedInterfaces().InstancePerRequest();
			//builder.RegisterAssemblyTypes(typeof(CourseSliderService).Assembly)
			//    .Where(t => t.Name.EndsWith("Service"))
			//    .AsImplementedInterfaces().InstancePerRequest();
			//builder.RegisterAssemblyTypes(typeof(CourseTeacherService).Assembly)
			//    .Where(t => t.Name.EndsWith("Service"))
			//    .AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(ErrorService).Assembly)
				.Where(t => t.Name.EndsWith("Service"))
				.AsImplementedInterfaces().InstancePerRequest();
			//builder.RegisterAssemblyTypes(typeof(GroupService).Assembly)
			//    .Where(t => t.Name.EndsWith("Service"))
			//    .AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(MenuService).Assembly)
				.Where(t => t.Name.EndsWith("Service"))
				.AsImplementedInterfaces().InstancePerRequest();
			//builder.RegisterAssemblyTypes(typeof(ProductService).Assembly)
			//    .Where(t => t.Name.EndsWith("Service"))
			//    .AsImplementedInterfaces().InstancePerRequest();
			//builder.RegisterAssemblyTypes(typeof(RegistedService).Assembly)
			//    .Where(t => t.Name.EndsWith("Service"))
			//    .AsImplementedInterfaces().InstancePerRequest();
			//builder.RegisterAssemblyTypes(typeof(SliderService).Assembly)
			//    .Where(t => t.Name.EndsWith("Service"))
			//    .AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
				.Where(t => t.Name.EndsWith("Service"))
				.AsImplementedInterfaces().InstancePerRequest();

			Autofac.IContainer container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

			GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container); //Set the WebApi DependencyResolver

		}
	}
}
