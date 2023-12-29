using Quartz;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;
using StarWarsProgressBarIssueTracker.Infrastructure.Repositories;
using TaskStatus = StarWarsProgressBarIssueTracker.Infrastructure.Models.TaskStatus;

namespace StarWarsProgressBarIssueTracker.App.Jobs;

public class JobSchedulingService(IJobRepository jobRepository, ITaskRepository taskRepository)
{
    public async Task ScheduleTasksAsync(CancellationToken cancellationToken)
    {
        var jobs = await jobRepository.GetAll(cancellationToken);
        foreach (var job in jobs)
        {
            if (!job.IsPaused && ShouldSchedule(job))
            {
                var task = new DbTask
                {
                    ExecuteAt = DateTime.UtcNow,
                    Job = job,
                    Status = TaskStatus.Planned
                };
                await taskRepository.Add(task, cancellationToken);

                job.NextExecution = DateTime.UtcNow;
                await jobRepository.Update(job, cancellationToken);
            }
        }
    }

    private static bool ShouldSchedule(DbJob job)
    {
        if (job.NextExecution is null)
        {
            return true;
        }

        var cronExpression = new CronExpression(job.CronInterval);
        
        var nextScheduledExecution = cronExpression.GetNextValidTimeAfter(job.NextExecution.Value);
        return nextScheduledExecution!.Value.Date <= DateTime.UtcNow;
    }
}
