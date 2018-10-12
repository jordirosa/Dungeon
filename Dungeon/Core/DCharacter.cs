using System.IO;

using SDLEngine;

namespace Dungeon.Core
{
    class DCharacter
    {
        private const string characterAssetsPath = "data/character";
        private const string characterAssetsExtension = "dchr";

        private int mPositionX;
        private int mPositionY;

        private float mScale;

        bool mLeftArmEquipped;
        bool mRightArmEquipped;

        private SDLImage[,,,] mCharacterset;

        SDLGraphics mGraphics;

        public DCharacter(SDLGraphics graphics)
        {
            this.clearCharacter();

            this.mGraphics = graphics;
        }

        private void clearCharacter()
        {
            this.mPositionX = 0;
            this.mPositionY = 0;

            this.mScale = 1.0f;

            this.mLeftArmEquipped = false;
            this.mRightArmEquipped = false;

            this.mCharacterset = new SDLImage[1, DGlobal.MAP_DISTANCES_NUM, DGlobal.MAP_POSITIONS_NUM, DGlobal.CHARACTER_PIECES_NUM];
        }

        public void loadCharacter(string assetName)
        {
            string fileFullPath;

            this.clearCharacter();

            fileFullPath = characterAssetsPath + "/" + assetName + "." + characterAssetsExtension;
            StreamReader characterFile = new StreamReader(new FileStream(fileFullPath, FileMode.Open));

            this.loadCharacterset(characterFile.ReadLine());

            characterFile.Close();
        }

        private void loadCharacterset(string assetName)
        {
            //Position: CENTER
            loadTile(DGlobal.MapDistances.NEAR, DGlobal.MapPositions.CENTER, assetName);
            loadTile(DGlobal.MapDistances.MID, DGlobal.MapPositions.CENTER, assetName);
            loadTile(DGlobal.MapDistances.FAR, DGlobal.MapPositions.CENTER, assetName);
            loadTile(DGlobal.MapDistances.END, DGlobal.MapPositions.CENTER, assetName);

            //Position: LEFT 1
            loadTile(DGlobal.MapDistances.NEAR, DGlobal.MapPositions.LEFT1, assetName);
            loadTile(DGlobal.MapDistances.MID, DGlobal.MapPositions.LEFT1, assetName);
            loadTile(DGlobal.MapDistances.FAR, DGlobal.MapPositions.LEFT1, assetName);
            loadTile(DGlobal.MapDistances.END, DGlobal.MapPositions.LEFT1, assetName);

            //Position: LEFT 2
            loadTile(DGlobal.MapDistances.NEAR, DGlobal.MapPositions.LEFT2, assetName);
            loadTile(DGlobal.MapDistances.MID, DGlobal.MapPositions.LEFT2, assetName);
            loadTile(DGlobal.MapDistances.FAR, DGlobal.MapPositions.LEFT2, assetName);
            loadTile(DGlobal.MapDistances.END, DGlobal.MapPositions.LEFT2, assetName);

            //Position: RIGHT 1
            loadTile(DGlobal.MapDistances.NEAR, DGlobal.MapPositions.RIGHT1, assetName);
            loadTile(DGlobal.MapDistances.MID, DGlobal.MapPositions.RIGHT1, assetName);
            loadTile(DGlobal.MapDistances.FAR, DGlobal.MapPositions.RIGHT1, assetName);
            loadTile(DGlobal.MapDistances.END, DGlobal.MapPositions.RIGHT1, assetName);

            //Position: RIGHT 2
            loadTile(DGlobal.MapDistances.NEAR, DGlobal.MapPositions.RIGHT2, assetName);
            loadTile(DGlobal.MapDistances.MID, DGlobal.MapPositions.RIGHT2, assetName);
            loadTile(DGlobal.MapDistances.FAR, DGlobal.MapPositions.RIGHT2, assetName);
            loadTile(DGlobal.MapDistances.END, DGlobal.MapPositions.RIGHT2, assetName);

            foreach (SDLImage image in mCharacterset)
            {
                if (image != null)
                {
                    image.scale = this.mScale;
                }
            }
        }

