using Blog_website.BAL.IServices;
using Blog_website.BAL.Services;
using Blog_website.DAL.DBContext;
using Blog_website.DAL.Entity;
using Blog_website.Helper;
using Blog_website.Repo.IRepository;
using Blog_website.Repo.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbtContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Add services to the container.
//builder.Services.AddScoped<UserService>();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // Ignore reference loops to avoid serialization issues with cyclic references.
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<IUserServices, UserService>();
//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<,>));



builder.Services.AddScoped<IRepository<User>, UserRepository>(); // Register UserRepository
builder.Services.AddScoped<IRepository<Post>, PostRepository>();
builder.Services.AddScoped<IPostServices, PostService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.WithOrigins("http://localhost:4200"); // Allow only this specific origin
    options.AllowAnyMethod();
});


app.UseAuthorization();

app.MapControllers();

app.Run();
