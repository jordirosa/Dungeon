using Dungeon.Screens;
using SDLEngine;

namespace Dungeon
{
    class AppDungeon : SDLApp
    {
        public override SDLScreen getInitialScreen()
        {
            return new ScreenPlay(this);
        }
    }
}
