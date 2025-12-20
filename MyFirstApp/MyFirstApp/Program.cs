
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*app.MapGet("/", (HttpContext context) => {

    /*string path = context.Request.Path;
    string method = context.Request.Method;*/

    /*var UserAgent = "";
    if (context.Request.Headers.ContainsKey("User-Agent"))
    {
        UserAgent = context.Request.Headers["User-Agent"];
    }*/

/*

    context.Response.StatusCode = 200;

    //return "Request path "+path + "\n Http Method : "+method;
    //return "User Agent " + UserAgent;
    context.Response.Headers["Content-Type"] = "text/html";
    context.Response.Headers["MyHeader"] = "Hello world";
    return "<h2>This is a text response</h2>";
   });*/

//Appelée pour chaque appelle vers une route url
app.Run( async (HttpContext context) =>
{
    string path = context.Request.Path;
    string method = context.Request.Method;


    if(path == "/" || path == "/Home")
    {
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("You are in homepage"); 

    }
    else if(path == "/Contact")
    {
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("You are in Contact page");
    }
    else if (method == "GET" &&  path == "/Product")
    {
        if (context.Request.Query.ContainsKey("id") && context.Request.Query.ContainsKey("name"))
        {
            string id = "";
            string name = "";
            id = context.Request.Query["id"];
            name = context.Request.Query["name"];

            context.Response.StatusCode = 200;
            await context.Response.WriteAsync("You selected the product with ID "+id + " and name "+name);
            return;
        }
        

        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("You are in Product page ");
    }
    else if (method == "POST" && path == "/Product")
    {
        StreamReader reader = new StreamReader(context.Request.Body);
        string data = await reader.ReadToEndAsync();
        string id = "";
        string name = "";
        Dictionary<string, StringValues> dict = QueryHelpers.ParseQuery(data);

        if(dict.ContainsKey("id"))
        {
            id = dict["id"];
        }
        if(dict.ContainsKey("name"))
        {
            name = dict["name"][0];
        }

        await context.Response.WriteAsync("Request body contains "+data + " id "+id + " name "+name);

    }
    else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("The page you are looking for was not found");
    }

});

app.Run();
