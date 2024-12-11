using CypherTechniques;
using CypherTechniques.EncodeDecode;
using CypherTechniques.EncryptionDecryption;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


//Configuracion CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins(
                "http://localhost:81"
            )
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        }
    ); 
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
); 


app.MapGet("/", () => Results.Json(
    new[] {new Page {Index = "index.html", Uri = "localhost"}})
);


app.MapGet("/invertir-texto", (string plainText) => 
{
    InvertirTexto it = new(plainText);
    return it.InvertirCadena();
});

app.MapGet("/revertir-texto", (string codedText) => 
{
    InvertirTexto it = new(codedText);
    return it.RevertirCadena();
});


app.MapGet("/base64encode", (string plainText) => 
{
    return Base64.Base64Encode(plainText);
});

app.MapGet("/base64decode", (string textBase64) => 
{
    return Base64.Base64Decode(textBase64);
});


app.MapGet("/number-base-encode", (string text, int baseN = 0) => 
{
    if (baseN == 2 || baseN == 8 || baseN == 10 || baseN == 16) {
        NumberBase nb = new()
        {
            StringInput = text
        };
        return nb.EncodeBaseString(baseN);
    } else {
        return "Base númerica (clave) no válida o no indicada";
    }
});

app.MapGet("/number-base-decode", (string text, int baseN) => 
{
    if (baseN == 2 || baseN == 8 || baseN == 10 || baseN == 16) {
        NumberBase nb = new()
        {
            StringInput = text
        };
        return nb.DecodeBaseString(baseN);
    } else {
        return "Base númerica (clave) no válida o no indicada";
    }
});


app.MapGet("/aes-encryption", (string textPlaine, string key) => 
{
    AES ojbAes = new(textPlaine, key);
    return ojbAes.EncryptAES();
});


app.MapGet("/aes-decryption", (byte[] cypherText, string key) => 
{
    AES ojbAes = new(cypherText, key);
    return ojbAes.DecryptAES();
});


app.Run();


