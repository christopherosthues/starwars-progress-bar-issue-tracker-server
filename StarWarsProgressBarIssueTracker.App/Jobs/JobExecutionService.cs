using Polly.Registry;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;
using StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

namespace StarWarsProgressBarIssueTracker.App.Jobs;

public class JobExecutionService(ITaskRepository taskRepository, JobFactory jobFactory,
    ResiliencePipelineProvider<string> pipelineProvider)
{
    public async Task ExecuteTask(JobType jobType, CancellationToken cancellationToken)
    {
        var tasks = await taskRepository.GetScheduledTasksAsync(jobType, cancellationToken);

        foreach (var task in tasks)
        {
            if (task.Status == Infrastructure.Models.TaskStatus.Planned)
            {
                task.Status = Infrastructure.Models.TaskStatus.Running;
                await taskRepository.UpdateAsync(task, cancellationToken);

                var job = jobFactory.CreateJob(jobType);

                try
                {
                    var pipeline = pipelineProvider.GetPipeline("job-pipeline");

                    await pipeline.ExecuteAsync(async token =>
                    {
                        try
                        {
                            task.Status = Infrastructure.Models.TaskStatus.Running;
                            await taskRepository.UpdateAsync(task, cancellationToken);

                            await job.ExecuteAsync(cancellationToken);

                            task.Status = Infrastructure.Models.TaskStatus.Completed;
                            task.ExecutedAt = DateTime.UtcNow;
                            await taskRepository.UpdateAsync(task, cancellationToken);
                        }
                        catch
                        {
                            task.Status = Infrastructure.Models.TaskStatus.FailureWaitingForRetry;
                            await taskRepository.UpdateAsync(task, cancellationToken);
                        }
                    }, cancellationToken);
                }
                catch
                {
                    task.Status = Infrastructure.Models.TaskStatus.Error;
                    await taskRepository.UpdateAsync(task, cancellationToken);
                }
            }
        }
    }
}
