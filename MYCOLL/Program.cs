using MYCOLL.Components;
using MYCOLL.Components.Account;
using MYCOLL.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container - SINGLE registration with hub options
// At the top with other services (around line 10)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Remove the AddSignalR() call and use this instead:
builder.Services.Configure<Microsoft.AspNetCore.Components.Server.CircuitOptions>(options =>
{
    options.DetailedErrors = true;
    options.MaxBufferedUnacknowledgedRenderBatches = 20;
    options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(1);
    options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
});

builder.Services.Configure<Microsoft.AspNetCore.SignalR.HubOptions>(options =>
{
    options.MaximumReceiveMessageSize = 10 * 1024 * 1024; // 10MB
    options.ClientTimeoutInterval = TimeSpan.FromMinutes(1);
    options.HandshakeTimeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

// Session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddScoped<MYCOLL.Services.TokenStorageService>();
builder.Services.AddScoped<MYCOLL.Services.AuthenticacaoService>();
builder.Services.AddTransient<MYCOLL.Services.JwtAuthenticationHandler>();

builder.Services.AddHttpClient("api", client =>
{
    client.BaseAddress = new Uri("https://7c17320m-7077.uks1.devtunnels.ms");
    client.DefaultRequestHeaders.Accept.Add(
        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
})
.AddHttpMessageHandler<MYCOLL.Services.JwtAuthenticationHandler>();

// Register business services
builder.Services.AddScoped<MYCOLL.Services.CategoriaService>();
builder.Services.AddScoped<MYCOLL.Services.SubCategoriaService>();
builder.Services.AddScoped<MYCOLL.Services.ProdutoService>();

var app = builder.Build();

// Removed duplicate IsDevelopment block
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await Inicializacao.CriaDadosIniciais(userManager, roleManager);
    }
    catch (Exception)
    {
        throw;
    }
}

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

app.Run();