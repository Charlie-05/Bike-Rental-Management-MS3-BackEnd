
using BikeRentalApplication.Database;
using BikeRentalApplication.IRepositories;
using BikeRentalApplication.IServices;
using BikeRentalApplication.Repositories;
using BikeRentalApplication.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BikeRentalApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // builder.Services.AddControllers();
            builder.Services.AddControllers()
         .AddJsonOptions(options =>
         {
             options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
         });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<RentalDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IBikeRepository, BikeRepository>();
            builder.Services.AddScoped<IBikeService, BikeService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IInventoryUnitRepository, InventoryUnitRepository>();
            builder.Services.AddScoped<IInventoryUnitService, InventoryUnitService>();
            builder.Services.AddScoped<IRentalRequestRepository, RentalRequestRepository>();
            builder.Services.AddScoped<IRentalRequestService, RentalRequestService>();
            builder.Services.AddScoped<IRentalRecordRepository, RentalRecordRepository>();
            builder.Services.AddScoped<IRentalRecordService, RentalRecordService>();
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<IBrandService, BrandService>();

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy(
                    name: "CORSPolicy",
                    builder =>
                    {
                        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                    }
                    );
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("CORSPolicy");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
