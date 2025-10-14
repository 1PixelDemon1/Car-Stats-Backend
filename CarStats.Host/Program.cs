using CarStats.Auth.Presentation;
using CarStats.Car.Presentation;
using CarStats.User.Presentation;

namespace CarStats.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            new AuthModuleLoader().Load(builder.Services);
            new CarModuleLoader().Load(builder.Services);
            new UserModuleLoader().Load(builder.Services);

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
