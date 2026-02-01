
//On peut changer le répertoire permettant de servir les fichiers 
using Microsoft.Extensions.FileProviders;

//On peut en ajouter un via le builder
var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    WebRootPath = "staticfiles"
});
var app = builder.Build();

//wwwroot contient les fichiers statiques comme les images, CSS, JavaScript, que l'on souhaite pouvoir servir directement aux clients web.
app.UseStaticFiles();

//On peut en ajouter un via le middleware UseStaticFiles
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Path.Combine(builder.Environment.ContentRootPath, "mywebroot"))
    )
}); 


app.MapGet("/", () => "Hello World!");


app.Run();
