using Microsoft.EntityFrameworkCore;
using MinimalApiServer.Databases;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<NotesDbContext>(builder.Configuration.GetConnectionString("Notes"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scoped = app.Services!.CreateScope();
    var dbScoped = scoped.ServiceProvider.GetService<NotesDbContext>()!;
    dbScoped.Database.EnsureCreated();
    if (dbScoped.Notes!.Count() == 0)
    {
        dbScoped.Notes!.Add(new Note()
        {
            Title = "StartupNote",
            Content = "Startup content note",
            CreatedOn = DateTime.Now
        });
        dbScoped.SaveChanges();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();


app.MapGet("/notes", async (NotesDbContext notesDbContext) => await notesDbContext.Notes!.ToListAsync());

app.MapGet("/notes/{id}", async (NotesDbContext notesDbContext, int id) =>
{
    return await notesDbContext.Notes!.FindAsync(id) is Note note ?
        Results.Ok(note) :
        Results.NotFound();
});

app.MapPost("/notes", async (NotesDbContext notesDbContext, Note note) =>
{
    await notesDbContext.Notes!.AddAsync(note);
    await notesDbContext.SaveChangesAsync();
    return Results.Created($"/notes/{note.Id}", note);
});


app.Run();

public partial class Program { }
