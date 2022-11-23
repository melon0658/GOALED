public enum Direction
{
  POSITIVE_X,
  NEGATIVE_Z,
  NEGATIVE_X,
  POSITIVE_Z,
}

public static class DirectionExtensions
{
  public static Direction Opposite(this Direction direction)
  {
    switch (direction)
    {
      case Direction.POSITIVE_X:
        return Direction.NEGATIVE_X;
      case Direction.NEGATIVE_Z:
        return Direction.POSITIVE_Z;
      case Direction.NEGATIVE_X:
        return Direction.POSITIVE_X;
      case Direction.POSITIVE_Z:
        return Direction.NEGATIVE_Z;
      default:
        throw new System.Exception("Invalid direction");
    }
  }

  public static Direction GetDirection(float angle)
  {
    if (angle == 0)
    {
      return Direction.POSITIVE_Z;
    }
    else if (angle == 90)
    {
      return Direction.POSITIVE_X;
    }
    else if (angle == 180)
    {
      return Direction.NEGATIVE_Z;
    }
    else if (angle == 270)
    {
      return Direction.NEGATIVE_X;
    }
    return Direction.POSITIVE_X;
  }
}