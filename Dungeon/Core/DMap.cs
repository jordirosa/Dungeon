using System.IO;

using SDLEngine;

namespace Dungeon.Core
{
    class DMap
    {
        private const string mapAssetsPath = "data/map";
        private const string mapAssetsExtension = "dmap";

        private string mName;

        private int mWidth;
        private int mHeight;

        private float mScale;

        private SDLImage[,,,] mTileset;
        private int[,,] mMapData;

        SDLGraphics mGraphics;

        public DMap(SDLGraphics graphics)
        {
            this.clearMap();

            this.mGraphics = graphics;
        }

        private void clearMap()
        {
            this.mName = "";

            this.mWidth = 0;
            this.mHeight = 0;

            this.mScale = 1.0f;

            this.mTileset = new SDLImage[1, DGlobal.MAP_DISTANCES_NUM, DGlobal.MAP_POSITIONS_NUM, DGlobal.MAP_ORIENTATIONS_NUM];
        }

        public void loadMap(string assetName)
        {
            string fileFullPath;
            string[] mapSize;
            string[] mapDataLine;
            string[] mapDataSplitted;

            this.clearMap();

            fileFullPath = mapAssetsPath + "/" + assetName + "." + mapAssetsExtension;
            StreamReader mapFile = new StreamReader(new FileStream(fileFullPath, FileMode.Open));

            this.mName = mapFile.ReadLine();
            this.loadTileset(mapFile.ReadLine());

            mapSize = mapFile.ReadLine().Split('x');
            this.mWidth = int.Parse(mapSize[0]);
            this.mHeight = int.Parse(mapSize[1]);
            this.mMapData = new int[this.mWidth, this.mHeight, DGlobal.MAP_ORIENTATIONS_NUM];
            for(int y = 0; y < this.mHeight; y++)
            {
                mapDataLine = mapFile.ReadLine().Split('_');
                for (int x = 0; x < this.mWidth; x++)
                {
                    mapDataSplitted = mapDataLine[x].Split('.');

                    mMapData[x, y, (int)DGlobal.MapOrientations.NORTH] = int.Parse(mapDataSplitted[0]);
                    mMapData[x, y, (int)DGlobal.MapOrientations.EAST] = int.Parse(mapDataSplitted[1]);
                    mMapData[x, y, (int)DGlobal.MapOrientations.SOUTH] = int.Parse(mapDataSplitted[2]);
                    mMapData[x, y, (int)DGlobal.MapOrientations.WEST] = int.Parse(mapDataSplitted[3]);
                    mMapData[x, y, (int)DGlobal.MapOrientations.FLOOR] = int.Parse(mapDataSplitted[4]);
                    mMapData[x, y, (int)DGlobal.MapOrientations.CEIL] = int.Parse(mapDataSplitted[5]);
                }
            }
        }

        public void loadTileset(string assetName)
        {
            //Position: CENTER
            loadTile(DGlobal.MapDistances.NONE, DGlobal.MapPositions.CENTER, assetName);
            loadTile(DGlobal.MapDistances.NEAR, DGlobal.MapPositions.CENTER, assetName);
            loadTile(DGlobal.MapDistances.MID, DGlobal.MapPositions.CENTER, assetName);
            loadTile(DGlobal.MapDistances.FAR, DGlobal.MapPositions.CENTER, assetName);
            loadTile(DGlobal.MapDistances.END, DGlobal.MapPositions.CENTER, assetName);

            //Position: LEFT 1
            loadTile(DGlobal.MapDistances.NONE, DGlobal.MapPositions.LEFT1, assetName);
            loadTile(DGlobal.MapDistances.NEAR, DGlobal.MapPositions.LEFT1, assetName);
            loadTile(DGlobal.MapDistances.MID, DGlobal.MapPositions.LEFT1, assetName);
            loadTile(DGlobal.MapDistances.FAR, DGlobal.MapPositions.LEFT1, assetName);
            loadTile(DGlobal.MapDistances.END, DGlobal.MapPositions.LEFT1, assetName);

            //Position: LEFT 2
            loadTile(DGlobal.MapDistances.NONE, DGlobal.MapPositions.LEFT2, assetName);
            loadTile(DGlobal.MapDistances.NEAR, DGlobal.MapPositions.LEFT2, assetName);
            loadTile(DGlobal.MapDistances.MID, DGlobal.MapPositions.LEFT2, assetName);
            loadTile(DGlobal.MapDistances.FAR, DGlobal.MapPositions.LEFT2, assetName);
            loadTile(DGlobal.MapDistances.END, DGlobal.MapPositions.LEFT2, assetName);

            //Position: RIGHT 1
            loadTile(DGlobal.MapDistances.NONE, DGlobal.MapPositions.RIGHT1, assetName);
            loadTile(DGlobal.MapDistances.NEAR, DGlobal.MapPositions.RIGHT1, assetName);
            loadTile(DGlobal.MapDistances.MID, DGlobal.MapPositions.RIGHT1, assetName);
            loadTile(DGlobal.MapDistances.FAR, DGlobal.MapPositions.RIGHT1, assetName);
            loadTile(DGlobal.MapDistances.END, DGlobal.MapPositions.RIGHT1, assetName);

            //Position: RIGHT 2
            loadTile(DGlobal.MapDistances.NONE, DGlobal.MapPositions.RIGHT2, assetName);
            loadTile(DGlobal.MapDistances.NEAR, DGlobal.MapPositions.RIGHT2, assetName);
            loadTile(DGlobal.MapDistances.MID, DGlobal.MapPositions.RIGHT2, assetName);
            loadTile(DGlobal.MapDistances.FAR, DGlobal.MapPositions.RIGHT2, assetName);
            loadTile(DGlobal.MapDistances.END, DGlobal.MapPositions.RIGHT2, assetName);

            foreach (SDLImage image in mTileset)
            {
                if (image != null)
                {
                    image.scale = this.mScale;
                }
            }
        }

