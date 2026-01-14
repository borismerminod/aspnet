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
    /*//On défini un middleware à appeler si on reçoit l'url /Home
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
    });*/

    //Utiliser des routes avec des paramètres
    //Les paramètres peuvent être optionnels 
    //Les paramètres peuvent être typés
    //L'ensemble des contraintes utilisables : https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-10.0#route-constraints
    //endpoint.MapGet("/products/{id:int:min(10):max(20)}", async (context) =>
    endpoint.MapGet("/products/{id:int:range(10,20)}", async (context) =>
    {
        var id = context.Request.RouteValues["id"];
        if(id != null)
        {
            //id = Convert.ToInt32(id);
            await context.Response.WriteAsync("This is product with id: " + id + "\n");
        }
        else
        {
            await context.Response.WriteAsync("You are in products page");
        }
    });

    //Les paramètres peuvent avoir des valeurs par défaut
    //endpoint.MapGet("/book/author/{authorname=john-doe}/{bookid?}", async (context) => //Les paramètres de routes sont insensible à la casse
    //endpoint.MapGet("/book/author/{authorname:alpha:minlength(4):maxlength(16)}/{bookid?}", async (context)=>
    //endpoint.MapGet("/book/author/{authorname:alpha:length(4,16)}/{bookid?}", async (context) => 
    endpoint.MapGet("/book/author/{authorname:alpha:length(5)}/{bookid?}", async (context) => 
    {
        var bookId = context.Request.RouteValues["bookid"];
        var authorName = Convert.ToString(context.Request.RouteValues["authorname"]);

        if(bookId != null)
        {
            bookId = Convert.ToInt32(bookId);
            await context.Response.WriteAsync("This is product with authorname : " + authorName + " id: " + bookId + "\n");
        }
        else
        {
            await context.Response.WriteAsync("This is products with authorname : " + authorName + "\n");
        }

    });

    //ATTENTION : le mieux est d'éviter d'utiliser des expressions régulière dans les routes mais plutôt dans le code du middleware 
    endpoint.MapGet("/quaterly-reports/{year:int:min(1999):minlength(4)}/{month:regex(^(mar|jun|sep|dec)$)}", async (context) =>
    {
        int year = Convert.ToInt32(context.Request.RouteValues["year"]);
        string? month = Convert.ToString(context.Request.RouteValues["month"]);
        await context.Response.WriteAsync($"This is the quaterly report for {year}-{month}");
    });

    endpoint.MapGet("/monthly-reports/{month:regex(^([1-9]|1[012])$)}", async (context) =>
    {
        int monthNumber = Convert.ToInt32(context.Request.RouteValues["month"]);
        await context.Response.WriteAsync($"This is the monthly report for month number {monthNumber}");
    });

    endpoint.MapGet("/daily-reports/{date:regex((19|20)\\d{{2}}-(0[1-9]|1[0-2])-(0[1-9]|[12]\\d|3[01]))}", async (context) =>
    {
        string date = Convert.ToString(context.Request.RouteValues["date"]);
        await context.Response.WriteAsync($"This is the monthly report for month number {date}");
    });

});

//On peut définir ici une réponse par défaut si l'url recherchée n'est pas trouvée
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Page not found");
});

app.Run();
