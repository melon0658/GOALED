using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIRECTION
{
  POSITIVE_X,
  NEGATIVE_X,
  POSITIVE_Z,
  NEGATIVE_Z
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
  private Dictionary<DIRECTION, Tile> neighbors = new Dictionary<DIRECTION, Tile>();
  private Dictionary<DIRECTION, Passable> passableTile = new Dictionary<DIRECTION, Passable>();
  private Vector3 position;
  public Vector3 Position { get { return position; } set { position = value; } }

  public Tile()
  {
    position = new Vector3(0, 0, 0);
    neighbors.Add(DIRECTION.POSITIVE_X, null);
    neighbors.Add(DIRECTION.NEGATIVE_X, null);
    neighbors.Add(DIRECTION.POSITIVE_Z, null);
    neighbors.Add(DIRECTION.NEGATIVE_Z, null);
    passableTile.Add(DIRECTION.POSITIVE_X, Passable.BLOCKED);
    passableTile.Add(DIRECTION.NEGATIVE_X, Passable.BLOCKED);
    passableTile.Add(DIRECTION.POSITIVE_Z, Passable.BLOCKED);
    passableTile.Add(DIRECTION.NEGATIVE_Z, Passable.BLOCKED);
  }

  public void SetNeighbor(DIRECTION direction, Passable passable, Tile tile)
  {
    neighbors[direction] = tile;
    this.passableTile[direction] = passable;
  }

  public Dictionary<DIRECTION, Tile> GetNeighbors()
  {
    return neighbors;
  }

  public Dictionary<DIRECTION, Tile> GetPassebleNeighbors()
  {
    Dictionary<DIRECTION, Tile> passableNeighbors = new Dictionary<DIRECTION, Tile>();
    foreach (var neighbor in neighbors)
    {
      if (passableTile[neighbor.Key] == Passable.PASSABLE)
      {
        passableNeighbors.Add(neighbor.Key, neighbor.Value);
      }
    }
    return passableNeighbors;
  }

  public Tile GetNeighbor(DIRECTION direction)
  {
    return neighbors[direction];
  }

  public TileType GetTileType()
  {
    if (new List<Tile>(neighbors.Values).FindAll(x => x != null).Count >= 3)
    {
      return TileType.JUNCTION;
    }
    else if (neighbors[DIRECTION.POSITIVE_X] != null && neighbors[DIRECTION.NEGATIVE_X] != null)
    {
      return TileType.STRAIGHT_HORIZONTAL;
    }
    else if (neighbors[DIRECTION.POSITIVE_Z] != null && neighbors[DIRECTION.NEGATIVE_Z] != null)
    {
      return TileType.STRAIGHT_VERTICAL;
    }
    else if ((neighbors[DIRECTION.POSITIVE_X] != null && neighbors[DIRECTION.POSITIVE_Z] != null) || (neighbors[DIRECTION.NEGATIVE_X] != null && neighbors[DIRECTION.NEGATIVE_Z] != null) || (neighbors[DIRECTION.POSITIVE_X] != null && neighbors[DIRECTION.NEGATIVE_Z] != null) || (neighbors[DIRECTION.NEGATIVE_X] != null && neighbors[DIRECTION.POSITIVE_Z] != null))
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
      if (neighbors[DIRECTION.NEGATIVE_X] != null && neighbors[DIRECTION.POSITIVE_Z] != null)
      {
        return Quaternion.Euler(0, 90, 0);
      }
      else if (neighbors[DIRECTION.POSITIVE_X] != null && neighbors[DIRECTION.POSITIVE_Z] != null)
      {
        return Quaternion.Euler(0, 180, 0);
      }
      else if (neighbors[DIRECTION.NEGATIVE_X] != null && neighbors[DIRECTION.NEGATIVE_Z] != null)
      {
        return Quaternion.Euler(0, 0, 0);
      }
      else if (neighbors[DIRECTION.POSITIVE_X] != null && neighbors[DIRECTION.NEGATIVE_Z] != null)
      {
        return Quaternion.Euler(0, 270, 0);
      }
    }
    return Quaternion.Euler(0, 0, 0);
  }
}
