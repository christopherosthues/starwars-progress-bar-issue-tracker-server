namespace StarWarsProgressBarIssueTracker.Domain.Configuration;

public class IssuesConnectionConfig
{
    public required string GraphQLUrl { get; set; }
    public required string RestURL { get; set; }
    public required string ProjectPath { get; set; }
    public required string Token { get; set; }
}
