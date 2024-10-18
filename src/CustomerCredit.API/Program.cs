using CustomerCredit.API.Consumers;
using MassTransit;
using Messaging.Consts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(opt =>
{
  // Request bazlý olduðundan endpoint tanýmlamasý yapýyorum. Endpoint ismi veriyoruz.
  opt.AddConsumer<CreditScoreConsumer>().Endpoint(x=> x.Name = EndpointTypes.GetCreditScoreRequest);

  opt.UsingRabbitMq((context, config) =>
  {
    // bütün istekleri git rabbitmq için konfigüre et.
    config.Host(builder.Configuration.GetConnectionString("RabbitConn")); // baðlantý ayarý
    config.ConfigureEndpoints(context);

    // Not: event güdümlü bir geliþtirme yaparken configure consumer üzerinden ilerledik. fakat request bazlý bir iþlem yaparken endpoint kavramýný kullanýyoruz.

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
