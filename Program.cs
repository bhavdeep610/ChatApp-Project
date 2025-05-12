using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseMySql(builder.Configuration.GetConnectionString("ChatAppConnection")
    ,ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ChatAppConnection"))));

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var Context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    Context.Database.Migrate();

}
if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

    app.MapGet("/", () => "Hello World!");

app.Run();
