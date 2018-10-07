namespace Dungeon.Core
{
    public class DGlobal
    {
        public const int MAP_DISTANCES_NUM = 5;
        public enum MapDistances
        {
            NONE = 0,
            NEAR = 1,
            MID = 2,
            FAR = 3,
            END = 4
        }

        public const int MAP_POSITIONS_NUM = 5;
        public enum MapPositions
        {
            CENTER = 0,
            LEFT1 = 1,
            LEFT2 = 2,
            RIGHT1 = 3,
            RIGHT2 = 4
        }

        public const int MAP_ORIENTATIONS_NUM = 6;
        public enum MapOrientations
        {
            NORTH = 0,
            EAST = 1,
            SOUTH = 2,
            WEST = 3,
            FLOOR = 4,
            CEIL = 5
        }

        public static string mapDistanceToString(MapDistances distance)
        {
            switch(distance)
            {
                case MapDistances.NONE:
                    return "D0";
                case MapDistances.NEAR:
                    return "D1";
                case MapDistances.MID:
                    return "D2";
                case MapDistances.FAR:
                    return "D3";
                case MapDistances.END:
                    return "D4";
                default:
                    return "";
            }
        }

        public static string mapPositionToString(MapPositions position)
        {
            switch(position)
            {
                case MapPositions.CENTER:
                    return "C";
                case MapPositions.LEFT1:
                    return "L1";
                case MapPositions.LEFT2:
                    return "L2";
                case MapPositions.RIGHT1:
                    return "R1";
                case MapPositions.RIGHT2:
                    return "R2";
                default:
                    return "";
            }
        }

        public static string mapOrientationToString(MapOrientations orientation)
        {
            switch(orientation)
            {
                case MapOrientations.NORTH:
                    return "N";
                case MapOrientations.EAST:
                    return "E";
                case MapOrientations.SOUTH:
                    return "S";
                case MapOrientations.WEST:
                    return "W";
                case MapOrientations.FLOOR:
                    return "F";
                case MapOrientations.CEIL:
                    return "C";
                default:
                    return "";
            }
        }

        public static void turnLeft(ref DGlobal.MapOrientations orientation)
        {
            switch(orientation)
            {
                case MapOrientations.NORTH:
                    orientation = MapOrientations.WEST;
                    break;
                case MapOrientations.EAST:
                    orientation = MapOrientations.NORTH;
                    break;
                case MapOrientations.SOUTH:
                    orientation = MapOrientations.EAST;
                    break;
                case MapOrientations.WEST:
                    orientation = MapOrientations.SOUTH;
                    break;
            }
        }

        public static void turnRight(ref DGlobal.MapOrientations orientation)
        {
            switch (orientation)
            {
                case MapOrientations.NORTH:
                    orientation = MapOrientations.EAST;
                    break;
                case MapOrientations.EAST:
                    orientation = MapOrientations.SOUTH;
                    break;
                case MapOrientations.SOUTH:
                    orientation = MapOrientations.WEST;
                    break;
                case MapOrientations.WEST:
                    orientation = MapOrientations.NORTH;
                    break;
            }
        }

        public static void turnBack(ref DGlobal.MapOrientations orientation)
        {
            switch (orientation)
            {
                case MapOrientations.NORTH:
                    orientation = MapOrientations.SOUTH;
                    break;
                case MapOrientations.EAST:
                    orientation = MapOrientations.WEST;
                    break;
                case MapOrientations.SOUTH:
                    orientation = MapOrientations.NORTH;
                    break;
                case MapOrientations.WEST:
                    orientation = MapOrientations.EAST;
                    break;
            }
        }

        public static void moveForward(DGlobal.MapOrientations orientation, ref int x, ref int y, int distance)
        {
            switch (orientation)
            {
                case DGlobal.MapOrientations.NORTH:
                    y -= distance;
                    break;
                case DGlobal.MapOrientations.EAST:
                    x += distance;
                    break;
                case DGlobal.MapOrientations.SOUTH:
                    y += distance;
                    break;
                case DGlobal.MapOrientations.WEST:
                    x -= distance;
                    break;
            }
        }

        public static void moveBack(DGlobal.MapOrientations orientation, ref int x, ref int y, int distance)
        {
            switch (orientation)
            {
                case DGlobal.MapOrientations.NORTH:
                    y += distance;
                    break;
                case DGlobal.MapOrientations.EAST:
                    x -= distance;
                    break;
                case DGlobal.MapOrientations.SOUTH:
                    y -= distance;
                    break;
                case DGlobal.MapOrientations.WEST:
                    x += distance;
                    break;
            }
        }

        public static void moveLeft(DGlobal.MapOrientations orientation, ref int x, ref int y, int distance)
        {
            switch (orientation)
            {
                case DGlobal.MapOrientations.NORTH:
                    x -= distance;
                    break;
                case DGlobal.MapOrientations.EAST:
                    y -= distance;
                    break;
                case DGlobal.MapOrientations.SOUTH:
                    x += distance;
                    break;
                case DGlobal.MapOrientations.WEST:
                    y += distance;
                    break;
            }
        }

        public static void moveRight(DGlobal.MapOrientations orientation, ref int x, ref int y, int distance)
        {
            switch (orientation)
            {
                case DGlobal.MapOrientations.NORTH:
                    x += distance;
                    break;
                case DGlobal.MapOrientations.EAST:
                    y += distance;
                    break;
                case DGlobal.MapOrientations.SOUTH:
                    x -= distance;
                    break;
                case DGlobal.MapOrientations.WEST:
                    y -= distance;
                    break;
            }
        }
    }
}
