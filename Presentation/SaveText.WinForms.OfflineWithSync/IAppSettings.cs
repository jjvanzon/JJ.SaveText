namespace JJ.Presentation.SaveText.WinForms.OfflineWithSync
{
	internal interface IAppSettings
	{
		string AppServiceUrl { get; }
		int SynchronizationTimerIntervalInMilliseconds { get; }
		int CheckServiceAvailabilityTimeoutInMilliseconds { get; }
	}
}
