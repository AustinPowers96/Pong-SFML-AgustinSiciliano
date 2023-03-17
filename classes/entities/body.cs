using SFML.System;
using SFML.Graphics;

namespace pong_sfml 
{
    public abstract class body 
    {
        public enum enumBodyType 
        {
            point,
            circle,
            rectangle,
            polygon
        }

#region "Properties"
        internal enumBodyType bodytype;
        public enumBodyType BodyType => bodytype;

        internal Vector2f position;
        public Vector2f Position => position;

        internal Vector2f velocity;
        public Vector2f Velocity 
        {
            get { return velocity; }
            set { velocity = value; }
        }

        internal float angle;
        public float Angle 
        {
            get { return angle; }
            set { angle = value; }
        }

        internal float angularVelocity;
        public float AngularVelocity 
        {
            get { return angularVelocity; }
            set { angularVelocity = value; }
        }

        internal float drag;
        public float Drag 
        {
            get { return drag; }
            set { drag = value; }
        }

    #endregion
    #region "Methods"

        // Velocidad del juego
        public void update(float delta) 
        {
            SetPosition(Position + Velocity * delta);
            SetVelocity(Velocity * (1 - drag * delta));
            Angle += AngularVelocity * delta;
        }

        public virtual void draw(RenderWindow window) 
        { 
        
        }

        public void SetPosition(Vector2f pos) 
        {
            position = pos;
        }

        public void SetXPosition(float x) 
        {
            position.X = x;
        }

        public void SetYPosition(float y) 
        {
            position.Y = y;
        }

        public void SetVelocity(Vector2f vel) 
        {
            velocity = vel;
        }

        public void SetXVelocity(float x) 
        {
            velocity.X = x;
        }

        public void SetYVelocity(float y) 
        {
            velocity.Y = y;
        }

        public void AddVelocity(Vector2f vel) 
        {
            velocity += vel;
        }

        public void AddXVelocity(float x) 
        {
            velocity.X += x;
        }

        public void AddYVelocity(float y) 
        {
            velocity.Y += y;
        }
#endregion
    }
}