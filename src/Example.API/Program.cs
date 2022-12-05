using Example.Application.CidadeService.Service;
using Example.Application.ExampleService.Service;
using Example.Infra.Data;
using Example.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IExampleService, ExampleService>();
builder.Services.AddScoped<IPessoaService, PessoaService>();
builder.Services.AddScoped<ICidadeService, CidadeService>();

builder.Services.AddDbContext<ExampleContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<PessoaContext>(p => p.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<CidadeContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ExampleContext>();
    dataContext.Database.Migrate();
}

using (var scopeP = app.Services.CreateScope())
{
    var dataContextP = scopeP.ServiceProvider.GetRequiredService<PessoaContext>();

    dataContextP.Database.Migrate();
}

using (var scopeC = app.Services.CreateScope())
{
    var dataContextC = scopeC.ServiceProvider.GetRequiredService<CidadeContext>();
    dataContextC.Database.Migrate();
}



app.UseAuthorization();

app.MapControllers();

app.Run();

