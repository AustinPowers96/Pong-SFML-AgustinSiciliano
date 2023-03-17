using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using pong_sfml.States;

namespace pong_sfml
{
    class Program
    {
        static void Main(string[] args)
        {
            Global.ScreenSize = new Vector2f(1280, 720);
            StatesController controller = new StatesController(Global.createWindow("Pong"));

        }
    }
}
