using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using BlogApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = "Blog API",
        Description = "Creating an API using ASP.NET core to learn.",
        Contact = new OpenApiContact
        {
            Name = "Ashfaq Hussain",
            Url = new Uri("http://ashfaqhahmed.com")
        }

    });
});

// Db Context
builder.Services.AddDbContext<BlogContext>(opt =>
    opt.UseInMemoryDatabase("Blogs")
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
