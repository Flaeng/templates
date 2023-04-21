using CSharp.GraphQL;

using HotChocolate.AspNetCore;

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDomainLayer(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration, true);

builder.Services
    .AddGraphQLServer()
    .AddType<TodoListType>()
    .AddType<TodoItemType>()
    .AddMutationType<Mutation>()
    .AddQueryType<Query>()

    // .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

// Configure the HTTP request pipeline.
// app.UseGraphQL();
app.MapGraphQL();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UsePlayground();
    // app.UseVoyager();
}

app.UseHttpsRedirection();
// app.Use(async (ctx, next) =>
// {
//     await Task.Yield();
//     var path = ctx.Request.Path.ToString().TrimStart('/');
//     if (String.IsNullOrWhiteSpace(path))
//         ctx.Response.Redirect("/graphql");
//     else
//         next?.Invoke(ctx);
// });

app.UseAuthorization();

app.Run();