using MassTransit;
using Messaging.Consts;
using Messaging.CustomerCredit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(opt =>
{
  // request iþlemlerinde uri de exchange tanýmý kullanýyoruz.
  opt.AddRequestClient<GetCreditScoreRequest>(new Uri($"exchange:{EndpointTypes.GetCreditScoreRequest}"),5000); // 5 saniye eder. 5 saniye boyunca cevap alamazsak timeout düþ.

  opt.UsingRabbitMq((context, config) =>
  {
    config.Host(builder.Configuration.GetConnectionString("RabbitConn"));
    config.ConfigureEndpoints(context);

  });

});



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

app.Run();
