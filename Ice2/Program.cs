using Ice2.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//add blob and table storage services
builder.Services.AddSingleton(new BlobStorage(builder.Configuration.GetConnectionString("AzureBlobStorage"), "user-profilepic"));
builder.Services.AddSingleton(new TableStorage(builder.Configuration.GetConnectionString("AzureTableStorage"), "user-data"));

builder.Configuration.GetConnectionString("AzureBlobStorage");
builder.Configuration.GetConnectionString("AzureTableStorage");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();  // Use HSTS in production.
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
