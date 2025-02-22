using Blazor_CRUD;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazor_CRUD.Services;
using CurrieTechnologies.Razor.SweetAlert2;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5077") });
builder.Services.AddScoped<IAsistenciasService, AsistenciaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddSweetAlert2();
await builder.Build().RunAsync();
