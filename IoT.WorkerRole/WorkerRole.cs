using Microsoft.WindowsAzure.ServiceRuntime;
using Orleans.Host;

namespace IoT.WorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            silo.Run();
        }

        public override void OnStop()
        {
            silo.Stop();
        }

        OrleansAzureSilo silo;

        public override bool OnStart()
        {
            silo = new OrleansAzureSilo();
            var success = silo.Start(RoleEnvironment.DeploymentId, RoleEnvironment.CurrentRoleInstance);

            return success;
        }
    }
}
