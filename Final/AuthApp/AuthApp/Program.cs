using AuthApp.BL;
using AuthApp.Filters;
using AuthApp.Mappings;
using AuthApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MapingProfiles));
builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", config =>
{
    config.Cookie.Name = "User.Login.Cookie";
    config.LoginPath = "/User/Login";
});
builder.Services.AddScoped<UserBL>();
builder.Services.AddScoped<ExceptionFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseMiddleware<RequestLoggerMiddleware>();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
