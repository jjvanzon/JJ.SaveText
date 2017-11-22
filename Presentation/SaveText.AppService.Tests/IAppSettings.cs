namespace JJ.Presentation.SaveText.AppService.Tests
{
	internal interface IAppSettings
	{
		string SaveTextAppServiceUrl { get; }
		string SaveTextWithSyncAppServiceUrl { get; }
		string ResourceAppServiceUrl { get; }
		string CultureName { get; }
	}
}