        public void loadTile(DGlobal.MapDistances distance, DGlobal.MapPositions position, string assetName)
        {
            mTileset[0, (int)distance, (int)position, (int)DGlobal.MapOrientations.NORTH] = this.mGraphics.loadImage(buildImageFullPath(assetName, distance, position, DGlobal.MapOrientations.NORTH));
            mTileset[0, (int)distance, (int)position, (int)DGlobal.MapOrientations.EAST] = this.mGraphics.loadImage(buildImageFullPath(assetName, distance, position, DGlobal.MapOrientations.EAST));
            mTileset[0, (int)distance, (int)position, (int)DGlobal.MapOrientations.SOUTH] = this.mGraphics.loadImage(buildImageFullPath(assetName, distance, position, DGlobal.MapOrientations.SOUTH));
            mTileset[0, (int)distance, (int)position, (int)DGlobal.MapOrientations.WEST] = this.mGraphics.loadImage(buildImageFullPath(assetName, distance, position, DGlobal.MapOrientations.WEST));
            mTileset[0, (int)distance, (int)position, (int)DGlobal.MapOrientations.FLOOR] = this.mGraphics.loadImage(buildImageFullPath(assetName, distance, position, DGlobal.MapOrientations.FLOOR));
            mTileset[0, (int)distance, (int)position, (int)DGlobal.MapOrientations.CEIL] = this.mGraphics.loadImage(buildImageFullPath(assetName, distance, position, DGlobal.MapOrientations.CEIL));
        }

        private string buildImageFullPath(string assetName, DGlobal.MapDistances distance, DGlobal.MapPositions position, DGlobal.MapOrientations orientation)
        {
            string imageFullPath;

            imageFullPath = "data/img/tiles/" + assetName + "_" + DGlobal.mapDistanceToString(distance) + "_" + DGlobal.mapPositionToString(position) + "_" + DGlobal.mapOrientationToString(orientation) + ".png";

            return imageFullPath;
        }

        public void draw(int x, int y, DGlobal.MapOrientations orientation)
        {
            this.drawTile(x, y, DGlobal.MapDistances.END, DGlobal.MapPositions.CENTER, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.END, DGlobal.MapPositions.LEFT1, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.END, DGlobal.MapPositions.LEFT2, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.END, DGlobal.MapPositions.RIGHT1, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.END, DGlobal.MapPositions.RIGHT2, orientation);

            this.drawTile(x, y, DGlobal.MapDistances.FAR, DGlobal.MapPositions.CENTER, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.FAR, DGlobal.MapPositions.LEFT1, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.END, DGlobal.MapPositions.LEFT2, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.FAR, DGlobal.MapPositions.RIGHT1, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.END, DGlobal.MapPositions.RIGHT2, orientation);

            this.drawTile(x, y, DGlobal.MapDistances.MID, DGlobal.MapPositions.CENTER, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.MID, DGlobal.MapPositions.LEFT1, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.MID, DGlobal.MapPositions.LEFT2, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.MID, DGlobal.MapPositions.RIGHT1, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.MID, DGlobal.MapPositions.RIGHT2, orientation);

            this.drawTile(x, y, DGlobal.MapDistances.NEAR, DGlobal.MapPositions.CENTER, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.NEAR, DGlobal.MapPositions.LEFT1, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.NEAR, DGlobal.MapPositions.LEFT2, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.NEAR, DGlobal.MapPositions.RIGHT1, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.NEAR, DGlobal.MapPositions.RIGHT2, orientation);

            this.drawTile(x, y, DGlobal.MapDistances.NONE, DGlobal.MapPositions.CENTER, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.NONE, DGlobal.MapPositions.LEFT1, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.NONE, DGlobal.MapPositions.LEFT2, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.NONE, DGlobal.MapPositions.RIGHT1, orientation);
            this.drawTile(x, y, DGlobal.MapDistances.NONE, DGlobal.MapPositions.RIGHT2, orientation);
        }

