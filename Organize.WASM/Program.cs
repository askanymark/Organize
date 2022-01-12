using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Organize.Business;
using Organize.DataAccess;
using Organize.IndexedDB;
using Organize.InMemoryStorage;
using Organize.Shared.Interfaces;
using Organize.TestFake;
using Organize.WASM;
using Organize.WASM.ItemEdit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserManager, UserManager>(); // Registration for dependency injection container
// builder.Services.AddScoped<IUserManager, UserManagerFake>();
builder.Services.AddScoped<IUserItemManager, UserItemManager>();
builder.Services.AddScoped<IItemDataAccess, ItemDataAccess>();
builder.Services.AddScoped<IUserDataAccess, UserDataAccess>();
// builder.Services.AddScoped<IPersistenceService, InMemoryStorage>();
builder.Services.AddScoped<IPersistenceService, IndexedDB>();
builder.Services.AddScoped<ItemEditService>();

var host = builder.Build();

var persistenceService = host.Services.GetRequiredService<IPersistenceService>();
await persistenceService.InitAsync();

var userService = host.Services.GetRequiredService<IUserService>();
var itemManager = host.Services.GetRequiredService<IUserItemManager>();

if (persistenceService is InMemoryStorage)
{
    TestData.CreateTestUser(itemManager);
    userService.currentUser = TestData.testUser;
}


await host.RunAsync();