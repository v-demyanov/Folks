namespace Folks.ChannelsService.Api.Hubs;

public static class HubConnectionsStore
{
    private static Dictionary<string, HashSet<string>> _usersConnections = new Dictionary<string, HashSet<string>>();
    
    public static HashSet<string> GetConnections(string userId)
    {
        _usersConnections.TryGetValue(userId, out var connections);
        if (connections is not null) 
        {
            return connections;
        }

        return new HashSet<string>();
    }

    public static void AddConnection(string userId, string connectionId)
    {
        _usersConnections.TryGetValue(userId, out var connections);
        if (connections is not null)
        {
            connections.Add(connectionId);
            _usersConnections[userId] = connections;
        }
        else
        {
            connections = new HashSet<string> { connectionId };
            _usersConnections.Add(userId, connections);
        }
    }

    public static void RemoveConnection(string userId, string connectionId)
    {
        _usersConnections.TryGetValue(userId, out var connections);
        if (connections is not null)
        {
            connections.Remove(connectionId);
            _usersConnections[userId] = connections;
        }
    }
}
