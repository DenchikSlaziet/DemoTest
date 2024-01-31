using ToursProject.Context.Enum;
using ToursProject.Context.Models;

namespace ToursProject
{
    internal static class WorkToUser
    {
        internal static User User { get; set; }

        internal static bool CompareRole(Role role)
         => role == User.Role;
    }
}
