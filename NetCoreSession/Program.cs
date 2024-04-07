using Microsoft.EntityFrameworkCore;
using NetCoreSession.Models.ContextClasses;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<NorthwindContext>(x => x.UseSqlServer().UseLazyLoadingProxies());

builder.Services.AddDistributedMemoryCache();//Eger Session kompleks yap�larla cal�smak icin Extension metodu eklenme durumuna maruz kalm�ssa bu kod projenizdeki haf�zay� dag�t�k sistemde tutarak daha sagl�kl� bir �evre sunacakt�r...

builder.Services.AddSession(
    x =>
    {
        x.IdleTimeout = TimeSpan.FromMinutes(3); //Ki�inin bos durma s�resi eger 1 dakikal�k bir bos durma s�resi olursa Session yok olsun...
        x.Cookie.HttpOnly = true;
        x.Cookie.IsEssential = true;
    });

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Category}/{action=GetCategories}/{id?}");

app.Run();
