using SFML.Graphics;

namespace pong_sfml 
{
    public static class Fonts
    {
        private static Font arial = new Font("Assets/Fonts/arial.ttf");
        public static Font Arial => arial;

        private static Font hyperspace = new Font("Assets/Fonts/Hyperspace.ttf");
        public static Font Hyperspace => hyperspace;
    }
}