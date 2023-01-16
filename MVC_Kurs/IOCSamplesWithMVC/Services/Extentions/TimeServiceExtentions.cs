using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace IOCSamplesWithMVC.Services.Extentions
{
    public static class TimeServiceExtentions
    {
        public static void AddTimeService(this IServiceCollection collection)
        {
            collection.AddSingleton<ITimeService, TimeService>();   
        }
    }
}
