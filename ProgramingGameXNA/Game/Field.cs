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

        public float CodeLimit = 64;

        public Field() : base()
        {
            this.Width = 640 * 1.5f;
            this.Height = 480 * 1.5f;
            this.position.X = -this.Width / 2;
            this.position.Y = -this.Height / 2;
        }

        public override void Draw(GameTime gameTime)
        {
            var size = new Vector2(Width, Height);

            graphics.FillRect2D(DrawPosition, size, Color.LightGreen);

            DrawCodeLimit();

            var leftWallPos = new Vector2(DrawPosition.X - 20, DrawPosition.Y - 20);
            var leftWallSize = new Vector2(20, Height + 40);
            graphics.FillRect2D(leftWallPos, leftWallSize, Color.Gray);

            var rightWallPos = new Vector2(DrawPosition.X + Width, DrawPosition.Y - 20);
            var rightWallSize = new Vector2(20, Height + 40);
            graphics.FillRect2D(rightWallPos, rightWallSize, Color.Gray);

            var bottomWallPos = new Vector2(DrawPosition.X, DrawPosition.Y + Height);
            var bottomWallSize = new Vector2(Width, 20);
            graphics.FillRect2D(bottomWallPos, bottomWallSize, Color.Gray);
        }


        private void DrawCodeLimit()
        {
            var position = new Vector2(
                DrawPosition.X + CodeLimit,
                DrawPosition.Y + CodeLimit);

            var size = new Vector2(
                Width - CodeLimit * 2,
                Height - CodeLimit * 2);

            graphics.DrawRect2D(position, size, Color.Red);
        }
    }
}
