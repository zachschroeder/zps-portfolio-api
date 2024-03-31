namespace Portfolio.Book;

public record Book(Guid id, string title, string author);

public record AddBookDto()
{
    // This record is set up differently in order to use `required` modifier - helpful for strict deserialization
    public required string title { get; init; }
    public required string author { get; init; }
};

public record DeleteBookDto(Guid id);