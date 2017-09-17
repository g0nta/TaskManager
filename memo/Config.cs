using System;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace memo
{
    public static class Config
	{
		private static IConfigurationBuilder builder
			= new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appConfig.json");

		private static IConfigurationRoot config = builder.Build();

        public static string FilePath 
        { 
            get
            {
                return config["filepath"];
            }  
        }

    }
}
