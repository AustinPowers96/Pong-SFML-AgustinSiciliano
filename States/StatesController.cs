using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;


namespace pong_sfml.States
{
    class StatesController
    {
        private RenderWindow window;
        private MainMenuState mainMenuState;
        private GameState gameState;
        private ControlsState controlsState;
        private CreditsState creditsState;
        private GameOverState gameOverState;
       

        public StatesController(RenderWindow window)
        {
            Global.sfx.Add("wall", new sound("Assets/Sounds/wall.wav"));
            Global.sfx.Add("paddle", new sound("Assets/Sounds/paddle.wav"));
            Global.sfx.Add("score", new sound("Assets/Sounds/score.wav"));
            Global.sfx.Add("songMainMenu", new sound("Assets/Sounds/Sport2.wav"));
            Global.sfx.Add("songMatch", new sound("Assets/Sounds/Sport1.wav"));
            Global.sfx.Add("CreditsSong", new sound("Assets/Sounds/CreditsSong.wav"));
            this.window = window;
            this.mainMenuState = new MainMenuState(this.window);
            this.gameOverState = new GameOverState(this.window);
            this.controlsState = new ControlsState(this.window);
            this.creditsState = new CreditsState(this.window);
            this.mainMenuState.OnPlayPressed += StartGame;
            this.mainMenuState.OnControlsPressed += OpenControls;
            this.controlsState.FromControlsToMainMenuPressed += QuitControls;
            this.mainMenuState.OnCreditsPressed += OpenCredits;
            this.creditsState.FromCreditsToMainMenuPressed += QuitCredits;
            this.mainMenuState.OnQuitPressed += QuitApplication;
            this.gameOverState.OnRestartPressed += RestartFromGameOver;
            this.gameOverState.OnQuitToMainMenuPressed += QuitToMainMenu;
            this.mainMenuState.Play();
        }

        private void StartGame()
        {
            mainMenuState.Stop();
            this.gameState = new GameState(this.window);
            this.gameState.OnPlayerOneWin += PlayerOneWin;
            this.gameState.OnPlayerTwoWin += PlayerTwoWin;
            this.gameState.OnQuitPressed += QuitGame;
            gameState.run(); 
        }

        private void OpenControls()
        {
            this.mainMenuState.Stop();
            this.controlsState.Play();
        }

        private void QuitControls()
        {
            this.controlsState.Stop();
            this.mainMenuState.Play();
        }

        private void OpenCredits()
        {
            this.mainMenuState.Stop();
            this.creditsState.Play();
        }

        private void QuitCredits()
        {
            this.creditsState.Stop();
            this.mainMenuState.Play();
        }


        private void QuitGame()
        {
            this.gameState.OnQuitPressed -= QuitGame;
            gameState.Stop();
            //this.gameState = null;
            this.gameOverState.OnQuitToMainMenuPressed -= QuitGame;
            mainMenuState.Play();
        }

        private void QuitApplication()
        {
            mainMenuState.Stop();
            window.Close();
        }

        private void PlayerOneWin()
        {
            this.gameState.Stop();
            this.gameOverState.SetWinnerPlayer(1);
            this.gameOverState.Play();
        }

        private void PlayerTwoWin()
        {
            this.gameState.Stop();
            this.gameOverState.SetWinnerPlayer(2);
            this.gameOverState.Play();
        }

        private void QuitToMainMenu()
        {
            gameOverState.Stop();
            this.gameState.OnQuitPressed -= QuitGame;
            this.gameState = null;
            this.gameOverState.OnQuitToMainMenuPressed -= QuitGame;
            mainMenuState.Play();
        }

        private void RestartFromGameOver()
        {
            gameOverState.Stop();
            this.gameState = null;
            this.gameState = new GameState(this.window);
            this.gameState.OnPlayerOneWin += PlayerOneWin;
            this.gameState.OnPlayerTwoWin += PlayerTwoWin;
            this.gameState.OnQuitPressed += QuitGame;
            gameState.run();
        }
    }
}
