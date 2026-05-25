namespace BestAuth.Domain.Requests
{
    public record LoginRequest
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }

    public record RegisterRequest
    {
        public required string UserName { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
