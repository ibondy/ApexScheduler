using ApexScheduler.ApexStatus;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApexScheduler.RavenDB
{
    public interface IDbClient
    {
        Task<StatusRoot> GetStatus(string apexId);

        void Dispose();
    }
}