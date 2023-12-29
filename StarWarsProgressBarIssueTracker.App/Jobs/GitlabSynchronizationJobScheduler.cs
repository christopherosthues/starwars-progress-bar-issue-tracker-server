using Quartz;

namespace StarWarsProgressBarIssueTracker.App.Jobs;

public class GitlabSynchronizationJobScheduler : Quartz.IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await Task.CompletedTask;
    }
}
