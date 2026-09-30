namespace Demo.Services;

public class NetworkService : INetworkService, IDisposable
{
	private readonly IConnectivity connectivity;
	private bool isConnected;

	public event EventHandler<bool> ConnectionChanged;
	public bool IsConnected => isConnected;
	public NetworkAccess AccessType => connectivity.NetworkAccess;

	public NetworkService(IConnectivity connectivity)
	{
		this.connectivity = connectivity;
        isConnected = connectivity.NetworkAccess == NetworkAccess.Internet;
		connectivity.ConnectivityChanged += OnConnectivityChanged;
	}

	private void OnConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
	{
		bool newStatus = e.NetworkAccess == NetworkAccess.Internet;
		if (isConnected != newStatus)
		{
			isConnected = newStatus;
			ConnectionChanged?.Invoke(this, isConnected);
		}
	}

	public void Dispose()
	{
		connectivity.ConnectivityChanged -= OnConnectivityChanged;
	}
}