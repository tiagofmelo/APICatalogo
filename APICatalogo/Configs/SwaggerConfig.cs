using Microsoft.OpenApi.Models;

namespace APICatalogo.Configs
{
    public class SwaggerConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Catálogo", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Header de autorização JWT usando o esquema Bearer.\r\n\r\nInforme 'Bearer'[espaço]'TOKEN'.\r\n\r\nExamplo: \'Bearer 12345abcdef\'",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                   {
                      new OpenApiSecurityScheme
                      {
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                         }
                      },
                      new string[] {}
                   }
                });
            });
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI();
        }
    }
}
