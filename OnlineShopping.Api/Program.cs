var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();

builder.Services.ConfigureSettings(builder.Configuration);

builder.Services.RegisterDbContext(builder.Configuration);

builder.Services.RegisterServices();
builder.Services.RegisterHelpers();

builder.Services.ConfigureAuthentication();
builder.Services.ConfigureSwagger();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.HandleModelStateErrors();

builder.Services.AddNeededCors();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();


// Configure the HTTP request pipeline.

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseMiddleware<CustomUnauthorizedMiddleware>();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<AppDbContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
try
{
    await SeedData.SeedProductsAndCategories(context);
    await SeedData.SeedRolesAndUsers(userManager, roleManager);
}
catch (Exception ex)
{
    //TODO: use serilog to log the errors here
    throw;
}
app.Run();