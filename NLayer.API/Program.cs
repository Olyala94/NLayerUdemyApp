using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.API.Filters;
using NLayer.API.Middlewares;
using NLayer.API.Modules;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWork;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using NLayer.Service.Validations;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers(options =>
        options.Filters
            .Add(new ValidateFilterAttribute()))
            .AddFluentValidation(x => x
            .RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());


        //Default olarak bu Filter aktiv oldugu için bunu buraya yazmamýz lazým ki bizim yazdýðýmýz Validatimiz çalýþsýn (Ama MVC tarafýnda bunun aktin olma tarafý yok)
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //MemoryCach'i Aktif ettik
        builder.Services.AddMemoryCache();

        //Generik olduðu için typeof ile içeriye girdik
        builder.Services.AddScoped(typeof(NotfoundFilter<>));
        builder.Services.AddAutoMapper(typeof(MapProfile));

        //Configuration
        builder.Services.AddDbContext<AppDbContext>(x =>
        {
            x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
            {
                option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
            });
        });

        builder.Host.UseServiceProviderFactory
            (new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(containerbuilder => containerbuilder.RegisterModule(new RepoServiceModul()));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UserCutomerException();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}