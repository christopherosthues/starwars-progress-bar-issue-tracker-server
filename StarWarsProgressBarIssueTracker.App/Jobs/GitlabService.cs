using Polly.Registry;

namespace StarWarsProgressBarIssueTracker.App.Jobs;

public class GitlabService(ResiliencePipelineProvider<string> pipelineProvider)
{
    public async Task SynchronizeAsync(CancellationToken cancellationToken)
    {
        var pipeline = pipelineProvider.GetPipeline("job-pipeline");

        await pipeline.ExecuteAsync(static async token =>
        {
            await Task.CompletedTask;
        }, cancellationToken);
    }
}
