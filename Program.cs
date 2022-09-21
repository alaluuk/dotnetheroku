using university;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.EnvironmentName == "Development") {
   builder.Services.AddTransient(_ => new Database(builder.Configuration.GetConnectionString("DefaultConnection")));
}
else{
    builder.Services.AddTransient(_ => new Database(builder.Configuration.GetConnectionString(Environment.GetEnvironmentVariable("DATABASE_URL"))));
}


var app = builder.Build();

// Configure the HTTP request pipeline.
/* if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} */
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//example about MiddleWare function
app.Use(async (contex, next)=>
{
    Console.WriteLine("Middleware excuted");
    await next();
}
);

app.Run();
