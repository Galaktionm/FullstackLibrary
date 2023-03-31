using Microsoft.EntityFrameworkCore;
using LibraryApp.API.Data.Entities;
using Microsoft.AspNetCore.Identity;
using LibraryApp.API.Services;
using LibraryApp.API.Application.Serializers;
using LibraryApp.API.Data.Repositories;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using LibraryApp.API.Application;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options=>options.JsonSerializerOptions.Converters.Add(new UserSerializer()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(
    options=>options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
 .AddEntityFrameworkStores<DatabaseContext>();

 builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        RequireExpirationTime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtParameters:Issuer"],
        ValidAudience = builder.Configuration["JwtParameters:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.
        GetBytes(builder.Configuration["JwtParameters:SecurityKey"]))
        //The same constructor we used to generate credentials
        //when creating the JWT
    };
});

builder.Services.AddScoped<UserManagementService>();
builder.Services.AddScoped<JwtHandler>();

builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<CheckoutRepository>();

builder.Services.AddSingleton<DatabaseSeeder>();

builder.Services.AddHttpContextAccessor();


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new MediatorModule()));

builder.Services.AddCors(options=>{
    options.AddPolicy("AngularPolicy", policy=>{
        policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("*");
    });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
    });
}

using(var scope=app.Services.CreateScope()) {
    var dbContext=scope.ServiceProvider.GetService<DatabaseContext>();
    dbContext.Database.Migrate();

    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    await roleManager.CreateAsync(new IdentityRole("User"));
    await roleManager.CreateAsync(new IdentityRole("Admin"));

    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();

    var userInfo=builder.Configuration.GetSection("UserInfo");
    User user=new User(userInfo["Username"], userInfo["Email"]);
    try {
        var result=await userManager.CreateAsync(user, userInfo["Password"]);
        await userManager.AddToRoleAsync(user, "User");
    } catch {
        Debug.WriteLine("Already registered");
    }

    var adminInfo=builder.Configuration.GetSection("AdminInfo");
    User admin=new User(adminInfo["Username"], adminInfo["Email"]);
    try {
        var result2=await userManager.CreateAsync(admin, adminInfo["Password"]);
        await userManager.AddToRoleAsync(admin, "Admin");
    } catch {
         Debug.WriteLine("Already registered");
    }

    var seeder=scope.ServiceProvider.GetService<DatabaseSeeder>();
    seeder.loadData();


}

app.UseCors("AngularPolicy");

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
