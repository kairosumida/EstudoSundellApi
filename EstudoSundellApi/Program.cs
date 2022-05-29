
using EstudoSundellApi.Data;
using EstudoSundellApi.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<SindellContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/artigos", async (SindellContext db) =>

    await db.Families.ToListAsync());

app.MapPost("/artigos", async (Family todo, SindellContext db) =>
{
    todo = new Family
    {
        Id = "Andersen.3",
        PartitionKey = "Andersen",
        LastName = "Andersen",
        Parents = new List<Parent>()
                {
                    new Parent { FirstName = "Thomas" },
                    new Parent { FirstName = "Mary Kay" }
                },
        Children = new List<Child>()
                {
                    new Child
                    {
                        FirstName = "Henriette Thaulow",
                        Gender = "female",
                        Grade = 5,
                        Pets = new List<Pet>()
                        {
                            new Pet { GivenName = "Fluffy" }
                        }
                    }
                },
        Address = new Address { State = "WA", County = "King", City = "Seattle" },
        IsRegistered = false
    };
    db.Families.Add(todo);
    await db.SaveChangesAsync();
    

    return Results.Created($"/artigos/{todo.Id}", todo);
});

app.MapPut("/artigos/{id}", async (int id, Family inputTodo, SindellContext db) =>
{
    var artigo = await db.Families.FindAsync(id);

    if (artigo is null) return Results.NotFound();

    
    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.MapDelete("/artigos/{id}", async (int id, SindellContext db) =>
{
    if (await db.Families.FindAsync(id) is Family todo)
    {
        db.Families.Remove(todo);
        await db.SaveChangesAsync();
        return Results.Ok(todo);
    }

    return Results.NotFound();
});

app.Run();

