var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();

<<<<<<< HEAD
=======
public class ArtigoSindel
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataTermino { get; set; }
}


class SindellDb : DbContext
{
    
    public SindellDb(DbContextOptions options) : base(options) { }
    public DbSet<ArtigoSindel> ArtigosSindels { get; set; }
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
    public const string DatabaseName = "DatabaseCosmo";
    public const string URI = "https://kairoswift.documents.azure.com:443/";
    public const string PrimaryKey = "tWfyWR2STBOY1fLmCtsEv2WfR3qk82f5EdKsBmFN8KknI9vWjfVVuqPy9D7jLEVpFaimjTyaEHWKOxisrrGQ2w==";
    public const string PrimaryConnectionString = "AccountEndpoint=https://kairoswift.documents.azure.com:443/;AccountKey=tWfyWR2STBOY1fLmCtsEv2WfR3qk82f5EdKsBmFN8KknI9vWjfVVuqPy9D7jLEVpFaimjTyaEHWKOxisrrGQ2w==";
    
}
>>>>>>> parent of 6539803 (Update Program.cs)
