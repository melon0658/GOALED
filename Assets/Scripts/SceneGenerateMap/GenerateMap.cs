using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
  [SerializeField] private GameObject straightTilePrefab;
  [SerializeField] private GameObject cornerTilePrefab;
  [SerializeField] private GameObject junctionTilePrefab;
  private Tile StartTile;
  private Vector2 JunctionTileSize = new Vector2(115, 160);
  private Vector2 NormalTileSize = new Vector2(75, 75);

  void Start()
  {
    Debug.Log(straightTilePrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size);
    Debug.Log(cornerTilePrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size);
    StartTile = new Tile();
    StartTile.Position = new Vector3(0, 0, 0);
    Tile prevtile = StartTile;
    Tile tile;
    for (int i = 0; i < 10; i++)
    {
      tile = new Tile();
      tile.SetNeighbor(DIRECTION.NEGATIVE_X, Passable.BLOCKED, prevtile);
      prevtile.SetNeighbor(DIRECTION.POSITIVE_X, Passable.PASSABLE, tile);
      prevtile = tile;
    }
    for (int i = 0; i < 10; i++)
    {
      tile = new Tile();
      tile.SetNeighbor(DIRECTION.NEGATIVE_Z, Passable.BLOCKED, prevtile);
      prevtile.SetNeighbor(DIRECTION.POSITIVE_Z, Passable.PASSABLE, tile);
      prevtile = tile;
    }
    for (int i = 0; i < 15; i++)
    {
      tile = new Tile();
      tile.SetNeighbor(DIRECTION.POSITIVE_X, Passable.BLOCKED, prevtile);
      prevtile.SetNeighbor(DIRECTION.NEGATIVE_X, Passable.PASSABLE, tile);
      prevtile = tile;
    }
    for (int i = 0; i < 15; i++)
    {
      tile = new Tile();
      tile.SetNeighbor(DIRECTION.POSITIVE_Z, Passable.BLOCKED, prevtile);
      prevtile.SetNeighbor(DIRECTION.NEGATIVE_Z, Passable.PASSABLE, tile);
      prevtile = tile;
    }
    for (int i = 0; i < 2; i++)
    {
      tile = new Tile();
      tile.SetNeighbor(DIRECTION.NEGATIVE_X, Passable.BLOCKED, prevtile);
      prevtile.SetNeighbor(DIRECTION.POSITIVE_X, Passable.PASSABLE, tile);
      prevtile = tile;
    }
    for (int i = 0; i < 2; i++)
    {
      tile = new Tile();
      tile.SetNeighbor(DIRECTION.POSITIVE_Z, Passable.BLOCKED, prevtile);
      prevtile.SetNeighbor(DIRECTION.NEGATIVE_Z, Passable.PASSABLE, tile);
      prevtile = tile;
    }
    for (int i = 0; i < 5; i++)
    {
      tile = new Tile();
      tile.SetNeighbor(DIRECTION.POSITIVE_X, Passable.BLOCKED, prevtile);
      prevtile.SetNeighbor(DIRECTION.NEGATIVE_X, Passable.PASSABLE, tile);
      prevtile = tile;
    }
    for (int i = 0; i < 5; i++)
    {
      tile = new Tile();
      tile.SetNeighbor(DIRECTION.NEGATIVE_Z, Passable.BLOCKED, prevtile);
      prevtile.SetNeighbor(DIRECTION.POSITIVE_Z, Passable.PASSABLE, tile);
      prevtile = tile;
    }
    var janction = new Tile();
    janction.SetNeighbor(DIRECTION.NEGATIVE_Z, Passable.BLOCKED, prevtile);
    prevtile.SetNeighbor(DIRECTION.POSITIVE_Z, Passable.PASSABLE, janction);
    var tile1 = new Tile();
    tile1.SetNeighbor(DIRECTION.POSITIVE_X, Passable.BLOCKED, janction);
    janction.SetNeighbor(DIRECTION.NEGATIVE_X, Passable.PASSABLE, tile1);
    // var tile1_2 = new Tile();
    // tile1_2.SetNeighbor(DIRECTION.POSITIVE_Z, Passable.BLOCKED, tile1);
    // tile1.SetNeighbor(DIRECTION.NEGATIVE_Z, Passable.PASSABLE, tile1_2);
    var tile2 = new Tile();
    tile2.SetNeighbor(DIRECTION.NEGATIVE_X, Passable.BLOCKED, janction);
    janction.SetNeighbor(DIRECTION.POSITIVE_X, Passable.PASSABLE, tile2);
    // var tile2_2 = new Tile();
    // tile2_2.SetNeighbor(DIRECTION.POSITIVE_Z, Passable.BLOCKED, tile2);
    // tile2.SetNeighbor(DIRECTION.NEGATIVE_Z, Passable.PASSABLE, tile2_2);
    Generate();
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void Generate()
  {
    Queue<Tile> queue = new Queue<Tile>();
    queue.Enqueue(StartTile);
    while (queue.Count > 0)
    {
      Tile tile = queue.Dequeue();
      foreach (var neighbor in tile.GetPassebleNeighbors())
      {
        var tileObject = neighbor.Value;
        Vector2 space = tile.GetTileType() != TileType.JUNCTION ? NormalTileSize : JunctionTileSize;
        if (tileObject.GetTileType() == TileType.JUNCTION)
        {
          space = space + new Vector2(25, 25);
        }
        if (neighbor.Value != null)
        {
          if (neighbor.Key == DIRECTION.POSITIVE_X)
          {
            tileObject.Position = new Vector3(tile.Position.x + space.x, tile.Position.y, tile.Position.z);
          }
          else if (neighbor.Key == DIRECTION.NEGATIVE_X)
          {
            tileObject.Position = new Vector3(tile.Position.x - space.x, tile.Position.y, tile.Position.z);
          }
          else if (neighbor.Key == DIRECTION.POSITIVE_Z)
          {
            tileObject.Position = new Vector3(tile.Position.x, tile.Position.y, tile.Position.z + space.y);
          }
          else if (neighbor.Key == DIRECTION.NEGATIVE_Z)
          {
            tileObject.Position = new Vector3(tile.Position.x, tile.Position.y, tile.Position.z - space.y);
          }
          queue.Enqueue(neighbor.Value);
        }
      }
      GenerateTile(tile);
    }
  }

  private void GenerateTile(Tile tile)
  {
    TileType tileType = tile.GetTileType();
    if (tileType == TileType.STRAIGHT_HORIZONTAL)
    {
      GameObject newtile = Instantiate(straightTilePrefab, tile.Position, Quaternion.identity);
      newtile.name = tile.GetTileType().ToString();
    }
    else if (tileType == TileType.STRAIGHT_VERTICAL)
    {
      var rotate = tile.GetRotate();
      GameObject newtile = Instantiate(straightTilePrefab, tile.Position, rotate);
      newtile.name = tile.GetTileType().ToString();
    }
    else if (tileType == TileType.CORNER)
    {
      var rotate = tile.GetRotate();
      GameObject newtile = Instantiate(cornerTilePrefab, tile.Position, rotate);
      newtile.name = tile.GetTileType().ToString();
    }
    else if (tileType == TileType.JUNCTION)
    {
      GameObject newtile = Instantiate(junctionTilePrefab, tile.Position, Quaternion.identity);
      newtile.name = tile.GetTileType().ToString();
    }
  }

  public (Tile, List<Vector3>, List<Vector3>) WayToTile(Tile start, int step, DIRECTION? direction = null)
  {
    if (start == null)
    {
      start = StartTile;
    }
    List<Vector3> path = new List<Vector3>();
    List<Vector3> rotate = new List<Vector3>();

    Tile tile = direction == null ? start : start.GetNeighbor(direction.Value);
    for (int i = 0; i < step; i++)
    {
      var neighbors = tile.GetPassebleNeighbors();
      foreach (var neighbor in neighbors)
      {
        if (neighbor.Value != null)
        {
          tile = neighbor.Value;
          if (neighbor.Value.GetTileType() == TileType.CORNER)
          {
            rotate.Add(new Vector3(0, -90, 0));
          }
          else
          {
            rotate.Add(new Vector3(0, 0, 0));
          }
          path.Add(tile.Position);
          if (tile.GetTileType() == TileType.JUNCTION)
          {
            return (tile, path, rotate);
          }
          break;
        }
      }
    }
    return (tile, path, rotate);
  }
}
