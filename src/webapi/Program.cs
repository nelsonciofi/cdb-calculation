using webapi.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.AddCdb();

builder.Services.AddControllers().CustomizeDefaultModelStateValidationResult();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(cfg => cfg.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
