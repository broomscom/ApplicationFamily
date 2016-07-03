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
            IDocker docker = new StandardConfigHubDocker();
            docker.Setup("broomscom.com/CFG.Hub", "25E9BDF2-DF1E-42BD-BD58-1CAFFDC554A2", false);

            // Assert
            Assert.IsTrue(docker.Ping() == "Pong");
        }

        [TestMethod]
        public void PublishValueTest()
        {
            // Create DOCKER instance
            IDocker docker = new StandardConfigHubDocker();

            // Act
            docker.Publish("Test Component 1.FirstValue", "True");

            // Assert

        }
    }
}
