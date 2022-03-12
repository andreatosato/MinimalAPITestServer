using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MinimalApiServer.Databases;
using Xunit;

namespace MinimalApiServer.Tests;

public class NoteTests : IClassFixture<MinimalApiApplicationFactory>
{
    private readonly MinimalApiApplicationFactory factory;

    public NoteTests(MinimalApiApplicationFactory factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task Get_Notes()
    {
        var client = factory.CreateClient();
        var notes = await client.GetFromJsonAsync<List<Note>>("/notes");

        Assert.NotNull(notes);
        Assert.Empty(notes);
    }

    [Fact]
    public async Task Create_Notes()
    {
        var date = DateTime.Now;
        var client = factory.CreateClient();
        var noteResponse = await client.PostAsJsonAsync<Note>("/notes", new Note
        {
            Title = "My Test Note",
            Content = "My Content Test Note",
            CreatedOn = date
        });

        var response = await noteResponse.Content.ReadFromJsonAsync<Note>();
        Assert.Equal("My Content Test Note", response!.Content);
        Assert.Equal("My Test Note", response!.Title);
        Assert.Equal(date, response!.CreatedOn);

        Assert.Equal(noteResponse.Headers.Location!.OriginalString, $"/notes/{response!.Id}");
    }

    [Fact]
    public async Task CreateAndGetNotes()
    {
        var date = DateTime.Now;
        var client = factory.CreateClient();
        var noteResponse = await client.PostAsJsonAsync<Note>("/notes", new Note
        {
            Title = "My Test Note",
            Content = "My Content Test Note",
            CreatedOn = date
        });

        var notes = await client.GetFromJsonAsync<List<Note>>("/notes");

        Assert.NotNull(notes);
        Assert.NotEmpty(notes);
        Assert.Collection(notes,
            n =>
            {
                Assert.Equal("My Content Test Note", n!.Content);
                Assert.Equal("My Test Note", n!.Title);
                Assert.Equal(date, n!.CreatedOn);
            });
    }
}
