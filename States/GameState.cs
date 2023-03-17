using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace pong_sfml.States
{
    class GameState : LoopState
    {
        private const float timeStep = 1 / 60.0f;
        private float timeScale = 1.0f;
        private DateTime lastTime;

        rectangle leftPaddle;
        rectangle rightPaddle;

        rectangle ball;
        DateTime ballSpawn;

        rectangle topEdge;
        rectangle bottomEdge;

        float maxPaddleSpeed = 300f;
        float paddleAcceleration = 120f;

        int leftScore;
        int rightScore;

        rectangle lastPoint;

        Vector2f playingField;

        public event Action OnQuitPressed;

        public event Action OnPlayerOneWin;
        public event Action OnPlayerTwoWin;

        private void OnPressKey (object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Code == Keyboard.Key.Escape)
            {
                OnQuitPressed?.Invoke();
            }
        }

        private void TriggerGameOver(int winnerPlayer)
        {
            if (winnerPlayer == 1)
            {
                OnPlayerOneWin.Invoke();
            }
            else if (winnerPlayer == 2)
            {
                OnPlayerTwoWin.Invoke();
            }
        }

        public GameState(RenderWindow window) : base(window)
        {
            Start();
        }

        protected override void Start()
        {
            Global.sfx["songMatch"].play(true);
            Global.sfx["songMatch"].SetVolume(20f);


            playingField = new Vector2f(Global.ScreenSize.Y / 2f - 300, Global.ScreenSize.Y / 2f + 300);

            leftPaddle = new rectangle(new Vector2f(10, 80));
            leftPaddle.SetPosition(new Vector2f(20, Global.ScreenSize.Y / 2f));
            leftPaddle.OutlineColour = new Color(200, 0, 0);
            leftPaddle.OutlineThickness = 2f;
            leftPaddle.FillColour = new Color(255, 50, 50);
            leftPaddle.Drag = 10f;
            leftScore = 0;

            rightPaddle = new rectangle(new Vector2f(10, 80));
            rightPaddle.SetPosition(new Vector2f(Global.ScreenSize.X - 20, Global.ScreenSize.Y / 2f));
            rightPaddle.OutlineColour = new Color(0, 200, 0);
            rightPaddle.OutlineThickness = 2f;
            rightPaddle.FillColour = new Color(50, 255, 50);
            rightPaddle.Drag = 10f;
            rightScore = 0;

            topEdge = new rectangle(new Vector2f(Global.ScreenSize.X, 5));
            topEdge.SetPosition(new Vector2f(Global.ScreenSize.X / 2f, playingField.X + 20));
            topEdge.FillColour = Color.White;

            bottomEdge = new rectangle(new Vector2f(Global.ScreenSize.X, 5));
            bottomEdge.SetPosition(new Vector2f(Global.ScreenSize.X / 2f, playingField.Y - 20));
            bottomEdge.FillColour = Color.White;

            ballSpawn = DateTime.Now.AddSeconds(1);

            if (util.randint(0, 1) > 0)
            {
                lastPoint = rightPaddle;
            }
            else
            {
                lastPoint = leftPaddle;
            }

            this.Window.Closed += OnCloseWindow;
            this.Window.KeyPressed += OnPressKey;
        }

        protected override void Update(float delta)
        {
            Global.Keyboard.update();

            if (ball == null)
            {
                if (DateTime.Now > ballSpawn)
                {
                    ball = new rectangle(new Vector2f(17, 17));
                    ball.SetPosition(Global.ScreenSize / 2f);
                    ball.SetVelocity(new Vector2f(100, util.randfloat(-500, 500)));
                    ball.FillColour = Color.White;

                    if (lastPoint == rightPaddle)
                    {
                        ball.SetXVelocity(ball.Velocity.X * -1);
                    }
                }
            }
            else
            {
                ball.update(delta);

                // Chequeo de colision contra las paredes
                if (intersection.rectangleInsideRectangle(ball.ToFloatRect(), topEdge.ToFloatRect()) ||
                    intersection.rectangleInsideRectangle(ball.ToFloatRect(), bottomEdge.ToFloatRect()))
                {
                    Global.sfx["wall"].play();
                    ball.SetYVelocity(ball.Velocity.Y * -1f);
                }

                // Chequeo de colision entre paletas
                if (intersection.rectangleInsideRectangle(ball.ToFloatRect(), leftPaddle.ToFloatRect()))
                {
                    Global.sfx["paddle"].play();
                    Vector2f dir = util.normalise(ball.Position - leftPaddle.Position);
                    dir *= util.magnitude(ball.Velocity * -1.2f);
                    ball.SetVelocity(dir);
                }

                // Chequeo de colision entre paletas
                if (intersection.rectangleInsideRectangle(ball.ToFloatRect(), rightPaddle.ToFloatRect()))
                {
                    Global.sfx["paddle"].play();
                    Vector2f dir = util.normalise(ball.Position - rightPaddle.Position);
                    dir *= util.magnitude(ball.Velocity * -1.2f);
                    ball.SetVelocity(dir);
                }

                if (ball.Position.X < 0)
                {
                    rightScore += 1;
                    ballSpawn = DateTime.Now.AddSeconds(1);
                    ball = null;
                    Global.sfx["score"].play();
                }
                else if (ball.Position.X > Global.ScreenSize.X)
                {
                    leftScore += 1;
                    ballSpawn = DateTime.Now.AddSeconds(1);
                    ball = null;
                    Global.sfx["score"].play();
                }
            }

            // La paleta izquierda se controla con W y S
            // La paleta derecha se controla con las flechas (arriba y abajo)

            // Paleta Izquierda
            if (Global.Keyboard["w"].isPressed && leftPaddle.Velocity.Y > -maxPaddleSpeed)
            {
                leftPaddle.AddYVelocity(-paddleAcceleration);
            }

            if (Global.Keyboard["s"].isPressed && leftPaddle.Velocity.Y < maxPaddleSpeed)
            {
                leftPaddle.AddYVelocity(paddleAcceleration);
            }

            if (leftPaddle.Position.Y < playingField.X + leftPaddle.Size.Y)
            {
                leftPaddle.SetYVelocity(0);
                leftPaddle.SetYPosition(playingField.X + leftPaddle.Size.Y);
            }

            if (leftPaddle.Position.Y > playingField.Y - leftPaddle.Size.Y)
            {
                leftPaddle.SetYVelocity(0);
                leftPaddle.SetYPosition(playingField.Y - leftPaddle.Size.Y);
            }

            leftPaddle.update(delta);

            // Paleta Derecha
            if (Global.Keyboard["up"].isPressed && rightPaddle.Velocity.Y > -maxPaddleSpeed)
            {
                rightPaddle.AddYVelocity(-paddleAcceleration);
            }

            if (Global.Keyboard["down"].isPressed && rightPaddle.Velocity.Y < maxPaddleSpeed)
            {
                rightPaddle.AddYVelocity(paddleAcceleration);
            }

            if (rightPaddle.Position.Y < playingField.X + rightPaddle.Size.Y)
            {
                rightPaddle.SetYVelocity(0);
                rightPaddle.SetYPosition(playingField.X + rightPaddle.Size.Y);
            }

            if (rightPaddle.Position.Y > playingField.Y - rightPaddle.Size.Y)
            {
                rightPaddle.SetYVelocity(0);
                rightPaddle.SetYPosition(playingField.Y - rightPaddle.Size.Y);
            }

            rightPaddle.update(delta);

            if (this.leftScore >= Global.MaxGoals || this.rightScore >= Global.MaxGoals)
            {
                this.TriggerGameOver((this.leftScore > this.rightScore) ? 1 : 2);
            }
        }

        protected override void Draw()
        {
            Window.Clear();

            topEdge.draw(Window);
            bottomEdge.draw(Window);

            // Puntajes
            Text leftScoreText = new Text(leftScore.ToString(), Fonts.Hyperspace);
            leftScoreText.CharacterSize = 48;
            leftScoreText.Position = new Vector2f(10, 0);
            leftScoreText.FillColor = Color.White;
            Window.Draw(leftScoreText);

            Text rightScoreText = new Text(rightScore.ToString(), Fonts.Hyperspace);
            rightScoreText.CharacterSize = 48;
            rightScoreText.Position = new Vector2f(Global.ScreenSize.X - 10 - 24, 0);
            rightScoreText.FillColor = Color.White;
            Window.Draw(rightScoreText);

            leftPaddle.draw(Window);
            rightPaddle.draw(Window);

            // Linea del medio
            int divisions = 20;
            float spacing = (playingField.Y - playingField.X) / divisions;

            for (int i = 1; i < divisions - 1; i++)
            {
                RectangleShape rs = new RectangleShape(new Vector2f(2, spacing / 2f));
                rs.Position = new Vector2f(Global.ScreenSize.X / 2f, playingField.X + spacing * i);
                rs.Origin = new Vector2f(-1, spacing / -2f);
                rs.FillColor = Color.White;
                Window.Draw(rs);
            }

            if (ball != null)
            {
                ball.draw(Window);
            }

            Window.Display();
        }

        public void run()
        {
            while (Window.IsOpen)
            {
                if (!Window.HasFocus())
                {
                    continue;
                }

                if ((float)(DateTime.Now - lastTime).TotalMilliseconds / 1000f > timeStep)
                {
                    float delta = timeStep * timeScale;
                    lastTime = DateTime.Now;

                    Window.DispatchEvents();
                    Update(delta);
                }

                Draw();
            }
        }


        protected override void Finish()
        {
            this.Window.Closed -= OnCloseWindow;
            this.Window.KeyPressed -= OnPressKey;
            Global.sfx["songMatch"].stop();
            
        }




    }

}

