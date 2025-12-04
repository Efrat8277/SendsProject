using SendsProject.Core.Repositories;
using SendsProject.Core.Services;
using SendsProject.Data;
using SendsProject.Data.Repositories;
using SendsProject.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var policy = "policy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policy, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddScoped<IDeliveryPersonRepository,DeliveryPersonRepository>();
builder.Services.AddScoped<IDeliveryPersonService, DeliveryPersonSrvice>();

builder.Services.AddScoped<IRecipientRepository, RecipientRepository>();
builder.Services.AddScoped<IRecipientService, RecipientService>();

builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IPackageService, PackageService>();

builder.Services.AddSingleton<DataContext>();


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

app.UseCors(policy);

app.Run();
