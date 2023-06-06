using ExamPapers.API.Server.Authentication;
using ExamPapers.API.Server.DataAccess;
using ExamPapers.API.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ExamPapersDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("connection");
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<UserDataAccesser>();
builder.Services.AddScoped<TokenDataAccesser>();

builder.Services.AddScoped<IAuthenticationServices, AuthenticationServices>();

builder.Services.AddAuthentication(TokenAuthOptions.DefaultSchemeName)
    .AddScheme<TokenAuthOptions, TokenAuthenticationHandler>(TokenAuthOptions.DefaultSchemeName, options =>
    {

    });
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});
builder.Services.AddControllers();

#if DEBUG
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var basePath = AppContext.BaseDirectory;

    var xmlFile = new FileInfo(Path.Combine(basePath, "ExamPapers.API.Server.xml"));
    if (xmlFile.Exists)
        options.IncludeXmlComments(xmlFile.FullName);
    
    options.AddSecurityDefinition("Token", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Token"
                }
            },
            new string[]{}
        }
    });
});
#endif

var app = builder.Build();

#if DEBUG
app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
});
#endif

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();