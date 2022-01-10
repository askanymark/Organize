using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Organize.Business;
using Organize.Shared.Interfaces;
using Organize.TestFake;
using Organize.WASM;
using Organize.WASM.ItemEdit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddScoped<IUserService, UserService>();

// builder.Services.AddSingleton<IUserManager, UserManager>(); // Registration for dependency injection container
builder.Services.AddScoped<IUserManager, UserManagerFake>();
builder.Services.AddScoped<IUserItemManager, UserItemManager>();
builder.Services.AddScoped<ItemEditService>();

var host = builder.Build();

var userService = host.Services.GetRequiredService<IUserService>();
TestData.CreateTestUser();
userService.currentUser = TestData.testUser;

await host.RunAsync();