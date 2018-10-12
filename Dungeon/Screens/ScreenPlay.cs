using SDL2;

using SDLEngine;

using Dungeon.Core;

namespace Dungeon.Screens
{
    class ScreenPlay : SDLScreen
    {
        private DMap map;

        private int x;
        private int y;

        private DGlobal.MapOrientations orientation;

        #region Constructor
        public ScreenPlay(SDLApp app) : base(app)
        {
        }
        #endregion

        public override void initialize()
        {
            x = 1;
            y = 7;

            orientation = DGlobal.MapOrientations.NORTH;

            map = new DMap(this.mApp.graphics);
            map.loadMap("TestMap");

            DCharacter testCharacter = new DCharacter(this.mApp.graphics);
            testCharacter.loadCharacter("Goblin");
            testCharacter.setPosition(1, 2);
            map.addCharacter(testCharacter);

            testCharacter = new DCharacter(this.mApp.graphics);
            testCharacter.loadCharacter("Goblin");
            testCharacter.setPosition(2, 4);
            map.addCharacter(testCharacter);

            testCharacter = new DCharacter(this.mApp.graphics);
            testCharacter.loadCharacter("Goblin");
            testCharacter.setPosition(3, 6);
            testCharacter.setRightArmEquipped(true);
            map.addCharacter(testCharacter);
        }

        public override void update(SDL.SDL_Event eventSDL)
        {
            int posX = this.x;
            int posY = this.y;
            DGlobal.MapOrientations orientation = this.orientation;

            if (eventSDL.type == SDL.SDL_EventType.SDL_KEYDOWN)
            {
                switch(eventSDL.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_UP:
                        if (this.map.checkWalkable(posX, posY, orientation))
                        {
                            DGlobal.moveForward(orientation, ref posX, ref posY, 1);
                            DGlobal.turnBack(ref orientation);
                            if (this.map.checkWalkable(posX, posY, orientation))
                            {
                                this.x = posX;
                                this.y = posY;
                            }
                        }
                        break;
                    case SDL.SDL_Keycode.SDLK_DOWN:
                        DGlobal.turnBack(ref orientation);
                        if (this.map.checkWalkable(posX, posY, orientation))
                        {
                            DGlobal.turnBack(ref orientation);
                            DGlobal.moveBack(orientation, ref posX, ref posY, 1);
                            if (this.map.checkWalkable(posX, posY, orientation))
                            {
                                this.x = posX;
                                this.y = posY;
                            }
                        }
                        break;
                    case SDL.SDL_Keycode.SDLK_LEFT:
                        DGlobal.turnLeft(ref this.orientation);
                        break;
                    case SDL.SDL_Keycode.SDLK_RIGHT:
                        DGlobal.turnRight(ref this.orientation);
                        break;
                    case SDL.SDL_Keycode.SDLK_ESCAPE:
                        this.mApp.quit();
                        break;
                }
            }

            int mouseX;
            int mouseY;
            SDL.SDL_GetMouseState(out mouseX, out mouseY);
            System.Console.WriteLine("Mouse X: " + mouseX + " Y: " + mouseY);
        }

        public override void draw()
        {
            this.map.draw(x, y, orientation);
        }
    }
}
