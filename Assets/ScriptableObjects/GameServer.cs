using UnityEngine;
using System;
using Grpc.Core;

[CreateAssetMenu(fileName = "GameServer", menuName = "GameServer", order = 1)]
public class GameServer : ScriptableObject, ISerializationCallbackReceiver
{
  [SerializeField] private string host;
  [SerializeField] private int port;
  [NonSerialized] public Channel channel;
  [NonSerialized] public GameService.GameService.GameServiceClient client;

  public void OnEnable()
  {
    channel = new Channel(host, port, ChannelCredentials.Insecure);
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

