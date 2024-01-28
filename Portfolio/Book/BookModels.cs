namespace Portfolio.Book;

public record Book(Guid id, string title, string author);

public record AddBookDto(string title, string author);