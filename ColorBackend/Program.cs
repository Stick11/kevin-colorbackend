using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        var configuration = builder.Configuration;

        var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("VE_JWT_SECRET_KEY"));
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Environment.GetEnvironmentVariable("VE_JWT_ISSUER_TOKEN"),
                    ValidAudience = Environment.GetEnvironmentVariable("VE_JWT_AUDIENCE_TOKEN"),
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        builder.Services.AddEndpointsApiExplorer();
        //string environment = Environment.GetEnvironmentVariable("NOMBRE_AMBIENTE");
        string environment = "Prod";
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "COLOR BACKEND SERVICE", Version = "v0.0.1" });
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });


        builder.Services.AddCors(options =>
        {

            options.AddPolicy("Prod",
                builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Access-Control-Allow-Origin");

                });
        });

        var app = builder.Build();

        app.UseCors("Prod");

        app.UseSwagger();

        app.UseDeveloperExceptionPage();

        app.UseSwaggerUI(c => { });

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}