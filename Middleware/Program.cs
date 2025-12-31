
using Middleware.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);

//Permet l'utilisation de mon middleware en utilisant l'injection de dépendance
builder.Services.AddTransient<MyMiddleware>();


var app = builder.Build();


//Utilisation d'un middleware unique qui ne peut pas en chaîner d'autres
/*app.Run( async (HttpContext context) =>
{
    await context.Response.WriteAsync("Welcome from ASP.NET Core APP !");
});*/


//Avec Use j'ai la méthode next passée en paramètre je peux chaîner vers le prochain middleware
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Welcome from ASP.NET Core APP !");
    await next(context);
});


// Mon second middleware
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("\n\n");
    await next(context);
});

// Mon troisième middleware qui utilise une classe dédiée
//app.UseMiddleware<MyMiddleware>();

//En utilisant une méthode d'extension on peut simplifier l'appel de notre middleware
app.MyMiddleware();

app.UseAnotherCustomMiddleware();

//Appel un middleware si la condition définie dans la fonction du premier élément est remplie sinon on passe au middleware suivant
//Pour l'avoir il faut taper dans l'URL : http://localhost:5055/?IsAuthorized=true
app.UseWhen((context) => context.Request.Query.ContainsKey("IsAuthorized") && context.Request.Query["IsAuthorized"] == "true", 
    app =>
    {
        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Conditionnal Middlewate called\n");
            next(context);
        });
    }
);

//Mon quatrième middleware est ici et sera appelé automatiquement à la suite de celui appelé dans mon Use
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("This is my first ASP.NET Core APP !");
});

app.Run();
