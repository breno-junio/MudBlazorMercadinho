using MudBlazoMercadinho.Components;
using MudBlazoMercadinho.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<MercadinhoService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var mercadinhoService = scope.ServiceProvider.GetRequiredService<MercadinhoService>();

    // Aguarda a inicialização dos dados de teste de forma assíncrona e segura
    await mercadinhoService.InicializarDadosAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
