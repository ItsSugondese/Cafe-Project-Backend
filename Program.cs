using BisleriumCafeBackend.Exceptions;
using BisleriumCafeBackend.Repository.AddInRepo;
using BisleriumCafeBackend.Repository.CoffeeRepo;
using BisleriumCafeBackend.Repository.MemberRepo;
using BisleriumCafeBackend.Repository.TemporaryAttachmentsRepo;
using BisleriumCafeBackend.Repository.TransactionRepo;
using BisleriumCafeBackend.Services.AddInServices;
using BisleriumCafeBackend.Services.CoffeeServices;
using BisleriumCafeBackend.Services.Login;
using BisleriumCafeBackend.Services.MemberServices;
using BisleriumCafeBackend.Services.TemporaryAttachmentsServices;
using BisleriumCafeBackend.Services.TransactionService;
using BisleriumCafeBackend.Services.Users;
using OfficeOpenXml;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAddInService, AddInServiceImpl>();
builder.Services.AddScoped<ICoffeeService, CoffeeServiceImpl>();
builder.Services.AddScoped<IMemberService, MemberServiceImpl>();
builder.Services.AddScoped<IOrderService, OrderServiceImpl>();
builder.Services.AddScoped<ITransactionService, TransactionServiceImpl>();
builder.Services.AddScoped<IOrderAddInMappingService, OrderAddInMappingServiceImpl>();
builder.Services.AddScoped<ITemporaryAttachmentsService, TemporaryAttachmentsServiceImpl>();

//Add repo to the container
builder.Services.AddScoped<IAddInRepo, AddInRepoImpl>();
builder.Services.AddScoped<ICoffeeRepo, CoffeeRepoImpl>();
builder.Services.AddScoped<IMemberRepo, MemberRepoImpl>();
builder.Services.AddScoped<IOrderRepo, OrderRepoImpl>();
builder.Services.AddScoped<ITransactionRepo, TransactionRepoImpl>();
builder.Services.AddScoped<IOrderAddInMappingRepo, OrderAddInMappingRepoImpl>();
builder.Services.AddScoped<ITemporaryAttachmentsRepo, TemporaryAttachmentsRepoImpl>();
//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(CustomExceptionFilterAttribute));
});


var app = builder.Build();

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
