using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public interface IIssueRepository : IRepository<DbIssue>
{
    void DeleteVehicle(DbVehicle dbVehicle);
}
