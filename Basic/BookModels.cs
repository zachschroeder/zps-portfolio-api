namespace Portfolio.Basic;

public record Book(Guid id, string title, string author);

public record AddBookDto(string title, string author);