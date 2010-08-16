using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ProgramingGameXNA.Game
{
    public class Player : GameObject
    {
        public override Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public override string ImageName {
            get { return "Player"; }
        }
        public MyProgram MyProgram { get; set; }

        public List<CodeStatement> StatementList;
        public BoundingBox implBox;

        public Player()
        {
            this.implBox = new BoundingBox();
            implBox.Min = new Vector3(-8, 16, 0);
            implBox.Max = new Vector3(8, 32, 0);
        }

        public override void Update(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Space))
            {
                ImplementCode();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            var pos = position - camera.Position + camera.Origin;
            var box = implBox.Offset(pos);
            graphics.DrawRect2D(box.LeftTop(), box.Size(), Color.Violet);
        }

        private void ImplementCode()
        {
            var impBox = this.implBox.Offset(Position);
            for (int i = 0; i < StatementList.Count; i++)
            {
                var statement = StatementList[i];
                var stateBox = statement.BoundingBox;
                if (impBox.Intersects(stateBox) == false) continue;

                MyProgram.Add(statement);
            }
        }
    }
}
