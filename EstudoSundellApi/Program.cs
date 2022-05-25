﻿//https://localhost:7017/todoitems?pageNumber=1&pageSize=1
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SindellDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/artigos", async (SindellDb db) =>
    await db.ArtigosSindels.ToListAsync());

app.MapGet("/artigos/{id}", async (int id, SindellDb db) =>
    await db.ArtigosSindels.FindAsync(id)
        is ArtigoSindel todo
            ? Results.Ok(todo)
            : Results.NotFound());
app.MapGet("/artigos_by_page",
    async (int pageNumber, int pageSize, SindellDb db) =>

        await db.ArtigosSindels.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync()

  ).Produces<List<ArtigoSindel>>(StatusCodes.Status200OK)
  .WithName("GetArtigosByPage").WithTags("Getters");
app.MapPost("/artigos", async (ArtigoSindel todo, SindellDb db) =>
{
    db.ArtigosSindels.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/artigos/{todo.Id}", todo);
});

app.MapPut("/artigos/{id}", async (int id, ArtigoSindel inputTodo, SindellDb db) =>
{
    var artigo = await db.ArtigosSindels.FindAsync(id);

    if (artigo is null) return Results.NotFound();

    artigo.Nome = inputTodo.Nome;
    artigo.DataInicio = DateTime.Now;
    artigo.DataTermino = inputTodo.DataTermino;
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/artigos/{id}", async (int id, SindellDb db) =>
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
    public int Id { get; set; }
    public string? Nome { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataTermino { get; set; }
}


class SindellDb : DbContext
{
    public SindellDb(DbContextOptions<SindellDb> options)
        : base(options) { }

    public DbSet<ArtigoSindel> ArtigosSindels => Set<ArtigoSindel>();
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