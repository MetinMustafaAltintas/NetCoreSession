using Microsoft.EntityFrameworkCore;
using NetCoreSession.Models.ContextClasses;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<NorthwindContext>(x => x.UseSqlServer().UseLazyLoadingProxies());

builder.Services.AddDistributedMemoryCache();//Eger Session kompleks yapýlarla calýsmak icin Extension metodu eklenme durumuna maruz kalmýssa bu kod projenizdeki hafýzayý dagýtýk sistemde tutarak daha saglýklý bir çevre sunacaktýr...

builder.Services.AddSession(
    x =>
    {
        x.IdleTimeout = TimeSpan.FromMinutes(3); //Kiþinin bos durma süresi eger 1 dakikalýk bir bos durma süresi olursa Session yok olsun...
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
