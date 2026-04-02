namespace Lab5.Presentation.Http.Requests;

public sealed record AmountSessionRequest(Guid SessionId, decimal Amount);