        public void drawTile(int x, int y, DGlobal.MapDistances distance, DGlobal.MapPositions position, DGlobal.MapOrientations lookOrientation)
        {
            DGlobal.MapOrientations orientation = lookOrientation;

            if (position == DGlobal.MapPositions.CENTER)
            {
                this.drawTileOrientation(x, y, distance, position, orientation, lookOrientation);
                DGlobal.turnLeft(ref orientation);
                this.drawTileOrientation(x, y, distance, position, orientation, lookOrientation);
                DGlobal.turnRight(ref orientation);
                DGlobal.turnRight(ref orientation);
                this.drawTileOrientation(x, y, distance, position, orientation, lookOrientation);
                this.drawTileOrientation(x, y, distance, position, DGlobal.MapOrientations.FLOOR, lookOrientation);
                this.drawTileOrientation(x, y, distance, position, DGlobal.MapOrientations.CEIL, lookOrientation);
                DGlobal.turnRight(ref orientation);
                this.drawTileOrientation(x, y, distance, position, orientation, lookOrientation);
            }
            else if(position == DGlobal.MapPositions.RIGHT1 || position == DGlobal.MapPositions.RIGHT2)
            {
                this.drawTileOrientation(x, y, distance, position, orientation, lookOrientation);
                DGlobal.turnRight(ref orientation);
                this.drawTileOrientation(x, y, distance, position, orientation, lookOrientation);
                DGlobal.turnRight(ref orientation);
                this.drawTileOrientation(x, y, distance, position, orientation, lookOrientation);
                this.drawTileOrientation(x, y, distance, position, DGlobal.MapOrientations.FLOOR, lookOrientation);
                this.drawTileOrientation(x, y, distance, position, DGlobal.MapOrientations.CEIL, lookOrientation);
                DGlobal.turnRight(ref orientation);
                this.drawTileOrientation(x, y, distance, position, orientation, lookOrientation);
            }
            else if (position == DGlobal.MapPositions.LEFT1 || position == DGlobal.MapPositions.LEFT2)
            {
                this.drawTileOrientation(x, y, distance, position, orientation, lookOrientation);
                DGlobal.turnLeft(ref orientation);
                this.drawTileOrientation(x, y, distance, position, orientation, lookOrientation);
                DGlobal.turnLeft(ref orientation);
                this.drawTileOrientation(x, y, distance, position, orientation, lookOrientation);
                this.drawTileOrientation(x, y, distance, position, DGlobal.MapOrientations.FLOOR, lookOrientation);
                this.drawTileOrientation(x, y, distance, position, DGlobal.MapOrientations.CEIL, lookOrientation);
                DGlobal.turnLeft(ref orientation);
                this.drawTileOrientation(x, y, distance, position, orientation, lookOrientation);
            }
        }

        public void drawTileOrientation(int x, int y, DGlobal.MapDistances distance, DGlobal.MapPositions position, DGlobal.MapOrientations orientation, DGlobal.MapOrientations lookOrientation)
        {
            int positionX = x;
            int positionY = y;

            DGlobal.MapOrientations drawingOrientation = orientation;

            switch(lookOrientation)
            {       
                case DGlobal.MapOrientations.WEST:
                    DGlobal.turnRight(ref drawingOrientation);
                    break;
                case DGlobal.MapOrientations.EAST:
                    DGlobal.turnLeft(ref drawingOrientation);
                    break;
                case DGlobal.MapOrientations.SOUTH:
                    DGlobal.turnBack(ref drawingOrientation);
                    break;
            }

            switch(position)
            {
                case DGlobal.MapPositions.RIGHT1:
                    DGlobal.moveRight(lookOrientation, ref positionX, ref positionY, 1);
                    break;
                case DGlobal.MapPositions.RIGHT2:
                    DGlobal.moveRight(lookOrientation, ref positionX, ref positionY, 2);
                    break;
                case DGlobal.MapPositions.LEFT1:
                    DGlobal.moveLeft(lookOrientation, ref positionX, ref positionY, 1);
                    break;
                case DGlobal.MapPositions.LEFT2:
                    DGlobal.moveLeft(lookOrientation, ref positionX, ref positionY, 2);
                    break;
            }

            DGlobal.moveForward(lookOrientation, ref positionX, ref positionY, (int)distance);

            if (positionX >= 0 && positionX < this.mWidth && positionY >= 0 && positionY < this.mHeight)
            {
                if (this.mMapData[positionX, positionY, (int)orientation] > 0 && this.mTileset[(this.mMapData[positionX, positionY, (int)orientation]) - 1, (int)distance, (int)position, (int)drawingOrientation] != null)
                {
                    this.mGraphics.drawImage(this.mTileset[(this.mMapData[positionX, positionY, (int)orientation]) - 1, (int)distance, (int)position, (int)drawingOrientation]);
                }
            }
        }

        public bool checkWalkable(int x, int y, DGlobal.MapOrientations orientation)
        {
            if (x < this.mWidth && x >= 0 && y < this.mHeight && y >= 0)
            {
                if (this.mMapData[x, y, (int)orientation] == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
