using SDL2;

namespace SDLEngine
{
    public abstract class SDLApp
    {
        #region Attributes
        private bool mExit;

        private SDLGraphics mGraphics;

        private SDLScreen mScreen;
        #endregion

        #region Constructor
        public SDLApp()
        {
            this.mExit = false;

            this.mScreen = null;
        }
        #endregion

        #region Properties
        public SDLGraphics graphics
        {
            get
            {
                return this.mGraphics;
            }
        }
        #endregion

        public abstract SDLScreen getInitialScreen();

        public void setScreen(SDLScreen screen)
        {
            this.mScreen = screen;
            this.mScreen.initialize();
        }

        public void run(int resolutionX, int resolutionY, float scale)
        {
            SDL.SDL_Event eventSDL;

            this.mGraphics = new SDLGraphics();
            this.mGraphics.init(resolutionX, resolutionY, scale);

            this.setScreen(this.getInitialScreen());

            while (!this.mExit)
            {
                while (SDL.SDL_PollEvent(out eventSDL) != 0)
                {
                    if (eventSDL.type == SDL.SDL_EventType.SDL_QUIT)
                    {
                        this.mExit = true;
                    }

                    this.mScreen.update(eventSDL);
                }

                this.mGraphics.clear();
                this.mScreen.draw();
                this.mGraphics.render();
            }
        }

        public void quit()
        {
            this.mExit = true;
        }
    }
}
