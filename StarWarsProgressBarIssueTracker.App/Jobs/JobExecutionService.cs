using Polly.Registry;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;
using StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

namespace StarWarsProgressBarIssueTracker.App.Jobs;

public class JobExecutionService(ITaskRepository taskReposity, JobFactory jobFactory,
    ResiliencePipelineProvider<string> pipelineProvider)
{
    public async Task ExecuteTask(JobType jobType, CancellationToken cancellationToken)
    {
        var tasks = await taskReposity.GetScheduledTasksAsync(jobType, cancellationToken);

        foreach (var task in tasks)
        {
            if (task.Status == Infrastructure.Models.TaskStatus.Planned)
            {
                task.Status = Infrastructure.Models.TaskStatus.Running;
                await taskReposity.Update(task, cancellationToken);

                var job = jobFactory.CreateJob(jobType);

                try
                {
                    var pipeline = pipelineProvider.GetPipeline("job-pipeline");

                    await pipeline.ExecuteAsync(async token =>
                    {
                        try
                        {
                            task.Status = Infrastructure.Models.TaskStatus.Running;
                            await taskReposity.Update(task, cancellationToken);

                            await job.ExecuteAsync(cancellationToken);

                            task.Status = Infrastructure.Models.TaskStatus.Completed;
                            task.ExecutedAt = DateTime.UtcNow;
                            await taskReposity.Update(task, cancellationToken);
                        }
                        catch
                        {
                            task.Status = Infrastructure.Models.TaskStatus.FailureWaitingForRetry;
                            await taskReposity.Update(task, cancellationToken);
                        }
                    }, cancellationToken);
                }
                catch
                {
                    task.Status = Infrastructure.Models.TaskStatus.Error;
                    await taskReposity.Update(task, cancellationToken);
                }
            }
        }
    }
}
