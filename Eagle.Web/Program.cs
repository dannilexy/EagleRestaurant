using Eagle.Web;
using Eagle.Web.Service;
using Eagle.Web.Service.IService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies", c=> c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
    .AddOpenIdConnect("oidc", d =>
    {
        d.Authority = builder.Configuration.GetValue<string>("ServiceUrls:IdentityAPI");
        d.GetClaimsFromUserInfoEndpoint = true;
        d.ClientId = "eagle";
        d.ClientSecret = "secret";
        d.ResponseType = "code";

        d.TokenValidationParameters.NameClaimType = "name";
        d.TokenValidationParameters.RoleClaimType = "role";
        d.Scope.Add("eagle");

        d.SaveTokens = true;
    });

builder.Services.AddHttpClient<IProductServices, ProductService>();

SD.ProductAPIBase = builder.Configuration.GetValue<string>("ServiceUrls:ProductAPI");
builder.Services.AddScoped<IProductServices,ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
