using System.Collections.Generic;
using UnityEngine;

public enum Passable
{
  PASSABLE,
  BLOCKED
}

public enum TileType
{
  STRAIGHT_HORIZONTAL,
  STRAIGHT_VERTICAL,
  LARGE_STRAIGHT_HORIZONTAL,
  LARGE_STRAIGHT_VERTICAL,
  CORNER,
  LARGE_JUNCTION,
  SMALL_JUNCTION,
}

public class Tile
{
  private Dictionary<Direction, Tile> neighbors = new Dictionary<Direction, Tile>();
  private Dictionary<Direction, Passable> passableTiles = new Dictionary<Direction, Passable>();
  private Vector3 position = new Vector3(0, 0, 0);
  public Vector3 Position { get { return position; } set { position = value; } }
  private bool checkPoint = false;
  public bool CheckPoint { get { return checkPoint; } set { checkPoint = value; } }
  public Event Event { get; set; } = new DefaultEvent();
  public bool IsGenerated { get; set; } = false;
  public TileType? tileType { get; set; } = null;
  public string Name;
  public string id;
  private Vector2 JunctionTileSize = new Vector2(75 + (75 / 2), 75 + (75 / 2));
  private Vector2 NormalTileSize = new Vector2(75, 75);

  public Tile(int id)
  {
    this.id = id.ToString();
    Name = "Tile " + id;
    for (int i = 0; i < 4; i++)
    {
      neighbors.Add((Direction)i, null);
      passableTiles.Add((Direction)i, Passable.BLOCKED);
    }
  }

  public void SetNeighbor(Direction direction, Passable passable, Tile tile)
  {
    neighbors[direction] = tile;
    passableTiles[direction] = passable;
  }

  public Dictionary<Direction, Tile> GetNeighbors()
  {
    return neighbors;
  }

  public Dictionary<Direction, Tile> GetPassebleNeighbors()
  {
    Dictionary<Direction, Tile> passableNeighbors = new Dictionary<Direction, Tile>();
    foreach (var neighbor in neighbors)
    {
      if (passableTiles[neighbor.Key] == Passable.PASSABLE)
      {
        passableNeighbors.Add(neighbor.Key, neighbor.Value);
      }
    }
    return passableNeighbors;
  }

  public Tile GetNeighbor(Direction direction)
  {
    return neighbors[direction];
  }

  public TileType GetTileType()
  {
    if (tileType != null)
    {
      return (TileType)tileType;
    }
    if (new List<Tile>(neighbors.Values).FindAll(x => x != null).Count >= 3)
    {
      if (new List<Tile>(GetPassebleNeighbors().Values).FindAll(x => x != null).Count >= 2)
      {
        return TileType.LARGE_JUNCTION;
      }
      else
      {
        return TileType.SMALL_JUNCTION;
      }
    }
    else if (neighbors[Direction.POSITIVE_X] != null && neighbors[Direction.NEGATIVE_X] != null)
    {
      return TileType.STRAIGHT_HORIZONTAL;
    }
    else if (neighbors[Direction.POSITIVE_Z] != null && neighbors[Direction.NEGATIVE_Z] != null)
    {
      return TileType.STRAIGHT_VERTICAL;
    }
    else if ((neighbors[Direction.POSITIVE_X] != null && neighbors[Direction.POSITIVE_Z] != null) || (neighbors[Direction.NEGATIVE_X] != null && neighbors[Direction.NEGATIVE_Z] != null) || (neighbors[Direction.POSITIVE_X] != null && neighbors[Direction.NEGATIVE_Z] != null) || (neighbors[Direction.NEGATIVE_X] != null && neighbors[Direction.POSITIVE_Z] != null))
    {
      return TileType.CORNER;
    }
    return TileType.STRAIGHT_HORIZONTAL;
  }

  public Vector2 GetSpace()
  {
    TileType tileType = GetTileType();
    if (tileType == TileType.LARGE_JUNCTION)
    {
      return JunctionTileSize;
    }
    else if (tileType == TileType.LARGE_STRAIGHT_HORIZONTAL)
    {
      return new Vector2(NormalTileSize.x * 1.25f, NormalTileSize.y);
    }
    else if (tileType == TileType.LARGE_STRAIGHT_VERTICAL)
    {
      return new Vector2(NormalTileSize.x, NormalTileSize.y * 1.25f);
    }
    else
    {
      return NormalTileSize;
    }
  }

  public Quaternion GetRotate()
  {
    var tileType = GetTileType();
    if (tileType == TileType.STRAIGHT_HORIZONTAL || tileType == TileType.LARGE_STRAIGHT_HORIZONTAL)
    {
      return Quaternion.Euler(0, 0, 0);
    }
    else if (tileType == TileType.STRAIGHT_VERTICAL || tileType == TileType.LARGE_STRAIGHT_VERTICAL)
    {
      return Quaternion.Euler(0, 90, 0);
    }
    else if (tileType == TileType.CORNER)
    {
      if (neighbors[Direction.NEGATIVE_X] != null && neighbors[Direction.POSITIVE_Z] != null)
      {
        return Quaternion.Euler(0, 90, 0);
      }
      else if (neighbors[Direction.POSITIVE_X] != null && neighbors[Direction.POSITIVE_Z] != null)
      {
        return Quaternion.Euler(0, 180, 0);
      }
      else if (neighbors[Direction.NEGATIVE_X] != null && neighbors[Direction.NEGATIVE_Z] != null)
      {
        return Quaternion.Euler(0, 0, 0);
      }
      else if (neighbors[Direction.POSITIVE_X] != null && neighbors[Direction.NEGATIVE_Z] != null)
      {
        return Quaternion.Euler(0, 270, 0);
      }
    }
    return Quaternion.Euler(0, 0, 0);
  }

}