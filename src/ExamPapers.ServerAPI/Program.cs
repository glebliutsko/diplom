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
builder.Services.AddScoped<TokenDataAccesser>();

builder.Services.AddScoped<IAuthenticationServices, AuthenticationServices>();

builder.Services.AddControllers();

#if DEBUG
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var basePath = AppContext.BaseDirectory;

    var xmlFile = new FileInfo(Path.Combine(basePath, "ExamPapers.ServerAPI.xml"));
    if (xmlFile.Exists)
        options.IncludeXmlComments(xmlFile.FullName);
});
#endif

var app = builder.Build();

#if DEBUG
app.UseSwagger();
app.UseSwaggerUI();
#endif

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();