using UnityEngine;
using System;
using Grpc.Core;

[CreateAssetMenu(fileName = "GameServer", menuName = "GameServer", order = 1)]
public class GameServer : ScriptableObject, ISerializationCallbackReceiver
{
  [SerializeField] private string localHost;
  [SerializeField] private int localPort;
  [SerializeField] private string host;
  [SerializeField] private int port;
  [SerializeField] private bool isLocal;
  [NonSerialized] public Channel channel;
  [NonSerialized] public GameService.GameService.GameServiceClient client;

  public void OnEnable()
  {
    if (isLocal)
    {
      channel = new Channel(localHost, localPort, ChannelCredentials.Insecure);
    }
    else
    {
      channel = new Channel(host, port, ChannelCredentials.Insecure);
    }
    client = new GameService.GameService.GameServiceClient(channel);
  }

  public void OnBeforeSerialize()
  {

  }

  public void OnAfterDeserialize()
  {
    // channel.ShutdownAsync().Wait();
    client = null;
    channel = null;
  }
}

