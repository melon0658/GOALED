using UnityEngine;
using System;
using Grpc.Core;

[CreateAssetMenu(fileName = "MatchingServer", menuName = "MatchingServer", order = 1)]
public class MatchingServer : ScriptableObject, ISerializationCallbackReceiver
{
  [SerializeField] private string localHost;
  [SerializeField] private int localPort;
  [SerializeField] private string host;
  [SerializeField] private int port;
  [SerializeField] private bool isLocal;
  [NonSerialized] public Channel channel;
  [NonSerialized] public MatchingService.MatchingService.MatchingServiceClient client;

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
    client = new MatchingService.MatchingService.MatchingServiceClient(channel);
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

