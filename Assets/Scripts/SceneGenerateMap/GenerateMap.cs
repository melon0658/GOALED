using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
  [SerializeField] private GameObject straightTilePrefab;
  [SerializeField] private GameObject cornerTilePrefab;
  [SerializeField] private GameObject junctionTilePrefab;
  [SerializeField] private GameObject checkpointPrefab;
  [SerializeField] private GameObject SmallJunctionTilePrefab;
  [SerializeField] private GameObject largeStraightTilePrefab;
  [SerializeField] private GameObject textPrefab;
  private Tile startTile;
  public Tile StartTile { get => startTile; }
  private Vector2 JunctionTileSize = new Vector2(75 + (75 / 2), 75 + (75 / 2));
  private Vector2 NormalTileSize = new Vector2(75, 75);

  private void ConnectTiles(Tile tile1, Tile tile2, Direction direction)
  {
    tile1.SetNeighbor(direction, Passable.PASSABLE, tile2);
    tile2.SetNeighbor(direction.Opposite(), Passable.BLOCKED, tile1);
  }

  public void GenerateTiles()
  {
    startTile = new Tile(0);
    startTile.Position = new Vector3(0, 0, 0);
    startTile.tileType = TileType.LARGE_JUNCTION;
    Tile tile1 = new Tile(1);
    Tile tile2 = new Tile(2);
    Tile tile3 = new Tile(3);
    Tile tile4 = new Tile(4);
    Tile tile5 = new Tile(5);
    Tile tile6 = new Tile(6);
    Tile tile7 = new Tile(7);
    Tile tile8 = new Tile(8);
    Tile tile9 = new Tile(9);
    Tile tile10 = new Tile(10);
    Tile tile11 = new Tile(11);
    Tile tile12 = new Tile(12);
    Tile tile13 = new Tile(13);
    Tile tile14 = new Tile(14);
    Tile tile15 = new Tile(15);
    Tile tile16 = new Tile(16);
    Tile tile17 = new Tile(17);
    Tile tile18 = new Tile(18);
    Tile tile19 = new Tile(19);
    Tile tile20 = new Tile(20);
    Tile tile21 = new Tile(21);
    Tile tile22 = new Tile(22);
    Tile tile23 = new Tile(23);
    // Tile tile24 = new Tile(24);
    Tile tile25 = new Tile(25);
    Tile tile26 = new Tile(26);
    Tile tile27 = new Tile(27);
    Tile tile28 = new Tile(28);
    Tile tile29 = new Tile(29);
    Tile tile30 = new Tile(30);
    Tile tile31 = new Tile(31);
    Tile tile32 = new Tile(32);
    Tile tile33 = new Tile(33);
    Tile tile34 = new Tile(34);
    Tile tile35 = new Tile(35);
    Tile tile36 = new Tile(36);
    Tile tile37 = new Tile(37);
    Tile tile38 = new Tile(38);
    Tile tile39 = new Tile(39);
    Tile tile40 = new Tile(40);
    Tile tile41 = new Tile(41);
    Tile tile42 = new Tile(42);
    Tile tile43 = new Tile(43);
    Tile tile44 = new Tile(44);
    Tile tile45 = new Tile(45);
    Tile tile46 = new Tile(46);
    Tile tile47 = new Tile(47);
    Tile tile48 = new Tile(48);
    Tile tile49 = new Tile(49);
    Tile tile50 = new Tile(50);
    Tile tile51 = new Tile(51);
    Tile tile52 = new Tile(52);
    Tile tile53 = new Tile(53);
    Tile tile54 = new Tile(54);
    Tile tile55 = new Tile(55);
    Tile tile56 = new Tile(56);
    Tile tile57 = new Tile(57);
    Tile tile58 = new Tile(58);
    Tile tile59 = new Tile(59);
    Tile tile60 = new Tile(60);
    Tile tile61 = new Tile(61);
    Tile tile62 = new Tile(62);
    Tile tile63 = new Tile(63);
    Tile tile64 = new Tile(64);
    Tile tile65 = new Tile(65);
    Tile tile66 = new Tile(66);
    Tile tile67 = new Tile(67);
    Tile tile68 = new Tile(68);
    Tile tile69 = new Tile(69);
    Tile tile70 = new Tile(70);
    Tile tile71 = new Tile(71);
    Tile tile72 = new Tile(72);
    Tile tile73 = new Tile(73);
    Tile tile74 = new Tile(74);
    Tile tile75 = new Tile(75);
    Tile tile76 = new Tile(76);
    Tile tile77 = new Tile(77);
    Tile tile78 = new Tile(78);
    Tile tile79 = new Tile(79);
    Tile tile80 = new Tile(80);
    Tile tile81 = new Tile(81);
    Tile tile82 = new Tile(82);
    Tile tile83 = new Tile(83);
    Tile tile84 = new Tile(84);
    Tile tile85 = new Tile(85);
    Tile tile86 = new Tile(86);


    ConnectTiles(startTile, tile1, Direction.POSITIVE_X);
    ConnectTiles(tile1, tile2, Direction.POSITIVE_X);
    ConnectTiles(tile2, tile3, Direction.POSITIVE_Z);
    ConnectTiles(tile3, tile12, Direction.POSITIVE_Z);

    ConnectTiles(startTile, tile4, Direction.NEGATIVE_X);
    ConnectTiles(tile4, tile5, Direction.NEGATIVE_X);
    ConnectTiles(tile5, tile6, Direction.POSITIVE_Z);
    ConnectTiles(tile6, tile7, Direction.POSITIVE_Z);
    ConnectTiles(tile7, tile8, Direction.POSITIVE_X);
    ConnectTiles(tile8, tile9, Direction.POSITIVE_X);
    ConnectTiles(tile9, tile10, Direction.POSITIVE_X);
    ConnectTiles(tile10, tile11, Direction.POSITIVE_X);
    ConnectTiles(tile11, tile12, Direction.POSITIVE_X);
    ConnectTiles(tile12, tile13, Direction.POSITIVE_X);
    ConnectTiles(tile13, tile14, Direction.POSITIVE_Z);
    ConnectTiles(tile14, tile15, Direction.POSITIVE_Z);
    ConnectTiles(tile15, tile16, Direction.POSITIVE_Z);
    ConnectTiles(tile16, tile17, Direction.POSITIVE_Z);
    ConnectTiles(tile17, tile18, Direction.POSITIVE_Z);
    ConnectTiles(tile18, tile19, Direction.NEGATIVE_X);
    tile19.CheckPoint = true;
    ConnectTiles(tile19, tile20, Direction.NEGATIVE_X);
    ConnectTiles(tile20, tile21, Direction.NEGATIVE_X);
    ConnectTiles(tile21, tile22, Direction.POSITIVE_Z);
    ConnectTiles(tile22, tile23, Direction.POSITIVE_Z);
    ConnectTiles(tile23, tile25, Direction.POSITIVE_X);
    ConnectTiles(tile25, tile26, Direction.POSITIVE_X);
    ConnectTiles(tile26, tile27, Direction.POSITIVE_X);
    ConnectTiles(tile27, tile28, Direction.POSITIVE_Z);

    ConnectTiles(tile28, tile29, Direction.NEGATIVE_X);
    ConnectTiles(tile29, tile30, Direction.NEGATIVE_X);
    ConnectTiles(tile30, tile31, Direction.NEGATIVE_X);
    ConnectTiles(tile31, tile32, Direction.NEGATIVE_X);
    ConnectTiles(tile32, tile33, Direction.NEGATIVE_X);
    ConnectTiles(tile33, tile34, Direction.NEGATIVE_X);

    ConnectTiles(tile28, tile35, Direction.POSITIVE_Z);
    ConnectTiles(tile35, tile36, Direction.POSITIVE_Z);
    ConnectTiles(tile36, tile37, Direction.POSITIVE_Z);
    ConnectTiles(tile37, tile38, Direction.POSITIVE_Z);
    ConnectTiles(tile38, tile39, Direction.NEGATIVE_X);
    tile39.tileType = TileType.LARGE_STRAIGHT_HORIZONTAL;
    ConnectTiles(tile39, tile40, Direction.NEGATIVE_X);
    ConnectTiles(tile40, tile41, Direction.NEGATIVE_Z);
    ConnectTiles(tile41, tile42, Direction.NEGATIVE_Z);
    ConnectTiles(tile42, tile43, Direction.NEGATIVE_Z);
    ConnectTiles(tile43, tile44, Direction.NEGATIVE_X);
    ConnectTiles(tile44, tile45, Direction.NEGATIVE_X);
    ConnectTiles(tile45, tile46, Direction.POSITIVE_Z);
    ConnectTiles(tile46, tile47, Direction.POSITIVE_Z);
    ConnectTiles(tile47, tile48, Direction.POSITIVE_Z);
    ConnectTiles(tile48, tile49, Direction.NEGATIVE_X);
    ConnectTiles(tile49, tile50, Direction.NEGATIVE_X);
    ConnectTiles(tile50, tile51, Direction.NEGATIVE_Z);
    tile51.tileType = TileType.LARGE_STRAIGHT_VERTICAL;
    ConnectTiles(tile51, tile52, Direction.NEGATIVE_Z);

    ConnectTiles(tile52, tile53, Direction.NEGATIVE_Z);
    ConnectTiles(tile34, tile53, Direction.POSITIVE_Z);

    ConnectTiles(tile53, tile54, Direction.NEGATIVE_X);
    ConnectTiles(tile54, tile55, Direction.NEGATIVE_X);
    ConnectTiles(tile55, tile56, Direction.NEGATIVE_X);
    ConnectTiles(tile56, tile57, Direction.NEGATIVE_X);
    ConnectTiles(tile57, tile58, Direction.NEGATIVE_X);
    ConnectTiles(tile58, tile59, Direction.NEGATIVE_X);
    ConnectTiles(tile59, tile60, Direction.NEGATIVE_X);
    ConnectTiles(tile60, tile61, Direction.NEGATIVE_Z);

    ConnectTiles(tile61, tile62, Direction.NEGATIVE_Z);

    ConnectTiles(tile62, tile63, Direction.POSITIVE_X);
    ConnectTiles(tile63, tile64, Direction.POSITIVE_X);
    ConnectTiles(tile64, tile65, Direction.POSITIVE_X);
    ConnectTiles(tile65, tile66, Direction.NEGATIVE_Z);
    ConnectTiles(tile66, tile67, Direction.NEGATIVE_Z);
    tile67.tileType = TileType.LARGE_STRAIGHT_VERTICAL;
    ConnectTiles(tile67, tile68, Direction.NEGATIVE_Z);
    ConnectTiles(tile68, tile69, Direction.NEGATIVE_Z);
    ConnectTiles(tile69, tile70, Direction.NEGATIVE_X);
    ConnectTiles(tile70, tile71, Direction.NEGATIVE_X);
    tile71.tileType = TileType.LARGE_STRAIGHT_HORIZONTAL;
    ConnectTiles(tile71, tile75, Direction.NEGATIVE_X);

    ConnectTiles(tile62, tile72, Direction.NEGATIVE_Z);
    ConnectTiles(tile72, tile73, Direction.NEGATIVE_Z);
    ConnectTiles(tile73, tile74, Direction.NEGATIVE_Z);
    ConnectTiles(tile74, tile75, Direction.NEGATIVE_Z);
    ConnectTiles(tile75, tile76, Direction.NEGATIVE_Z);
    ConnectTiles(tile76, tile77, Direction.NEGATIVE_Z);
    ConnectTiles(tile77, tile78, Direction.NEGATIVE_Z);
    ConnectTiles(tile78, tile79, Direction.NEGATIVE_Z);
    ConnectTiles(tile79, tile80, Direction.NEGATIVE_Z);
    ConnectTiles(tile80, tile81, Direction.POSITIVE_X);
    ConnectTiles(tile81, tile82, Direction.POSITIVE_X);
    ConnectTiles(tile82, tile83, Direction.POSITIVE_X);
    ConnectTiles(tile83, tile84, Direction.POSITIVE_X);
    ConnectTiles(tile84, tile85, Direction.POSITIVE_Z);
    ConnectTiles(tile85, tile86, Direction.POSITIVE_Z);

    Generate();
  }

  void Update()
  {

  }

  private void Generate()
  {
    Queue<Tile> queue = new Queue<Tile>();
    queue.Enqueue(startTile);
    while (queue.Count > 0)
    {
      Tile tile = queue.Dequeue();
      foreach (var neighbor in tile.GetPassebleNeighbors())
      {
        var tileObject = neighbor.Value;
        TileType tileType = tile.GetTileType();
        Vector2 space = tileObject.GetSpace();
        switch (tileType)
        {
          case TileType.LARGE_JUNCTION:
            space = space + new Vector2(37.5f, 37.5f);
            break;
          case TileType.LARGE_STRAIGHT_HORIZONTAL:
            space = space + new Vector2(37.5f / 2, 0);
            break;
          case TileType.LARGE_STRAIGHT_VERTICAL:
            space = space + new Vector2(0, 37.5f / 2);
            break;
        }
        switch (neighbor.Key)
        {
          case Direction.POSITIVE_X:
            tileObject.Position = new Vector3(tile.Position.x + space.x, tile.Position.y, tile.Position.z);
            break;
          case Direction.NEGATIVE_X:
            tileObject.Position = new Vector3(tile.Position.x - space.x, tile.Position.y, tile.Position.z);
            break;
          case Direction.POSITIVE_Z:
            tileObject.Position = new Vector3(tile.Position.x, tile.Position.y, tile.Position.z + space.y);
            break;
          case Direction.NEGATIVE_Z:
            tileObject.Position = new Vector3(tile.Position.x, tile.Position.y, tile.Position.z - space.y);
            break;
        }
        queue.Enqueue(neighbor.Value);
      }
      GenerateTile(tile);
    }
  }

  private void GenerateTile(Tile tile)
  {
    if (tile.IsGenerated)
    {
      return;
    }
    TileType tileType = tile.GetTileType();
    GameObject tileObject = null;
    switch (tileType)
    {
      case TileType.STRAIGHT_HORIZONTAL:
        tileObject = tile.CheckPoint ? Instantiate(checkpointPrefab, tile.Position, tile.GetRotate()) : Instantiate(straightTilePrefab, tile.Position, tile.GetRotate());
        break;
      case TileType.STRAIGHT_VERTICAL:
        tileObject = tile.CheckPoint ? Instantiate(checkpointPrefab, tile.Position, tile.GetRotate()) : Instantiate(straightTilePrefab, tile.Position, tile.GetRotate());
        break;
      case TileType.LARGE_STRAIGHT_HORIZONTAL:
        tileObject = Instantiate(largeStraightTilePrefab, tile.Position, tile.GetRotate());
        break;
      case TileType.LARGE_STRAIGHT_VERTICAL:
        tileObject = Instantiate(largeStraightTilePrefab, tile.Position, tile.GetRotate());
        break;
      case TileType.CORNER:
        tileObject = Instantiate(cornerTilePrefab, tile.Position, tile.GetRotate());
        break;
      case TileType.LARGE_JUNCTION:
        tileObject = Instantiate(junctionTilePrefab, tile.Position, tile.GetRotate());
        break;
      case TileType.SMALL_JUNCTION:
        tileObject = Instantiate(SmallJunctionTilePrefab, tile.Position, tile.GetRotate());
        break;
    }
    Instantiate(textPrefab, tile.Position, Quaternion.Euler(90, 0, 0));
    tileObject.name = tile.Name;
    tile.IsGenerated = true;
  }

  public (Tile, List<Vector3>, List<Vector3>) WayToTile(Tile start, int step, Direction? direction = null)
  {
    if (start == null)
    {
      start = startTile;
    }
    List<Vector3> path = new List<Vector3>();
    List<Vector3> rotate = new List<Vector3>();

    // Tile tile = direction == null ? start : start.GetNeighbor(direction.Value);
    Tile tile = start;
    for (int i = 0; i < step; i++)
    {
      var neighbors = tile.GetPassebleNeighbors();
      if (direction != null)
      {
        neighbors = neighbors.Where(x => x.Key == direction.Value).ToDictionary(x => x.Key, x => x.Value);
        direction = null;
      }
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
          if (tile.GetTileType() == TileType.LARGE_JUNCTION || tile.CheckPoint)
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
