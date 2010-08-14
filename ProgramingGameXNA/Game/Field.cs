using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgramingGameXNA.Game
{
    public class Field : GameObject
    {
        public float Width { get; set; }
        public float Height { get; set; }

        public float Left { get { return Position.X; } }
        public float Right { get { return Position.X + Width; } }
        public float Top { get { return Position.Y; } }
        public float Bottom { get { return Position.Y + Height; } }

        public Field() : base()
        {
            this.Width = 640 * 2f;
            this.Height = 480 * 2f;
            this.position.X = -this.Width / 2;
            this.position.Y = -this.Height / 2;
        }


        public override void Draw(GameTime gameTime)
        {
            var size = new Vector2(Width, Height);

            graphics.FillRect2D(DrawPosition, size, Color.LightGreen);
        }
    }
}
