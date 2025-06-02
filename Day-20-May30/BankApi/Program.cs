using BankApi.Contexts;
using BankApi.Interfaces;
using BankApi.Models;
using BankApi.Repositories;
using BankApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    opts.JsonSerializerOptions.WriteIndented = true;
                });

builder.Services.AddDbContext<BankingDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IRepository<int, Account>, AccountRepository>();
builder.Services.AddTransient<IRepository<int, TransactionEntry>, TransactionRepository>();

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();


builder.Services.AddTransient<ITransferAmountService, TransferAmountService>();
#pragma warning disable SKEXP0070
builder.Services.AddOllamaChatCompletion(
    modelId: "llama3.2:3b",
    endpoint: new Uri("http://localhost:11434")
);

builder.Services.AddTransient((serviceProvider)=> {
    return new Kernel(serviceProvider);
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
