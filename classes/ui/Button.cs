using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace pong_sfml.classes.ui
{
    class Button
    {
        private RenderWindow window;

        private Texture texture;
        private Font font;

        private Sprite background;
        private Text text;

        public event Action OnPressed;


        public Button(RenderWindow window, string textureFilePath, string fontFilePath, Vector2f position, string buttonText)
        {
            this.window = window;

            texture = new Texture(textureFilePath);
            font = new Font(fontFilePath);

            background = new Sprite(texture);
            text = new Text(buttonText, font);

            FloatRect backgroundRect = background.GetLocalBounds();

            background.Origin = new Vector2f(backgroundRect.Width / 2f, backgroundRect.Height / 2f);

            SetText(buttonText);
            SetPosition(position);

            window.MouseButtonReleased += OnReleaseMouseButton;
        }

        ~Button()
        {
            window.MouseButtonReleased -= OnReleaseMouseButton;
        }

        private void OnReleaseMouseButton(object sender, MouseButtonEventArgs eventArgs)
        {
            FloatRect bounds = background.GetGlobalBounds();

            if (bounds.Contains(eventArgs.X, eventArgs.Y))
                OnPressed?.Invoke();
        }

        public void SetText(string newText)
        {
            text.DisplayedString = newText;

            FloatRect textRect = text.GetLocalBounds();

            text.Origin = new Vector2f(textRect.Width / 2f, textRect.Height / 2f);
        }

        public void SetColor(Color color)
        {
            background.Color = color;
        }

        public void FormatText(Color fillColor, Color outlineColor, uint size, bool outline, float outlineThickness)
        {
            text.FillColor = fillColor;
            text.CharacterSize = size;

            if (outline)
            {
                text.OutlineColor = outlineColor;
                text.OutlineThickness = outlineThickness;
            }
            else
            {
                text.OutlineColor = Color.Transparent;
                text.OutlineThickness = 0f;
            }
        }

        public void SetPosition(Vector2f position)
        {
            text.Position = position;
            background.Position = position;
        }

        public void Draw()
        {
            window.Draw(background);
            window.Draw(text);
        }
    }



}

