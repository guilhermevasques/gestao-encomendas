using Sonae.GestaoEncomendas.MVC.Services;
using Sonae.GestaoEncomendas.MVC.Repository;
using Sonae.GestaoEncomendas.MVC.Repository.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IProdutoRepository, ProdutoRepository>();
builder.Services.AddSingleton<IEncomendaRepository, EncomendaRepository>();
builder.Services.AddSingleton<IEncomendaService, EncomendaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestaoEncomendas API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();