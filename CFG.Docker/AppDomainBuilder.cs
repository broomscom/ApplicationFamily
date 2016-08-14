
namespace CFG.Docker
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Static class for building instance with housing AppDomains
    /// </summary>
    public static class AppDomainBuilder
    {
        #region Merchant
        /// <summary>
        /// Resolves the instance.
        /// </summary>
        /// <typeparam name="T">Type to cast instance back as.</typeparam>
        /// <param name="dllLocationOnDisk">The DLL location on disk.</param>
        /// <param name="instanceNameWithNameSpace">The instance name with name space.</param>
        /// <param name="generatedAppDomain">The generated application domain.</param>
        /// <returns>Resolved instance inside of generated application domain.</returns>
        public static T ResolveInstance<T>(string dllLocationOnDisk, string instanceNameWithNameSpace, out AppDomain generatedAppDomain)
        {
            // Make specific internal call
            return ResolveInstanceInternal<T>(dllLocationOnDisk, instanceNameWithNameSpace, out generatedAppDomain, null);
        }

        /// <summary>
        /// Resolves the instance passing the specified arguments into the constructor.
        /// </summary>
        /// <typeparam name="T">Type to cast instance back as.</typeparam>
        /// <param name="dllLocationOnDisk">The DLL location on disk.</param>
        /// <param name="instanceNameWithNameSpace">The instance name with name space.</param>
        /// <param name="generatedAppDomain">The generated application domain.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>Resolved instance inside of generated application domain.</returns>
        public static T ResolveInstance<T>(string dllLocationOnDisk, string instanceNameWithNameSpace, out AppDomain generatedAppDomain, object[] args)
        {
            // Make specific internal call
            return ResolveInstanceInternal<T>(dllLocationOnDisk, instanceNameWithNameSpace, out generatedAppDomain, args);
        }

        /// <summary>
        /// Unloads the instance.
        /// </summary>
        /// <param name="usingAppDomain">The generated application domain.</param>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.ArgumentNullException">Specified parameters cannot be null.</exception>
        public static void UnloadInstance(AppDomain usingAppDomain, object instance)
        {
            try
            {
                // Validate - AppDomain is not null
                if (usingAppDomain == null || usingAppDomain.BaseDirectory == null)
                {
                    throw new CFGDockerException("AppDomain, instance and AppDoman Base Directory must all be non null", new ArgumentNullException());
                }

                // Dispose
                try
                {
                    IDisposable disposal = instance as IDisposable;
                    disposal.Dispose();
                }
                catch
                {
                    // Don't care
                }

                // Unload
                AppDomain.Unload(usingAppDomain);
            }
            catch (Exception err)
            {
                throw new CFGDockerException("Unexpected failure unloading AppDomain for directory'" + usingAppDomain.BaseDirectory + "'", err);
            }
        }
        #endregion

        #region Cores
        /// <summary>
        /// Internal re-factored method.
        /// </summary>
        /// <typeparam name="T">Type to cast instance back as.</typeparam>
        /// <param name="dllLocationOnDisk">The DLL location on disk.</param>
        /// <param name="instanceNameWithNameSpace">The instance name with name space.</param>
        /// <param name="generatedAppDomain">The generated application domain.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>Resolved instance inside of generated application domain.</returns>
        internal static T ResolveInstanceInternal<T>(string dllLocationOnDisk, string instanceNameWithNameSpace, out AppDomain generatedAppDomain, object[] args)
        {
            // Default AppDomain to null
            generatedAppDomain = null;

            // Validation - Verify DLL exists
            if (!File.Exists(dllLocationOnDisk))
            {
                throw new CFGDockerException("The DLL '" + dllLocationOnDisk + "' does not exist on disk", null);
            }

            // Validation - Verify access to DLL
            try
            {
                File.ReadAllBytes(dllLocationOnDisk);
            }
            catch (Exception err)
            {
                throw new CFGDockerException("The DLL '" + dllLocationOnDisk + "' exists on disk, but cannot be accessed", err);
            }

            // Build new AppDomain to house instance
            try
            {
                generatedAppDomain = AppDomain.CreateDomain(
                    instanceNameWithNameSpace,
                    AppDomain.CurrentDomain.Evidence,
                    new AppDomainSetup()
                    {
                        ApplicationBase = Path.GetDirectoryName(dllLocationOnDisk),
                        PrivateBinPath = Path.GetDirectoryName(dllLocationOnDisk),
                        PrivateBinPathProbe = Path.GetDirectoryName(dllLocationOnDisk)
                    });
            }
            catch (Exception err)
            {
                throw new CFGDockerException("Unexpected failure building AppDomain for assembly '" + dllLocationOnDisk + "'", err);
            }

            // Build new instance housed in new AppDomain
            object instance = null;
            try
            {
                instance = generatedAppDomain.CreateInstanceAndUnwrap(
                    Assembly.LoadFile(dllLocationOnDisk).FullName,
                    instanceNameWithNameSpace,
                    false,
                    BindingFlags.CreateInstance,
                    null,
                    args,
                    CultureInfo.InvariantCulture,
                    null);
            }
            catch (Exception err)
            {
                throw new CFGDockerException("Unexpected failure building Instance from generated AppDomain for assembly '" + dllLocationOnDisk + "' of type '" + instanceNameWithNameSpace + "'", err);
            }

            // Return instance
            try
            {
                return (T)instance;
            }
            catch (Exception err)
            {
                try
                {
                    throw new CFGDockerException("Unexpected failure casting Instance to type '" + instanceNameWithNameSpace + "' to a '" + typeof(T).FullName + "' from generated AppDomain for assembly '" + dllLocationOnDisk + "' of type '" + instanceNameWithNameSpace + "'", err);
                }
                catch (Exception innerErr)
                {
                    throw new CFGDockerException("Unexpected unknown failure casting  for assembly '" + dllLocationOnDisk + "' with type '" + instanceNameWithNameSpace + "'", innerErr);
                }
            }
        }
        #endregion
    }
}