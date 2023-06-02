using ExamPapers.ServerAPI.DataAccess;
using ExamPapers.ServerAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ExamPapersDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("connection");
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<UserDataAccesser>();

builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();