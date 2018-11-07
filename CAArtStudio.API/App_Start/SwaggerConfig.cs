using System.Web.Http;
using WebActivatorEx;
using CAArtStudio.API;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace CAArtStudio.API
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>{  c.SingleApiVersion("v1", "CAArtStudio.API");})
                .EnableSwaggerUi();
        }
    }
}
