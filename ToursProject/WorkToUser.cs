﻿using ToursProject.Context.Enum;
using ToursProject.Context.Models;

namespace ToursProject
{
    internal static class WorkToUser
    {
        private static User user;

        public static User User
        {
            get 
            { 
                if(user == null)
                {
                    user = new User()
                    {
                        FirstName = "Неавторизованный гость",
                        LastName = string.Empty,
                        Patronymic = string.Empty,
                        Role = Role.Admin
                    };
                }
                return user;
            }
            set { user = value; }
        }

        internal static bool CompareRole(Role role)
         => role == User.Role;
    }
}
