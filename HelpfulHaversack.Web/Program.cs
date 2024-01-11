using HelpfulHaversack.Web.Services;
using HelpfulHaversack.Web.Services.IService;
using HelpfulHaversack.Web.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure HTTP Client
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient(); //base client
builder.Services.AddHttpClient<ITreasuryService, TreasuryService>(); //<Interface, Implementation>

// Get Program URL from appsettings.json
string? treasuryServiceUrl = builder.Configuration["ServiceUrls:TreasuryAPI"];
if (treasuryServiceUrl != null)
    StaticDetails.TreasuryApiBase = treasuryServiceUrl;
else
    throw new Exception("Required Service URL was not found.");

// Tell the app where to find the implementations of the interfaces

// Add an ITreasuryService with the implementation TreasuryService
builder.Services.AddScoped<ITreasuryService, TreasuryService>();

// builder.Services.add<lifetime of the service> which is scoped in this instance
builder.Services.AddScoped<IBaseService, BaseService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