        private void loadTile(DGlobal.MapDistances distance, DGlobal.MapPositions position, string assetName)
        {
            mCharacterset[0, (int)distance, (int)position, (int)DGlobal.MapOrientations.NORTH] = this.mGraphics.loadImage(buildImageFullPath(assetName, distance, position, DGlobal.CharacterPieces.BODY));
            mCharacterset[0, (int)distance, (int)position, (int)DGlobal.MapOrientations.EAST] = this.mGraphics.loadImage(buildImageFullPath(assetName, distance, position, DGlobal.CharacterPieces.ARM_LEFT));
            mCharacterset[0, (int)distance, (int)position, (int)DGlobal.MapOrientations.SOUTH] = this.mGraphics.loadImage(buildImageFullPath(assetName, distance, position, DGlobal.CharacterPieces.ARM_RIGHT));
            mCharacterset[0, (int)distance, (int)position, (int)DGlobal.MapOrientations.WEST] = this.mGraphics.loadImage(buildImageFullPath(assetName, distance, position, DGlobal.CharacterPieces.ARM_LEFT_EQUIPPED));
            mCharacterset[0, (int)distance, (int)position, (int)DGlobal.MapOrientations.FLOOR] = this.mGraphics.loadImage(buildImageFullPath(assetName, distance, position, DGlobal.CharacterPieces.ARM_RIGHT_EQUIPPED));
        }

        private string buildImageFullPath(string assetName, DGlobal.MapDistances distance, DGlobal.MapPositions position, DGlobal.CharacterPieces piece)
        {
            string imageFullPath;

            imageFullPath = "data/img/character/" + assetName + "_" + DGlobal.mapDistanceToString(distance) + "_" + DGlobal.mapPositionToString(position) + "_" + DGlobal.characterPieceToString(piece) + ".png";

            return imageFullPath;
        }

        public void draw(DGlobal.MapDistances distance, DGlobal.MapPositions position)
        {
            if (this.mCharacterset[0, (int)distance, (int)position, (int)DGlobal.CharacterPieces.BODY] != null)
            {
                this.mGraphics.drawImage(this.mCharacterset[0, (int)distance, (int)position, (int)DGlobal.CharacterPieces.BODY]);
            }

            if (this.mLeftArmEquipped == false)
            {
                if (this.mCharacterset[0, (int)distance, (int)position, (int)DGlobal.CharacterPieces.ARM_LEFT] != null)
                {
                    this.mGraphics.drawImage(this.mCharacterset[0, (int)distance, (int)position, (int)DGlobal.CharacterPieces.ARM_LEFT]);
                }
            }
            else
            {
                if (this.mCharacterset[0, (int)distance, (int)position, (int)DGlobal.CharacterPieces.ARM_LEFT_EQUIPPED] != null)
                {
                    this.mGraphics.drawImage(this.mCharacterset[0, (int)distance, (int)position, (int)DGlobal.CharacterPieces.ARM_LEFT_EQUIPPED]);
                }
            }

            if (this.mRightArmEquipped == false)
            {
                if (this.mCharacterset[0, (int)distance, (int)position, (int)DGlobal.CharacterPieces.ARM_RIGHT] != null)
                {
                    this.mGraphics.drawImage(this.mCharacterset[0, (int)distance, (int)position, (int)DGlobal.CharacterPieces.ARM_RIGHT]);
                }
            }
            else
            {
                if (this.mCharacterset[0, (int)distance, (int)position, (int)DGlobal.CharacterPieces.ARM_RIGHT_EQUIPPED] != null)
                {
                    this.mGraphics.drawImage(this.mCharacterset[0, (int)distance, (int)position, (int)DGlobal.CharacterPieces.ARM_RIGHT_EQUIPPED]);
                }
            }
        }

        public void setPosition(int positionX, int positionY)
        {
            this.mPositionX = positionX;
            this.mPositionY = positionY;
        }

        public int getPositionX()
        {
            return this.mPositionX;
        }

        public int getPositionY()
        {
            return this.mPositionY;
        }

        public void setLeftArmEquipped(bool equipped)
        {
            this.mLeftArmEquipped = equipped;
        }

        public bool getLeftArmEquipped()
        {
            return this.mLeftArmEquipped;
        }

        public void setRightArmEquipped(bool equipped)
        {
            this.mRightArmEquipped = equipped;
        }

        public bool getRightArmEquipped()
        {
            return this.mRightArmEquipped;
        }
    }
}
