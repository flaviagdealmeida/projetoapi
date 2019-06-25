using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using Projeto.Services.Mappings;
using Swashbuckle.AspNetCore.Swagger;
using Projeto.BLL.Contracts;
using Projeto.BLL.Business;
using Projeto.DAL.Contracts;
using Projeto.DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Projeto.Services
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connectionString = Configuration.GetConnectionString("Aula");

            //mapa para injetar dependencias
            services.AddTransient<IProdutoBusiness, ProdutoBusiness>();
            services.AddTransient<IUsuarioBusiness, UsuarioBusiness>();

            services.AddTransient<
                IProdutoRepository, 
                ProdutoRepository>
                (cfg => new ProdutoRepository(connectionString));

            services.AddTransient<
              IUsuarioRepository,
              UsuarioRepository>
              (cfg => new UsuarioRepository(connectionString));


            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ViewModelToEntityMap>();
                cfg.AddProfile<EntityToViewModelMap>();
            });

            //configuração do JWT
            var signingConfigurations = new LoginConfiguration();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenConfiguration"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme  = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });
            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes
            (JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });


            //configuração do swagger
            services.AddSwaggerGen(
                s =>
                {
                  s.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Projeto Controle de Produtos",
                        Description = "Curso C# WebDeveloper",
                        Version = "v1",
                        Contact = new Contact
                    {
                        Name = "COTI Informática",
                        Email = "contato@cotiinformatica.com.br",
                        Url = "http://www.cotiinformatica.com.br"
                    }
                  });

                    s.OperationFilter<AddHeaderParameter>();
                }
            );
      
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();


            app.UseSwagger();
            app.UseSwaggerUI(
                s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "projeto");
                });
        }
    }
}
