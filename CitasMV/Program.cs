using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de sesión (30 minutos)
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


// // PRUEBA DE CONEXIÓN AQUÍ (ANTES DE builder.Build())
// try
// {
//     var connectionString = builder.Configuration.GetConnectionString("ConexionSQL");

//     using (SqlConnection conn = new SqlConnection(connectionString))
//     {
//         conn.Open();
//         Console.WriteLine("Conexión exitosa a la base de datos.");
//     }
// }
// catch (Exception ex)
// {
//     Console.WriteLine("Error de conexión: " + ex.Message);
// }
// // -----------------------------------------------------------


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();
