using api.Modules.Chat;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddSingleton<ChatService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Vue", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();
app.UseCors("Vue");
app.MapControllers();
app.MapHub<ChatHub>("/hubs/chat");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Run();
