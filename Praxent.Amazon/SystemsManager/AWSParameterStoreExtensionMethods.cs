using System;
using Amazon;
using Amazon.Extensions.Configuration.SystemsManager;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.Extensions.Configuration;

namespace Praxent.Amazon.SystemsManager
{
    public static class AWSParameterStoreExtensionMethods
    {
        /// <summary>
        /// Add the AWS Parameter Store to the configuration builder
        /// </summary>
        /// <param name="builder">IConfigurationBuilder</param>
        /// <param name="path"> The Path to use.</param>
        /// <param name="profileName"> The Profile Name for the machine's AWS Config.</param>
        /// <param name="isOptional"> Whether AWS Configuration can be skipped or must be present.</param>
        /// <returns></returns>
        public static IConfigurationBuilder AddAWSParameterStore(this IConfigurationBuilder builder, string path, string profileName = "default", bool isOptional = true)
        {
            builder.AddSystemsManager(configureSource =>
            {
                configureSource.Path = path;
                configureSource.ReloadAfter = TimeSpan.FromMinutes(5);
                configureSource.AwsOptions = new AWSOptions()
                {
                    Profile = profileName,
                };
                configureSource.Optional = isOptional;
                configureSource.OnLoadException += exceptionContext =>
                {
                    // Add custom error handling. For example, look at the exceptionContext.Exception and decide
                    // whether to ignore the error or tell the provider to attempt to reload.

                    //TODO: Add Logging
                };

                // Implement custom parameter process, which transforms Parameter Store names into
                // names for the .NET Core configuration system.
                configureSource.ParameterProcessor = new DefaultParameterProcessor();
            });

            return builder;
        }
    }
}