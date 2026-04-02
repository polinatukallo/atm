namespace Lab5.Presentation.Http.Requests;

public sealed record CreateUserSessionRequest(long AccountId, string Pin);