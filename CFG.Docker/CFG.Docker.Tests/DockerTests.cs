using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CFG.Docker.Tests
{
    [TestClass]
    public class DockerTests
    {
        private bool UseProductionForTesting = true;

        [TestMethod]
        public void PingServiceTest()
        {
            // Create DOCKER instance
            IDocker dockerInstance = BuildStandardDockerInstance();

            // Assert
            Assert.IsTrue(dockerInstance.Ping() == "Pong");
        }

        [TestMethod]
        public void RegisterAndThenDeleteComponentTest()
        {
            // Create DOCKER instance
            IDocker dockerInstance = BuildStandardDockerInstance();

            // Act
            dockerInstance.RegisterComponent("Component BETA");
            dockerInstance.DeleteComponent("Component Beta");     
        }

        [TestMethod]
        public void PublishConfigurationValue()
        {
            // Create DOCKER instance
            IDocker dockerInstance = BuildStandardDockerInstance();

            // Act            
            dockerInstance.PublishConfigurationAtom("Component BETA.ConfigValue1.Sub1.SubSub1", null);
        }

        [TestMethod]
        public void DeleteConfigurationValue()
        {
            // Create DOCKER instance
            IDocker dockerInstance = BuildStandardDockerInstance();

            // Act
            dockerInstance.RegisterComponent("Component BETA");
            dockerInstance.PublishConfigurationAtom("Component BETA.ConfigValue1", "Main");
            dockerInstance.PublishConfigurationAtom("Component BETA.ConfigValue1.Sub1", "Sub");
            dockerInstance.PublishConfigurationAtom("Component BETA.ConfigValue1.Sub1.SubSub1", "SubSub");
            dockerInstance.DeleteConfigurationAtom("Component BETA.ConfigValue1.Sub1.SubSub1");
            dockerInstance.DeleteConfigurationAtom("Component BETA.ConfigValue1.Sub1");        
            dockerInstance.DeleteConfigurationAtom("Component BETA.ConfigValue1");
            dockerInstance.DeleteComponent("Component BETA");
        }

        [TestMethod]
        public void ListComponentsTest()
        {
            // Create DOCKER instance
            IDocker dockerInstance = BuildStandardDockerInstance();

            // Act
            //dockerInstance.RegisterComponent("Alpha");
            //dockerInstance.RegisterComponent("Beta");
            //dockerInstance.RegisterComponent("Gamma");
            //dockerInstance.RegisterComponent("Omega");
            //dockerInstance.RegisterComponent("Aaron");
            //dockerInstance.PublishConfigurationAtom("Aaron.A", null);
            //dockerInstance.PublishConfigurationAtom("Aaron.B", null);
            //dockerInstance.PublishConfigurationAtom("Aaron.C", null);
            //dockerInstance.PublishConfigurationAtom("Aaron.A.1", null);
            //dockerInstance.PublishConfigurationAtom("Aaron.A.2", null);
            //dockerInstance.PublishConfigurationAtom("Aaron.A.3", null);
            //dockerInstance.PublishConfigurationAtom("Aaron.A.1.P", null);
            //dockerInstance.PublishConfigurationAtom("Aaron.A.1.B", null);
            //dockerInstance.PublishConfigurationAtom("Aaron.A.1.A", null);
            //dockerInstance.PublishConfigurationAtom("Aaron.A.1.F", "Actual Value");
            //List<string> componentList = dockerInstance.ListComponents();
            List<string> output = dockerInstance.ListSubAtoms("Aaron");
            string value = dockerInstance.ResolveAtomAsString("Aaron.A.1.F");
            value = dockerInstance.ResolveAtomAsString("Aaron.A.1.A");
            dockerInstance.ListSubAtoms("Aaron.A");
            output = dockerInstance.ListSubAtoms("Aaron.A.1");           
        }

        [TestMethod]
        public void ListSubAtomsTest()
        {
            // Create DOCKER instance
            IDocker dockerInstance = BuildStandardDockerInstance();

            // Act
            dockerInstance.ListSubAtoms("");
        }

        [TestMethod]
        public void ResolveConfigurationValue()
        {
            // Create DOCKER instance
            IDocker dockerInstance = BuildStandardDockerInstance();

            // Act
            dockerInstance.PublishConfigurationAtom("Component BETA.ConfigValue1", "Main");
            dockerInstance.PublishConfigurationAtom("Component BETA.ConfigValue1.Sub1", "Sub");            
            dockerInstance.PublishConfigurationAtom("Component BETA.ConfigValue1.Sub1.SubSub1", "SubSub");
        }

        private IDocker BuildStandardDockerInstance()
        {
            // Build and return
            IDocker docker = new StandardConfigHubDocker();
            docker.Setup((UseProductionForTesting ? "broomscom.com/CFG.Hub" : "http://localhost:53551/"), "994BCF73-1E51-419D-B1A2-E316EFF6F008", "64B7A077-2D9A-437C-AFEF-AA46B6352B60", false);
            return docker;
        }
    }
}
