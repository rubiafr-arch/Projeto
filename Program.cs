var builder = WebApplication.CreateBuilder(args);

// Adiciona CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("https://brunotrbr.github.io")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Adiciona Entity Framework e configura Serviços
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Configurações para Controllers e Serialização JSON
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

var app = builder.Build();

// Usa o Middleware customizado
app.UseMiddleware<RequestLoggingMiddleware>();

// Configura CORS
app.UseCors("FrontendPolicy");

app.UseAuthorization();
app.MapControllers();

app.Run();