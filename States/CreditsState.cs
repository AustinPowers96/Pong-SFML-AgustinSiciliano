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
    class CreditsState : LoopState
    {
        private Font textFont;
        private Text creditsText;
        private Text creditsText1;
        private Text creditsText2;
        private Text creditsText3;
        private Button creditsQuitButton;
        public event Action FromCreditsToMainMenuPressed;


        public CreditsState(RenderWindow window) : base(window)
        {

        }

        private void OnPressQuit() => FromCreditsToMainMenuPressed?.Invoke();


        protected override void Start()
        {

            string fontFilePath = "Assets/Fonts/Hyperspace.ttf";
            string buttonImageFilePath = "Assets/Images/Button.png";
            float firstButtonOffset = 100f;
            float buttonSpacing = 120f;
            Global.sfx["CreditsSong"].play();

            textFont = new Font(fontFilePath);
            creditsText = new Text("A GAME MADE BY AGUSTIN SICILIANO", textFont);
            creditsText1 = new Text("Songs by : 2 UNLIMITED & Quad City DJ's", textFont);
            creditsText2 = new Text("Font by : https://www.dafont.com/hyperspace.font", textFont);
            creditsText3 = new Text("Sounds by: https://freesound.org", textFont);

            creditsText.CharacterSize = 60;
            creditsText.FillColor = Color.Red;
            creditsText.OutlineColor = Color.Red;
            creditsText.OutlineThickness = 2f;

            creditsText1.CharacterSize = 40;
            creditsText1.FillColor = Color.White;
            creditsText1.OutlineColor = Color.Red;
            creditsText1.OutlineThickness = 2f;

            creditsText2.CharacterSize = 40;
            creditsText2.FillColor = Color.White;
            creditsText2.OutlineColor = Color.Red;
            creditsText2.OutlineThickness = 2f;

            creditsText3.CharacterSize = 40;
            creditsText3.FillColor = Color.White;
            creditsText3.OutlineColor = Color.Red;
            creditsText3.OutlineThickness = 2f;

            FloatRect creditsRect = creditsText.GetLocalBounds();
            FloatRect creditsRect1 = creditsText1.GetLocalBounds();
            FloatRect creditsRect2 = creditsText2.GetLocalBounds();
            FloatRect creditsRect3 = creditsText3.GetLocalBounds();

            creditsText.Origin = new Vector2f(creditsRect.Width / 2f, creditsRect.Height / 2f);
            creditsText.Position = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 3.6f - buttonSpacing);

            creditsText1.Origin = new Vector2f(creditsRect1.Width / 2f, creditsRect1.Height / 2f);
            creditsText1.Position = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 2f - buttonSpacing);

            creditsText2.Origin = new Vector2f(creditsRect2.Width / 2f, creditsRect2.Height / 2f);
            creditsText2.Position = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 1.6f - buttonSpacing);

            creditsText3.Origin = new Vector2f(creditsRect3.Width / 2f, creditsRect3.Height / 2f);
            creditsText3.Position = new Vector2f(Global.ScreenSize.X / 2.4f, Global.ScreenSize.Y / 1.3f - buttonSpacing);

            Vector2f creditsQuitButtonPosition = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 1.7f + firstButtonOffset + buttonSpacing);

            creditsQuitButton = new Button(this.Window, buttonImageFilePath, fontFilePath, creditsQuitButtonPosition, "Quit");

            creditsQuitButton.SetColor(Color.Red);

            creditsQuitButton.FormatText(fillColor: Color.Red, outlineColor: Color.White, size: 30, outline: true, outlineThickness: 2f);

            View view = new View(this.Window.GetView());

            view.Center = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 2f);

            this.Window.SetView(view);

            creditsQuitButton.OnPressed += OnPressQuit;
            this.Window.Closed += OnCloseWindow;


        }

        protected override void Update(float deltaTime)
        {

        }


        protected override void Draw()
        {
            this.Window.Draw(creditsText);
            this.Window.Draw(creditsText1);
            this.Window.Draw(creditsText2);
            this.Window.Draw(creditsText3);
            this.creditsQuitButton.Draw();
        }

        protected override void Finish()
        {
            creditsQuitButton.OnPressed -= OnPressQuit;
            this.Window.Closed -= OnCloseWindow;
            Global.sfx["CreditsSong"].stop();
        }



    }
}
