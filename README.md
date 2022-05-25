# EstudoSundellApi

<h1>O que ela faz?</h1>
<p>Ela é apenas um modelo de treinamento onde estou tentando criar uma minimal api que irá integrar no outro app onde realizo os artigos do SwiftbySindel</p>
<p>A ideia é Criar um banco de dados usando Azure Cosmo, adicionar uma estrutura bem simples, que diz o nome do artigo, tempo de inicio, tempo de termino e um id</p>
<h2Nome do artigo</h2>
<p>Para saber o nome do artigo que foi realizado</p>
<h2>Data de inicio</h2>
<p>Apenas para saber quando que iniciei aquele artigo</p>
<h2>Data de termino</h2>
<p>Para saber se demora ou se é dificil</p>
<h2>Id</h2>
<p>No momento ele é uma das coisas mais importante para o funcionamento do outro app, porque tem um switch q verifica o numero do Id e direciona a view</p>
<h2>NomeDoMetodo</h2>
<p>O plano é ter um nome que vai chamar o método dentro da aplicação, porem isso seria algo futuro. A ideia é que dentro do arquivo JSon tenha uma string, e essa String deve chamar um metodo na outra aplicação com isso
cairia o uso do switch case com o Id</p>

<h1>Conhecimentos</h1>
<h2>Core da aplicação</h2>
<p>1 - Alguns comandos para a aplicação começar a funcionar com o entity</p>

```CSharp
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SindellDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();
```

<h2> Desenvolvendo as tabelas do bd</h2>
<p> 1 - Crie uma classe</p>
<p> 2 - Adicione os campos como public</p>
<h2> Desenvolvendo o BD </h2>
<p> 1 - Crie uma classe que herda DbContext</p>
<p> 2 - Coloque o construtor </p>

```C#
    public SindellDb(DbContextOptions<SindellDb> options)
        : base(options) { }
```

<p> 3 - Adicione a linha que adiciona a tabela</p>

```C#
public DbSet<ArtigoSindel> ArtigosSindels => Set<ArtigoSindel>();
```

<h2> Dando Get</h2>

```C#
app.MapGet("/artigos", async (SindellDb db) =>
    await db.ArtigosSindels.ToListAsync());
```

<p> Repare que dentro de MapGet, temos o primeiro parametro que indica o que deve vir no endereço depois do nome do endereço.
  Porem depois vem uma delegação que é a espera de uma função que deve fazer algo no DB</p>
  
<h2> Dando Post</h2>

```C#
app.MapPost("/artigos", async (ArtigoSindel todo, SindellDb db) =>
{
    db.ArtigosSindels.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/artigos/{todo.Id}", todo);
});
```

<p> Novamente temos a mesma estrutura do get. O q muda é que agora usamos a função Post. E agora temos 2 parametros na função que aguarda
  uma é o ArigoSindel todo. Que vai receber um ArtigoSindel que na verdade é um JSon que se converte para ArtigoSindel. Esse Json é passado no body da requisicao</p>
 
<h2> Dando Put</h2>

```C#
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
```

<p> Repare que agora temos tambem um id. Esse é o Id que identifica o artigo. Depois de localizado é só modificar, com os dados de inputTodo, que tambem vem do body.</p>

<h2>Delete</h2>

```C#
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
```

<p> Quase igual o put, porem mais simples, por que so precisa do Id e pronto</p>
  
<h2>Run</h2>
<p> Agora que está tudo definido, basda dar app.Run()</p>

<h2> Paginação</h2>
<p>É possivel criar a paginação apenas passando 2 parametros no endereço, o int pageNumber e o int pageSize</p>
<p>Depois é só trabalhar com o BD para filtrar as informações</p>

```C#
app.MapGet("/artigos_by_page",
    async (int pageNumber, int pageSize, SindellDb db) =>

        await db.ArtigosSindels.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync()
    );
```

<p>Note que usei um nome diferente artigos_by_page, por algum motivo quando usado o artigos ele não funciona</p>

<h2> Paginação com Query</h2>
<p>Talvez não seja a melhor solução, mas o que eu descobri foi que, como podemos passar parâmetros dentro do endereço. Podemos enviar uma variavel string. Por exemplo: query=nome. Meu filtro irá ordenar por nome. E então retornar uma lista ordenada. Então depois coloco uma paginação.</p>

```C#
app.MapGet("/artigos_by_queryPage",
    async (int pageNumber, int pageSize, string query, SindellDb db) =>
    {

        if (query == "nome")
        {
            return await db.ArtigosSindels.OrderBy(x => x.Nome).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        }
        return await db.ArtigosSindels.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

    }
   );
```
