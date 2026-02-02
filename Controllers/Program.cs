using Controllers.Controllers;

var builder = WebApplication.CreateBuilder(args);

//On peut ajouter les controleur manuellement
//builder.Services.AddTransient<HomeController>();
//Mais le mieux est d'utiliser AddControllers pour que tout controleur soit ajouté automatiquement
builder.Services.AddControllers();

var app = builder.Build();

/*app.UseRouting();
app.UseEndpoints(endpoint =>
{
    //Pour chaque controleur, un endpoint est créé automatiquement (donc pour chaque méthode on a un endopoint avec le nom de la méthode)
    endpoint.MapControllers();
});*/

//Remplace les deux instructions ci-dessus
app.MapControllers();

app.Run();
