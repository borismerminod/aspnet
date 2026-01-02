var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


//On appelle un endpoint avant le routage ==> il sera toujours null car le routage n'a pas encore été fait
/*app.Use(async (context, next) =>
{
    Endpoint endpoint = context.GetEndpoint();
    await next(context);
});*/


//Middleware permettant de gérer la fonctionnalité de routage ==> va créer un objet appelé endpoint qui va servir dans le match des routes
// Donc cette fonction doit être appelée avant UseEndpoints
app.UseRouting();

//OK on est après le routage - Si on a une route qui matche l'objet endpoint sera instancié - sinon il sera null 
//Pas mal d'appelé ce genre de fonction pour faire du log
app.Use(async (context, next) =>
{
    Endpoint endpoint = context.GetEndpoint();

    if(endpoint != null)
        await context.Response.WriteAsync(endpoint.DisplayName + "\n");
    await next(context);
});


//On peut utiliser ce middleware pour définir pour une route un middleware à appeler en réponse
app.UseEndpoints(endpoint =>
{
    //On défini un middleware à appeler si on reçoit l'url /Home
    //Map est appelé quelque soit le type de protocole HTTP passé
    endpoint.Map("/Home", async context =>
    {
        await context.Response.WriteAsync("You are in homepage");
    });

    endpoint.MapGet("/Products", async context =>
    {
        await context.Response.WriteAsync("You are in product page");
    });

    endpoint.MapPost("/Products", async context =>
    {
        await context.Response.WriteAsync("You are in product page POST HTTP Method used");
    });

    //Utiliser des routes avec des paramètres
    endpoint.MapGet("/products/{id}", async (context) =>
    {
        var id = Convert.ToInt32(context.Request.RouteValues["id"]);
        await context.Response.WriteAsync("This is product with id: " + id + "\n");
    });

    endpoint.MapGet("/book/author/{authorname}/{bookid}", async (context) => //Les paramètres de routes sont insensible à la casse
    {
        var bookId = Convert.ToInt32(context.Request.RouteValues["bookid"]);
        var authorName = Convert.ToString(context.Request.RouteValues["authorname"]);
        await context.Response.WriteAsync("This is product with authorname : "+ authorName+ " id: " + bookId + "\n");
    });

});

//On peut définir ici une réponse par défaut si l'url recherchée n'est pas trouvée
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Page not found");
});

app.Run();
