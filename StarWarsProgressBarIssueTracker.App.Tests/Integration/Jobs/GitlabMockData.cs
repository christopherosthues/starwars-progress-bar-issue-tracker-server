using StarWarsProgressBarIssueTracker.Domain.Milestones;
using StarWarsProgressBarIssueTracker.Domain.Releases;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.App.Tests.Integration.Jobs;

public static class GitlabMockData
{
    public const string LabelResponse = """
                                        {
                                            "data": {
                                                "project": {
                                                    "__typename": "Project",
                                                    "id": "gid://gitlab/Project/1",
                                                    "labels": {
                                                        "__typename": "LabelConnection",
                                                        "nodes": [
                                                            {
                                                                "__typename": "Label",
                                                                "color": "#f7e7ce",
                                                                "description": "Vehicles appearing in Andor",
                                                                "id": "gid://gitlab/ProjectLabel/1",
                                                                "textColor": "#1F1E24",
                                                                "title": "Andor",
                                                                "updatedAt": "2023-02-25T12:57:46Z"
                                                            },
                                                            {
                                                                "__typename": "Label",
                                                                "color": "#cd5b45",
                                                                "description": "",
                                                                "id": "gid://gitlab/ProjectLabel/2",
                                                                "textColor": "#FFFFFF",
                                                                "title": "Bad Batch",
                                                                "updatedAt": "2022-11-02T15:01:41Z"
                                                            },
                                                            {
                                                                "__typename": "Label",
                                                                "color": "#FA7320",
                                                                "description": "The old Clone Wars series",
                                                                "id": "gid://gitlab/ProjectLabel/3",
                                                                "textColor": "#FFFFFF",
                                                                "title": "Clone Wars",
                                                                "updatedAt": "2023-03-10T22:26:55Z"
                                                            }
                                                        ]
                                                    }
                                                }
                                            }
                                        }
                                        """;

    public const string MilestoneResponse = """
                                            {
                                                "data": {
                                                    "project": {
                                                        "__typename": "Project",
                                                        "id": "gid://gitlab/Project/1",
                                                        "milestones": {
                                                            "__typename": "MilestoneConnection",
                                                            "nodes": [
                                                                {
                                                                    "__typename": "Milestone",
                                                                    "description": "",
                                                                    "id": "gid://gitlab/Milestone/1",
                                                                    "iid": "1",
                                                                    "state": "closed",
                                                                    "title": "Features",
                                                                    "updatedAt": "2022-12-30T11:49:41Z"
                                                                },
                                                                {
                                                                    "__typename": "Milestone",
                                                                    "description": "",
                                                                    "id": "gid://gitlab/Milestone/2",
                                                                    "iid": "2",
                                                                    "state": "active",
                                                                    "title": "Yuuzhan-Vong",
                                                                    "updatedAt": "2022-11-02T22:12:15Z"
                                                                },
                                                                {
                                                                    "__typename": "Milestone",
                                                                    "description": "Desc3",
                                                                    "id": "gid://gitlab/Milestone/3",
                                                                    "iid": "3",
                                                                    "state": "active",
                                                                    "title": "Mandalorians",
                                                                    "updatedAt": "2022-11-02T14:44:21Z"
                                                                }
                                                            ]
                                                        }
                                                    }
                                                }
                                            }
                                            """;

