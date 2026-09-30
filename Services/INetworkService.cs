namespace Demo.Services;

public interface INetworkService
{
	bool IsConnected{get;}
	NetworkAccess AccessType{get;}
	event EventHandler<bool> ConnectionChanged;
}