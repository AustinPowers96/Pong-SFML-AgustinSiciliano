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
    class GameOverState : LoopState
    {
        int winnerPlayer;
        private Font textFont;
        private Text gameOverText;
        private Text playerWinText;
        private Button restartButton;
        private Button quitButton;
        public event Action OnRestartPressed;
        public event Action OnQuitToMainMenuPressed;
        

        public GameOverState (RenderWindow window) : base(window)
        {
           
        }

        private void OnPressRestart() => OnRestartPressed?.Invoke(); 
        private void OnPressQuit() => OnQuitToMainMenuPressed?.Invoke();
       

        protected override void Start()
        {
            Console.WriteLine($"GANO EL JUGADOR {this.winnerPlayer}");
            string fontFilePath = "Assets/Fonts/Hyperspace.ttf";
            string buttonImageFilePath = "Assets/Images/Button.png";
            float firstButtonOffset = 100f;
            float buttonSpacing = 120f;


            textFont = new Font(fontFilePath);
            gameOverText = new Text("GAME OVER", textFont);
            playerWinText = new Text($"PLAYER {this.winnerPlayer} WINS", textFont);

            gameOverText.CharacterSize = 70;
            gameOverText.FillColor = Color.White;
            gameOverText.OutlineColor = Color.Red;
            gameOverText.OutlineThickness = 2f;

            playerWinText.CharacterSize = 50;
            playerWinText.FillColor = Color.Blue;
            playerWinText.OutlineColor = Color.Blue;
            playerWinText.OutlineThickness = 2f;

            FloatRect gameOverRect = gameOverText.GetLocalBounds();
            FloatRect playerWinRect = playerWinText.GetLocalBounds();

            gameOverText.Origin = new Vector2f(gameOverRect.Width / 2f, gameOverRect.Height / 2f);
            gameOverText.Position = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 2f - buttonSpacing);

            playerWinText.Origin = new Vector2f(playerWinRect.Width / 2f, playerWinRect.Height / 2f);
            playerWinText.Position = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 2f - buttonSpacing + firstButtonOffset);

            Vector2f RestartButtonPosition = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 2f + firstButtonOffset);
            Vector2f quitButtonPosition = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 2f + firstButtonOffset + buttonSpacing);

            restartButton = new Button(this.Window, buttonImageFilePath, fontFilePath, RestartButtonPosition, "Restart");
            quitButton = new Button(this.Window, buttonImageFilePath, fontFilePath, quitButtonPosition, "Quit");

            restartButton.SetColor(Color.Green);
            quitButton.SetColor(Color.Red);

            restartButton.FormatText(fillColor: Color.Green, outlineColor: Color.White, size: 30, outline: true, outlineThickness: 2f);
            quitButton.FormatText(fillColor: Color.Red, outlineColor: Color.White, size: 30, outline: true, outlineThickness: 2f);

            View view = new View(this.Window.GetView());

            view.Center = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 2f);

            this.Window.SetView(view);



            restartButton.OnPressed += OnPressRestart;
            quitButton.OnPressed += OnPressQuit;
            this.Window.Closed += OnCloseWindow;

        }

        protected override void Update(float deltaTime)
        {
            
        }


        protected override void Draw()
        {
            this.Window.Draw(gameOverText);
            this.Window.Draw(playerWinText);
            this.restartButton.Draw();
            this.quitButton.Draw();
        }

        protected override void Finish()
        {
            restartButton.OnPressed -= OnPressRestart;
            quitButton.OnPressed -= OnPressQuit;
            Global.sfx["songMatch"].stop();
            this.Window.Closed -= OnCloseWindow;
        }

        public void SetWinnerPlayer(int winnerPlayer)
        {
            this.winnerPlayer = winnerPlayer;
        }






    }
}
