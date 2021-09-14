using System;
using System.Collections.Generic;
using lagalt_api.Models.Domain;
using lagalt_api.Models.Domain.Enums;

namespace lagalt_api.Data
{
    public class SeedUsersHelper
    {
        public static ICollection<User> GetUserSeeds()
        {
            List<User> seedUsers = new List<User>()
            {
                new User
                {
                    UserId = "717f73d0-2823-425f-9184-cc1578a4f8f9",
                    UserName = "emma",
                    Hidden = true,
                    Description = ""
                },
                new User
                {
                    UserId = "717f73b0-2823-425f-9184-cc1678a4f7f9",
                    UserName = "marcus",
                    Hidden = false,
                    Description = ""
                },
                new User
                {
                    UserId = "417f73d0-2823-425f-91t4-cc1578a4f8f6",
                    UserName = "my",
                    Hidden = false,
                    Description = ""
                },
                new User
                {
                    UserId = "717f73f0-2823-425f-9134-cc1588a4f8f9",
                    UserName = "henrik",
                    Hidden = false,
                    Description = ""
                }
            };
            return seedUsers;
        }


        public static ICollection<Photo> GetPhotoSeeds()
        {
            List<Photo> seedPhotos = new List<Photo>()
                {
                    new Photo{PhotoId = 1, PhotoUrl = "https://avatars.dicebear.com/api/big-smile/emma.svg", ProjectId = 1},
                    new Photo{PhotoId = 2, PhotoUrl = "https://avatars.dicebear.com/api/big-smile/marcus.svg", ProjectId = 1},
                    new Photo{PhotoId = 3, PhotoUrl = "https://avatars.dicebear.com/api/big-smile/my.svg", ProjectId = 2},
                    new Photo{PhotoId = 4, PhotoUrl = "https://avatars.dicebear.com/api/big-smile/henrik.svg", ProjectId = 1}
                };
            return seedPhotos;
        }

        public static ICollection<Project> GetProjectSeeds()
        {
            List<Project> seedProjects = new List<Project>()
            {
                new Project
                {
                    ProjectId = 1,
                    ProjectName = "Pokemon",
                    Progress = ProgressStatus.Completed,
                    Created = new DateTime(2021, 07, 21, 10, 30, 03 ),
                    Closed = new DateTime(2021, 08, 24, 11, 32, 45)
                },
                new Project
                {
                    ProjectId = 2,
                    ProjectName = "Quiz",
                    Progress = ProgressStatus.Founding,
                    Created = new DateTime(2021, 08, 12, 10, 30, 03 )
                }
            };
            return seedProjects;
        }
    }
}