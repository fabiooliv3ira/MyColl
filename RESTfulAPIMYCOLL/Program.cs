using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RESTfulAPIMYCOLL.Data;
using RESTfulAPIMYCOLL.Repositories;
using System.Text;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IEncomendaRepository, EncomendaRepository>();
builder.Services.AddScoped<ISubCategoriasRepository, SubCategoriasRepository>();
builder.Services.AddScoped<IItemCarrinhoRepository, ItemCarrinhoRepository>();
builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();
builder.Services.AddScoped<ITipoUtilizadorRepository, TipoUtilizadorRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

//Registar servicos de autenticacao com JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidAudience = builder.Configuration["JWT:Audience"],
			ValidIssuer = builder.Configuration["JWT:Issuer"],
			IssuerSigningKey =
				new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(
						builder.Configuration["JWT:Key"] ?? throw new InvalidOperationException("JWT:Key configuration value is missing.")
					)
				)
		};
	});

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
	  .AddEntityFrameworkStores<ApplicationDbContext>()
	  .AddDefaultTokenProviders()
	  .AddApiEndpoints();

//Registar servicos adicionais - autenticacao e endpoints da API Identity
builder.Services.AddAuthorization();


var app = builder.Build();


//Mapear uma rota para o endpoint do Identity
app.MapGroup("/identity").MapIdentityApi<ApplicationUser>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Configurar CORS para permitir pedidos da aplicação Blazor

app.UseCors("AllowBlazorApp");

//Adicionar autenticação e autorização ao pipeline de processamento de pedidos

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
