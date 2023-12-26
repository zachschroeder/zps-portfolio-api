namespace Portfolio.Basic;

public record Book(Guid id, string title, string author);

public record CreateBookDto(string title, string author);