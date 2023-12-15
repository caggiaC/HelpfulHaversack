using HelpfulHaversack.Services.ContainerAPI.Data;
using Services.ContainerAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

var _templates = ItemTemplateMasterSet.Instance;
var _treasuries = TreasuryStore.Instance;


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

app.Run();

//static void OnProcessExit(object? sender, EventArgs e)
//{
//    TreasuryStore.Instance.Close();
//    ItemTemplateMasterSet.Instance.Close();
//}
