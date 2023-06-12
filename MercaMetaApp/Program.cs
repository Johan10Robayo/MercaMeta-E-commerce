using MercaMetaApp.Data;
using MercaMetaApp.Data.DAO;
using MercaMetaApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", option => {
    option.Cookie.Name = "MyCookieAuth";
    option.LoginPath = "/Site/Login"; //la ruta donde está el login 
    option.AccessDeniedPath = "/Site/AccesoDenegado";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});

//politicas de acceso
/*builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy",
        policy => policy.RequireClaim("Admin"));
    options.AddPolicy("EmpresaPolicy",
        policy => policy.RequireClaim("Empresa"));
    options.AddPolicy("ClientePolicy", 
        policy => policy.RequireClaim("Cliente"));
});*/

builder.Services.AddSingleton<ConexionDb>(
    new ConexionDb(builder.Configuration.GetConnectionString("MercaMetaConnection"))
    );
builder.Services.AddSingleton<ClienteDao>();
builder.Services.AddSingleton<PersonaDao>();
builder.Services.AddSingleton<UsuarioDao>();
builder.Services.AddSingleton<EmpresaDao>();
builder.Services.AddSingleton<ProductoDao>();
builder.Services.AddSingleton<ClienteService>();
builder.Services.AddSingleton<EmpresaService>();
builder.Services.AddSingleton<AdminService>();
builder.Services.AddSingleton<UsuarioService>();
builder.Services.AddSingleton<ProductoService>();



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
