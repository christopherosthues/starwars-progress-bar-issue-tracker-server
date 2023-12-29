using Quartz;

namespace StarWarsProgressBarIssueTracker.App.Jobs;

public class JobScheduler(JobSchedulingService jobSchedulingService) : Quartz.IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            await jobSchedulingService.ScheduleTasksAsync(context.CancellationToken);
        }
        catch (Exception)
        {
#warning TODO: log exception            
        }
    }
}
