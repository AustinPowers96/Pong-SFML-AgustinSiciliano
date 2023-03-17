using SFML.Graphics;
using SFML.System;
using System;

namespace pong_sfml.States
{
    public abstract class LoopState
    {
        public RenderWindow Window { get; set; }
        private bool isRunning;


        public LoopState (RenderWindow window)
        {
            this.Window = window;
            this.isRunning = true;
        }

        protected virtual void OnCloseWindow (object sender, EventArgs e)
        {
            isRunning = false;
            this.Window.Close();
        }

        protected abstract void Start ();
        protected abstract void Update (float deltaTime);
        protected abstract void Draw ();
        protected abstract void Finish ();

        public void Play ()
        {
            Clock clock = new Clock();

            isRunning = true;
            
            Start();

            while (isRunning)
            {
                
                Time deltaTime = clock.Restart();

                this.Window.DispatchEvents();

                Update(deltaTime.AsSeconds());

                this.Window.Clear(new Color(36, 36, 36));
                Draw();
                this.Window.Display();
            }

            Finish();
        }

        public void Stop ()
        {
            if (!isRunning)
            {
                Console.WriteLine("Cannot stop a state that is not running.");
                return;
            }

            isRunning = false;

            Finish();
        }
    }
}