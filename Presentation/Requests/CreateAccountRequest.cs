namespace Lab5.Presentation.Http.Requests;

public sealed record CreateAccountRequest(Guid SessionId, string Pin);