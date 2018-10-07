using SDL2;

namespace SDLEngine
{
    public abstract class SDLScreen
    {
        #region Attributes
        protected SDLApp mApp;
        #endregion

        #region Constructor
        public SDLScreen(SDLApp app)
        {
            this.mApp = app;
        }
        #endregion

        public abstract void initialize();
        public abstract void update(SDL.SDL_Event eventSDL);
        public abstract void draw();
    }
}