
using System;
namespace HumanGavel.Common.Configuration
{
	public static partial class ApplicationConfigurationManager
	{
		
		public static String DocumentDbURI
		{
			get
			{
				return GetSetting("DocumentDbURI");
			}
		}

		
		public static String DocumentDbKey
		{
			get
			{
				return GetSetting("DocumentDbKey");
			}
		}

		
		public static String CloudStorageConnectionString
		{
			get
			{
				return GetSetting("CloudStorageConnectionString");
			}
		}

			}
}