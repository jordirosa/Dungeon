using System;

using SDL2;

namespace SDLEngine
{
    public class SDLImage
    {
        #region Attributes
        private IntPtr mTexture;

        private int mX;
        private int mY;
        private int mWidth;
        private int mHeight;

        private float mScale;

        SDLCommon.SDLHorizontalAlignment mHorizontalAlignment;
        SDLCommon.SDLVerticalAlignment mVerticalAlignment;
        #endregion

        #region Properties
        public IntPtr texture
        {
            get
            {
                return this.mTexture;
            }
        }

        public int x { get => mX; set => mX = value; }
        public int y { get => mY; set => mY = value; }
        public int width { get => (int)(mWidth * mScale); set => mWidth = value; }
        public int height { get => (int)(mHeight * mScale); set => mHeight = value; }

        public float scale { get => mScale; set => mScale = value; }

        public SDLCommon.SDLHorizontalAlignment horizontalAlignment { get => mHorizontalAlignment; set => mHorizontalAlignment = value; }
        public SDLCommon.SDLVerticalAlignment verticalAlignment { get => mVerticalAlignment; set => mVerticalAlignment = value; }
        #endregion

        #region Constructor
        public SDLImage(IntPtr texture)
        {
            this.mTexture = texture;

            this.mX = 0;
            this.mY = 0;
            SDL.SDL_QueryTexture(this.mTexture, out _, out _, out this.mWidth, out this.mHeight);
            this.mScale = 1.0f;

            this.mHorizontalAlignment = SDLCommon.SDLHorizontalAlignment.LEFT;
            this.mVerticalAlignment = SDLCommon.SDLVerticalAlignment.TOP;
        }
        #endregion
    }
}
