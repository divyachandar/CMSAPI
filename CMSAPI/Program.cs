using CMSAPI.Data;
using CMSAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<CampusAdmissionContext>();

// register student service
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Make sure CORS middleware runs before routing to controllers
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
