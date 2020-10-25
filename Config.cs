using System.Dynamic;

namespace ApexScheduler
{
    public class ApexConfigs
    {
        public Config Apex1 { get; set; } = new Config();
        public Config Apex2 { get; set; } = new Config();

        public RavenDBConfig RavenDBConfig { get; set; } = new RavenDBConfig();
    }

    public class Config
    {
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Url { get; set; }
        public string User { get; set; }
        public string ALK { get; set; }
        public string AlkCaMg { get; set; }
        public ApexCommand AlkTestApexCommand { get; set; }
        public ApexCommand AllTestsApexCommand { get; set; }
    }

    public class RavenDBConfig
    {
        public string[] Url { get; set; }
        public string DatabaseName { get; set; }
    }
}