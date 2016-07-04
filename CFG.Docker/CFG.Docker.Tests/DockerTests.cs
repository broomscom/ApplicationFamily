using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CFG.Docker.Tests
{
    [TestClass]
    public class DockerTests
    {
        [TestMethod]
        public void PingServiceTest()
        {
            // Create DOCKER instance
            IDocker dockerInstance = BuildStandardDockerInstance();

            // Assert
            Assert.IsTrue(dockerInstance.Ping() == "Pong");
        }

        [TestMethod]
        public void RegisterComponentTest()
        {
            // Create DOCKER instance
            IDocker dockerInstance = BuildStandardDockerInstance();

            // Act
            dockerInstance.RegisterComponent("    Component BETA  ");            

            // Assert
        }

        private IDocker BuildStandardDockerInstance()
        {
            // Build and return
            IDocker docker = new StandardConfigHubDocker();
            docker.Setup("http://localhost:53551/", "994BCF73-1E51-419D-B1A2-E316EFF6F008", "64B7A077-2D9A-437C-AFEF-AA46B6352B60", false);
            return docker;
        }
    }
}
