using System;

using SDL2;

namespace SDLEngine
{
    public class SDLGraphics
    {
        #region Attributes
        private IntPtr mWindow;
        private IntPtr mRenderer;

        private int mWidth;
        private int mHeight;

        private float mScale;
        #endregion

        #region Constructor
        public SDLGraphics()
        {
            this.mWindow = IntPtr.Zero;
            this.mRenderer = IntPtr.Zero;

            this.mWidth = 0;
            this.mHeight = 0;

            this.mScale = 1.0f;
        }
        #endregion

        public bool init(int width, int height)
        {
            this.mWidth = width;
            this.mHeight = height;

            SDL.SDL_Init(SDL.SDL_INIT_VIDEO);
            this.mWindow = SDL.SDL_CreateWindow("#SDLEngine", SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, (int)(width * this.mScale), (int)(height * this.mScale), SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS);
            this.mRenderer = SDL.SDL_CreateRenderer(this.mWindow, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            SDL.SDL_SetRenderDrawColor(this.mRenderer, 0, 0, 0, 255);

            return true;
        }

        public bool init(int width, int height, float scale)
        {
            this.mScale = scale;

            return this.init(width, height);
        }

        public void clear()
        {
            SDL.SDL_RenderClear(this.mRenderer);
        }

        public void render()
        {
            SDL.SDL_RenderPresent(this.mRenderer);
        }

        public SDLImage loadImage(string path)
        {
            SDLImage image = null;
            IntPtr texture = IntPtr.Zero;

            texture = SDL_image.IMG_LoadTexture(this.mRenderer, path);

            if(texture != IntPtr.Zero)
            {
                image = new SDLImage(texture);
            }

            return image;
        }

        public void drawImage(SDLImage image)
        {
            SDL.SDL_Rect dstRect;

            float x;
            float y;

            x = image.x;
            switch(image.horizontalAlignment)
            {
                case SDLCommon.SDLHorizontalAlignment.CENTER:
                    x -= image.width / 2;
                    break;
                case SDLCommon.SDLHorizontalAlignment.RIGHT:
                    x -= image.width;
                    break;
            }
            dstRect.x = (int)(x * image.scale);

            y = image.y;
            switch (image.verticalAlignment)
            {
                case SDLCommon.SDLVerticalAlignment.CENTER:
                    y -= image.height / 2;
                    break;
                case SDLCommon.SDLVerticalAlignment.BOTTOM:
                    y -= image.height;
                    break;
            }
            dstRect.y = (int)(y * image.scale);

            dstRect.w = (int)(image.width * this.mScale);
            dstRect.h = (int)(image.height * this.mScale);

            SDL.SDL_RenderCopy(this.mRenderer, image.texture, IntPtr.Zero, ref dstRect);
        }
    }
}
