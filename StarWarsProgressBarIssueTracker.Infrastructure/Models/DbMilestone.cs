﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StarWarsProgressBarIssueTracker.Domain.Milestones;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbMilestone : DbEntityBase
{
    [StringLength(MilestoneConstants.MaxTitleLength, MinimumLength = MilestoneConstants.MinTitleLength)]
    public required string Title { get; set; }

    [MaxLength(MilestoneConstants.MaxDescriptionLength)]
    public string? Description { get; set; }

    public MilestoneState MilestoneState { get; set; }

    [Column("IssueId")]
    public virtual List<DbIssue> Issues { get; set; } = [];
}
