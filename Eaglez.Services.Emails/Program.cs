using Eaglez.Services.Emails.Data;
using Eaglez.Services.Emails.Extensions;
using Eaglez.Services.Emails.Messaging;
using Eaglez.Services.Emails.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));



// Add services to the container.
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IAzureMessageBusConsumer, AzureServiceBusConsumer>();
//builder.Services.AddSingleton<IMessageBus, AzureServiceBusMessageBus>();

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
app.UseAzureServiceBusConsumer();

app.MapControllers();

app.Run();
