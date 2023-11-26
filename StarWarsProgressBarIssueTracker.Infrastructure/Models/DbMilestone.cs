﻿using System.ComponentModel.DataAnnotations;
using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public class DbMilestone : DbEntityBase
{
    [MaxLength(50)]
    public required string Title { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    public MilestoneState MilestoneState { get; set; }

    public List<DbIssue> Issues { get; set; } = [];
}
