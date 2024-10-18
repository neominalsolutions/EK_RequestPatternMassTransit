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
  // Request bazl� oldu�undan endpoint tan�mlamas� yap�yorum. Endpoint ismi veriyoruz.
  opt.AddConsumer<CreditScoreConsumer>().Endpoint(x=> x.Name = EndpointTypes.GetCreditScoreRequest);

  opt.UsingRabbitMq((context, config) =>
  {
    // b�t�n istekleri git rabbitmq i�in konfig�re et.
    config.Host(builder.Configuration.GetConnectionString("RabbitConn")); // ba�lant� ayar�
    config.ConfigureEndpoints(context);

    // Not: event g�d�ml� bir geli�tirme yaparken configure consumer �zerinden ilerledik. fakat request bazl� bir i�lem yaparken endpoint kavram�n� kullan�yoruz.

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
