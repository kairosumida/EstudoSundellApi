<<<<<<< Updated upstream
﻿using Microsoft.EntityFrameworkCore;
=======
﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
>>>>>>> Stashed changes
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


<<<<<<< Updated upstream

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<SindellDbContext>();
=======
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<SindellDbContext>(opt => opt.UseCosmos(
        ConfigurationDBCosmo.URI,
        ConfigurationDBCosmo.PrimaryKey,
        ConfigurationDBCosmo.DatabaseName));

>>>>>>> Stashed changes
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();




app.MapGet("/artigos", async (SindellDbContext db) =>

    await db.ArtigosSindels.ToListAsync());

app.MapPost("/artigos", async (ArtigoSindel todo, SindellDbContext db) =>
{
    
    db.ArtigosSindels.Add(todo);
    await db.SaveChangesAsync();
    

    return Results.Created($"/artigos/{todo.Id}", todo);
});

app.MapPut("/artigos/{id}", async (int id, ArtigoSindel inputTodo, SindellDbContext db) =>
{
    var artigo = await db.ArtigosSindels.FindAsync(id);

    if (artigo is null) return Results.NotFound();

    artigo.Nome = inputTodo.Nome;
    artigo.DataInicio = DateTime.Now;
    artigo.DataTermino = inputTodo.DataTermino;
    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.MapDelete("/artigos/{id}", async (int id, SindellDbContext db) =>
{
    if (await db.ArtigosSindels.FindAsync(id) is ArtigoSindel todo)
    {
        db.ArtigosSindels.Remove(todo);
        await db.SaveChangesAsync();
        return Results.Ok(todo);
    }

    return Results.NotFound();
});

app.Run();


public class ArtigoSindel
{
    [JsonProperty(PropertyName = "id")]
    public int Id { get; set; }
<<<<<<< Updated upstream
    public string? Nome { get; set; }
    public DateTime DataInicio { get; set; }
=======
    [JsonProperty(PropertyName = "nome")]
    public string? Nome { get; set; }
    [JsonProperty(PropertyName = "dataInicio")]
    public DateTime DataInicio { get; set; }
    [JsonProperty(PropertyName = "dataTermino")]
>>>>>>> Stashed changes
    public DateTime? DataTermino { get; set; }
}


class SindellDbContext : DbContext
{
<<<<<<< Updated upstream
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseCosmos(
        ConfigurationDBCosmo.URI,
        ConfigurationDBCosmo.PrimaryKey,
        ConfigurationDBCosmo.DatabaseName);

    public SindellDbContext(DbContextOptions options) : base(options) { }
    public DbSet<ArtigoSindel> ArtigosSindels { get; set; }
=======
    public SindellDbContext(DbContextOptions<SindellDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
     .Entity<ArtigoSindel>().ToContainer("Items")
     .Property(e => e.Id)
     .ValueGeneratedOnAdd();
        
    }

    public DbSet<ArtigoSindel> ArtigosSindels => Set<ArtigoSindel>();
>>>>>>> Stashed changes
}
public class PaginationFilter
{
    public int PageNumer { get; set; }
    public int PageSize { get; set; }
    public PaginationFilter()
    {
        this.PageNumer = 1;
        this.PageSize = 10;
    }
    public PaginationFilter(int pageNumber, int pageSize)
    {
        this.PageNumer = pageNumber < 1 ? 1 : pageNumber;
        this.PageSize = pageSize > 10 ? 10 : pageSize;
    }

}
public static class ConfigurationDBCosmo
{
<<<<<<< Updated upstream
    public const string DatabaseName = "SindellDb";
=======
    public const string DatabaseName = "ToDoList";
>>>>>>> Stashed changes
    public const string URI = "https://kairoswift.documents.azure.com:443/";
    public const string PrimaryKey = "tWfyWR2STBOY1fLmCtsEv2WfR3qk82f5EdKsBmFN8KknI9vWjfVVuqPy9D7jLEVpFaimjTyaEHWKOxisrrGQ2w==";
    public const string PrimaryConnectionString = "AccountEndpoint=https://kairoswift.documents.azure.com:443/;AccountKey=tWfyWR2STBOY1fLmCtsEv2WfR3qk82f5EdKsBmFN8KknI9vWjfVVuqPy9D7jLEVpFaimjTyaEHWKOxisrrGQ2w==";

}
