using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexScheduler
{
    public class Scheduler
    {
        private readonly IEnumerable<IScheduledTask> _scheduledTasks;

        public Scheduler(IEnumerable<IScheduledTask> scheduledTasks)
        {
            _scheduledTasks = scheduledTasks;
        }

        public async Task Execute()
        {
            foreach (IScheduledTask scheduledTask in _scheduledTasks)
            {
                await scheduledTask.Invoke();
            }
        }
    }
}