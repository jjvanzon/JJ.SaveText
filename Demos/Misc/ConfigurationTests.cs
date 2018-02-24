using System.Configuration;
using JJ.Framework.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable UnusedVariable

namespace JJ.Demos.Misc
{
	[TestClass]
	public class ConfigurationTests
	{
		[TestMethod]
		public void Test_ConfigurationManager_OpenFromFile_ReadFrom_ConfigurationManager_AppSettings_Indexer_DoesNotWork()
		{
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(
				new ExeConfigurationFileMap { ExeConfigFilename = "Custom.config" },
				ConfigurationUserLevel.None);

			string appSettingValue = ConfigurationManager.AppSettings["MyAppSettingKey"];

			AssertHelper.IsNullOrEmpty(() => appSettingValue);
		}

		[TestMethod]
		public void Test_ConfigurationManager_OpenFromFile_ReadFrom_Configuration_AppSettings_Settings_Indexer_Value_Works()
		{
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(
				new ExeConfigurationFileMap { ExeConfigFilename = "Custom.config" },
				ConfigurationUserLevel.None);

			string appSettingValue = configuration.AppSettings.Settings["MyAppSettingKey"].Value;

			AssertHelper.AreEqual("MyAppSettingValue", () => appSettingValue);
		}

		[TestMethod]
		public void Test_ConfigurationManager_OpenFromFile_ReadFrom_Configuration_AppSettings_Indexer_DoesNotWork()
		{
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(
				new ExeConfigurationFileMap { ExeConfigFilename = "Custom.config" },
				ConfigurationUserLevel.None);

			object appSetting = null;
			// Not even accessible. Is accessible in my Watch screen, which is strange. Maybe that's a new thing.
			//object appSetting = configuration.AppSettings["MyAppSettingKey"];

			AssertHelper.IsNull(() => appSetting);
		}

		[TestMethod]
		public void Test_ConfigurationManager_OpenFromFile_GetConfigurationSetion()
		{
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(
				new ExeConfigurationFileMap { ExeConfigFilename = "Custom.config" },
				ConfigurationUserLevel.None);

			ConfigurationSection configurationSection = configuration.GetSection("system.serviceModel/bindings");

			AssertHelper.IsNotNull(() => configurationSection);
		}
	}
}
