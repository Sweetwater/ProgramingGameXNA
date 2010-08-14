using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ProgramingGameXNA.Game
{
    public class CollisionManager : GameObject
    {
        public Field Field { get; set; }
        public Player Player { get; set; }

        public override void Update(GameTime gameTime)
        {
            var playerPosition = Player.Position;
            if (playerPosition.X < Field.Left) playerPosition.X = Field.Right;
            if (playerPosition.X > Field.Right) playerPosition.X = Field.Left;
            if (playerPosition.Y < Field.Top) playerPosition.Y = Field.Bottom;
            if (playerPosition.Y > Field.Bottom) playerPosition.Y = Field.Top;

            Player.Position = playerPosition;
            base.Update(gameTime);
        }
    }
}
