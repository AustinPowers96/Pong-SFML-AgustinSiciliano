using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using pong_sfml.classes.ui;
using SFML.System;

namespace pong_sfml.States
{
    class ControlsState : LoopState
    {

        private Font textFont;
        private Text controlsTextPlayer1;
        private Text controlsTextPlayer2;
        private Button controlsQuitButton;
        public event Action FromControlsToMainMenuPressed;

        public ControlsState(RenderWindow window) : base(window)
        {

        }


        private void OnPressQuit() => FromControlsToMainMenuPressed?.Invoke();

        protected override void Start()
        {
            
            string fontFilePath = "Assets/Fonts/Hyperspace.ttf";
            string buttonImageFilePath = "Assets/Images/Button.png";
            float firstButtonOffset = 100f;
            float buttonSpacing = 120f;

            textFont = new Font(fontFilePath);
            controlsTextPlayer1 = new Text("Player 1 use W / S", textFont);
            controlsTextPlayer2 = new Text("Player 2 use arrows  UP / DOWN ", textFont);


            controlsTextPlayer1.CharacterSize = 50;
            controlsTextPlayer1.FillColor = Color.Red;
            controlsTextPlayer1.OutlineColor = Color.Red;
            controlsTextPlayer1.OutlineThickness = 2f;

            controlsTextPlayer2.CharacterSize = 50;
            controlsTextPlayer2.FillColor = Color.Green;
            controlsTextPlayer2.OutlineColor = Color.Green;
            controlsTextPlayer2.OutlineThickness = 2f;

            FloatRect controlsRect = controlsTextPlayer1.GetLocalBounds();
            FloatRect controlsRect1 = controlsTextPlayer2.GetLocalBounds();

           controlsTextPlayer1.Origin = new Vector2f(controlsRect.Width / 2f, controlsRect.Height / 2f);
           controlsTextPlayer1.Position = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 2f - buttonSpacing);

           controlsTextPlayer2.Origin = new Vector2f(controlsRect.Width / 2f, controlsRect.Height / 2f);
           controlsTextPlayer2.Position = new Vector2f(Global.ScreenSize.X / 2.7f, Global.ScreenSize.Y / 3f + buttonSpacing);

           Vector2f controlsQuitButtonPosition = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 2f + firstButtonOffset + buttonSpacing);

           controlsQuitButton = new Button(this.Window, buttonImageFilePath, fontFilePath, controlsQuitButtonPosition, "Quit");

           controlsQuitButton.SetColor(Color.Red);

           controlsQuitButton.FormatText(fillColor: Color.Red, outlineColor: Color.White, size: 30, outline: true, outlineThickness: 2f);

           View view = new View(this.Window.GetView());

           view.Center = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 2f);

           this.Window.SetView(view);

           controlsQuitButton.OnPressed += OnPressQuit;
           this.Window.Closed += OnCloseWindow;



        }

        protected override void Update(float deltaTime)
        {

        }


        protected override void Draw()
        {

            this.Window.Draw(controlsTextPlayer1);
            this.Window.Draw(controlsTextPlayer2);
            this.controlsQuitButton.Draw();
        }

        protected override void Finish()
        {
            controlsQuitButton.OnPressed -= OnPressQuit;
            this.Window.Closed -= OnCloseWindow;
        }











    }
}
