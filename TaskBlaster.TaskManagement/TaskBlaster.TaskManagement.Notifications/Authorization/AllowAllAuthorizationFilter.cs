using Hangfire.Dashboard;

namespace TaskBlaster.TaskManagement.Notifications.Authorization;

public class AllowAllAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        return true;
    }
}