using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
  POSITIVE_X,
  NEGATIVE_Z,
  NEGATIVE_X,
  POSITIVE_Z,
}

public enum Passable
{
  PASSABLE,
  BLOCKED
}

public enum TileType
{
  STRAIGHT_HORIZONTAL,
  STRAIGHT_VERTICAL,
  CORNER,
  JUNCTION
}

public class Tile
{
  private Dictionary<Direction, Tile> neighbors = new Dictionary<Direction, Tile>();
  private Dictionary<Direction, Passable> passableTile = new Dictionary<Direction, Passable>();
  private Vector3 position = new Vector3(0, 0, 0);
  public Vector3 Position { get { return position; } set { position = value; } }
  private bool checkPoint = false;
  public bool CheckPoint { get { return checkPoint; } set { checkPoint = value; } }
  private int eventId = -1;
  public int EventId { get { return eventId; } set { eventId = value; } }
  public Event Event { get; set; } = new DefaultEvent();

  public Tile()
  {
    for (int i = 0; i < 4; i++)
    {
      neighbors.Add((Direction)i, null);
      passableTile.Add((Direction)i, Passable.BLOCKED);
    }
  }

  public void SetNeighbor(Direction direction, Passable passable, Tile tile)
  {
    neighbors[direction] = tile;
    this.passableTile[direction] = passable;
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
      if (passableTile[neighbor.Key] == Passable.PASSABLE)
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
    if (new List<Tile>(neighbors.Values).FindAll(x => x != null).Count >= 3)
    {
      return TileType.JUNCTION;
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

  public Quaternion GetRotate()
  {
    var tileType = GetTileType();
    if (tileType == TileType.STRAIGHT_HORIZONTAL)
    {
      return Quaternion.Euler(0, 0, 0);
    }
    else if (tileType == TileType.STRAIGHT_VERTICAL)
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
