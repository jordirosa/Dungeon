namespace Dungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDungeon app = new AppDungeon();

            app.run(1280, 720, 1.0f);
        }
    }
}