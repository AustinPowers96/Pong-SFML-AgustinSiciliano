using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace pong_sfml 
{
    public static class Global 
    {            
        private static Vector2f screenSize;
        public static Vector2f ScreenSize 

        {
            get { return screenSize; }
            set { screenSize = value; }
        }

        public static int MaxGoals = 10;

        private static keyboard kb = new keyboard();
        public static keyboard Keyboard 
        {
            get { return kb; }
        }

        private static mouse mouse = new mouse();
        public static mouse Mouse  
        {
            get { return mouse; }
        }
        
        public static Dictionary<string, sound> sfx = new Dictionary<string, sound>();

        public static RenderWindow createWindow(string Title) 
        {
            RenderWindow window = new RenderWindow(new VideoMode((uint)ScreenSize.X, (uint)ScreenSize.Y), Title);
            View view = new View(ScreenSize / 2f, ScreenSize);
            window.SetView(view);
            window.SetKeyRepeatEnabled(false);
            window.SetFramerateLimit(60);
            window.Closed += window_CloseWindow;

            return window;
        }
        
        private static void window_CloseWindow(object sender, EventArgs e) 
        {
            if (sender == null) 
            { 
                return; 
            }
            if (sender.GetType() != typeof(RenderWindow)) 
            { 
                return; 
            }
            ((RenderWindow)sender).Close();
        }
    }
}