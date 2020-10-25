using System.Threading.Tasks;

namespace ApexScheduler
{
    public interface IScheduledTask
    {
        Task Invoke();
    }
}