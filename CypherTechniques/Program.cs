using Microsoft.AspNetCore.Mvc;
using CypherTechniques;
using CypherTechniques.EncodeDecode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/", () => Results.Json(
    new[] {new Page {Index = "index.html", Uri = "localhost"}})
);


app.MapGet("/base64encode", (string plainText) => 
{
    return Base64.Base64Encode(plainText);
});


app.MapGet("/base64decode", (string textBase64) => 
{
    return Base64.Base64Decode(textBase64);
});


app.MapGet("/number-base-encode", (string text, int baseN) => 
{
    NumberBase nb = new()
    {
        StringInput = text
    };
    return nb.EncodeBaseString(baseN);
});


app.MapGet("/number-base-decode", (string text, int baseN) => 
{
    NumberBase nb = new()
    {
        StringInput = text
    };
    return nb.DecodeBaseString(baseN);    
});


app.Run();