    public const string ReleaseResponse = """
                                          {
                                              "data": {
                                                  "project": {
                                                      "__typename": "Project",
                                                      "id": "gid://gitlab/Project/1",
                                                      "issues": {
                                                          "__typename": "IssueConnection",
                                                          "count": 538,
                                                          "nodes": [
                                                              {
                                                                  "__typename": "Issue",
                                                                  "description": "{\n  \"Description\": \"\",\n  \"Priority\": 0,\n  \"EngineColor\": null,\n  \"Translations\": []\n}",
                                                                  "dueDate": null,
                                                                  "id": "gid://gitlab/Issue/1",
                                                                  "iid": "11",
                                                                  "labels": {
                                                                      "__typename": "LabelConnection",
                                                                      "nodes": [
                                                                      ]
                                                                  },
                                                                  "milestone": null,
                                                                  "projectId": 1,
                                                                  "state": "closed",
                                                                  "title": "125-Z treadspeeder bike",
                                                                  "updatedAt": "2023-03-05T13:54:06Z",
                                                                  "webUrl": "https://gitlab.com/osthues.christopher/starwars-progress-bar/-/issues/11"
                                                              },
                                                              {
                                                                  "__typename": "Issue",
                                                                  "description": "{\n  \"Description\": \"Rebels\\r\\n- [ ] Imperial\\r\\n- [ ] Ezra Bridger\",\n  \"Priority\": 0,\n  \"EngineColor\": null,\n  \"Translations\": [\n    {\n      \"Country\": \"en\",\n      \"Text\": \"614-AvA Speeder Bike\"\n    },\n    {\n      \"Country\": \"es\",\n      \"Text\": \"Moto deslizadora 614-AvA Imperial\"\n    }\n  ]\n}",
                                                                  "dueDate": null,
                                                                  "id": "gid://gitlab/Issue/2",
                                                                  "iid": "22",
                                                                  "labels": {
                                                                      "__typename": "LabelConnection",
                                                                      "nodes": [
                                                                      ]
                                                                  },
                                                                  "milestone": null,
                                                                  "projectId": 1,
                                                                  "state": "opened",
                                                                  "title": "614-AvA Speeder Bike",
                                                                  "updatedAt": "2023-04-22T19:07:14Z",
                                                                  "webUrl": "https://gitlab.com/osthues.christopher/starwars-progress-bar/-/issues/22"
                                                              },
                                                              {
                                                                  "__typename": "Issue",
                                                                  "description": "{\n  \"Description\": \"\",\n  \"Priority\": 1,\n  \"EngineColor\": null,\n  \"Translations\": [\n    {\n      \"Country\": \"en\",\n      \"Text\": \"74-Z speeder bike\"\n    },\n    {\n      \"Country\": \"de\",\n      \"Text\": \"74-Z-D\\\\u00fcsenschlitten\"\n    },\n    {\n      \"Country\": \"es\",\n      \"Text\": \"Moto deslizadora 74-Z\"\n    }\n  ]\n}",
                                                                  "dueDate": null,
                                                                  "id": "gid://gitlab/Issue/3",
                                                                  "iid": "33",
                                                                  "labels": {
                                                                      "__typename": "LabelConnection",
                                                                      "nodes": [
                                                                      ]
                                                                  },
                                                                  "milestone": null,
                                                                  "projectId": 1,
                                                                  "state": "opened",
                                                                  "title": "74-Z speeder bike",
                                                                  "updatedAt": "2023-04-22T19:06:51Z",
                                                                  "webUrl": "https://gitlab.com/osthues.christopher/starwars-progress-bar/-/issues/33"
                                                              }
                                                         ]
                                                     }
                                                 }
                                              }
                                          }
                                          """;

    public static IList<DbLabel> AddedLabels()
    {
        return
        [
            new DbLabel
            {
                Title = "Andor",
                Description = "Vehicles appearing in Andor",
                TextColor = "#1F1E24",
                Color = "#f7e7ce",
                GitlabId = "gid://gitlab/ProjectLabel/1",
                LastModifiedAt = new DateTime(2023, 2, 25, 12, 57, 46)
            },
            new DbLabel
            {
                Title = "Bad Batch",
                Description = string.Empty,
                TextColor = "#FFFFFF",
                Color = "#cd5b45",
                GitlabId = "gid://gitlab/ProjectLabel/2",
                LastModifiedAt = new DateTime(2022, 11, 2, 15, 1, 41)
            },
            new DbLabel
            {
                Title = "Clone Wars",
                Description = "The old Clone Wars series",
                TextColor = "#FFFFFF",
                Color = "#FA7320",
                GitlabId = "gid://gitlab/ProjectLabel/3",
                LastModifiedAt = new DateTime(2023, 3, 10, 22, 26, 55)
            },
        ];
    }

    public static IList<DbMilestone> AddedMilestones()
    {
        return
        [
            new DbMilestone
            {
                Title = "Features",
                Description = string.Empty,
                GitlabId = "gid://gitlab/Milestone/1",
                GitlabIid = "1",
                State = MilestoneState.Closed,
                LastModifiedAt = new DateTime(2022, 12, 30, 11, 49, 41)
            },
            new DbMilestone
            {
                Title = "Yuuzhan-Vong",
                Description = string.Empty,
                GitlabId = "gid://gitlab/Milestone/2",
                GitlabIid = "2",
                State = MilestoneState.Open,
                LastModifiedAt = new DateTime(2022, 11, 2, 22, 12, 15)
            },
            new DbMilestone
            {
                Title = "Mandalorians",
                Description = "Desc3",
                GitlabId = "gid://gitlab/Milestone/3",
                GitlabIid = "3",
                State = MilestoneState.Open,
                LastModifiedAt = new DateTime(2022, 11, 2, 14, 44, 21)
            },
        ];
    }

    public static IList<DbRelease> AddedReleases()
    {
        return
        [
            new DbRelease
            {
                Title = "Features",
                Notes = "Hello there",
                Date = DateTime.Today,
                GitlabId = "gid://gitlab/Milestone/1",
                GitlabIid = "1",
                State = ReleaseState.Released,
                LastModifiedAt = new DateTime(2022, 12, 30, 11, 49, 41)
            },
            new DbRelease
            {
                Title = "Yuuzhan-Vong",
                Notes = string.Empty,
                GitlabId = "gid://gitlab/Milestone/2",
                GitlabIid = "2",
                State = ReleaseState.Planned,
                LastModifiedAt = new DateTime(2022, 11, 2, 22, 12, 15)
            },
            new DbRelease
            {
                Title = "Mandalorians",
                Notes = "Desc3",
                GitlabId = "gid://gitlab/Milestone/3",
                GitlabIid = "3",
                State = ReleaseState.Planned,
                LastModifiedAt = new DateTime(2022, 11, 2, 14, 44, 21)
            },
        ];
    }
}
