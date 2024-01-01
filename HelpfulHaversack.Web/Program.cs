using HelpfulHaversack.Web.Services;
using HelpfulHaversack.Web.Services.IService;
using HelpfulHaversack.Web.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers().AddNewtonsoftJson();

//Configure HTTP Client.
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient(); //base client
builder.Services.AddHttpClient<ITreasuryService, TreasuryService>(); //<Interface, Implementation>

//Get the program URL from appsettings.json
StaticDetails.TreasuryApiBase = builder.Configuration["ServiceUrls:TreasuryAPI"];

//Add custom services to the container.
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ITreasuryService, TreasuryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
