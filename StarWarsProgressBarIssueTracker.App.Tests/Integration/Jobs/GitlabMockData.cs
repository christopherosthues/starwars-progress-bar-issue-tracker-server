using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.App.Tests.Integration.Jobs;

public static class GitlabMockData
{
    public const string LabelResponse = """
                                        {
                                            "data": {
                                                "project": {
                                                    "__typename": "Project",
                                                    "id": "gid://gitlab/Project/32551361",
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

    public static IEnumerable<DbLabel> AddedLabels()
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
}
