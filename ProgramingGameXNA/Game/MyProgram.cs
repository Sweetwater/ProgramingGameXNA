using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgramingGameXNA.Game
{
    public class MyProgram : GameObject
    {
        private List<CodeStatement> codeList = new List<CodeStatement>();
        private Color frameColor;
        public MyProgram(Color frameColor)
        {
            this.IsHUD = true;
            this.frameColor = frameColor;
        }

        public void Add(CodeStatement code)
        {
            var x = codeList.Count * (128 + 10) + 10 + 64;
            var y = 10 + 16;
            code.Position = position + new Vector2(x, y);
            code.IsHUD = true;

            this.codeList.Add(code);
        }

        public void Replace(CodeStatement code)
        {
            for (int i = 0; i < codeList.Count; i++)
            {
                codeList[i].RemoveGame(); ;
            }
            this.codeList.Clear();

            var x = codeList.Count * (128 + 10) + 10 + 64;
            var y = 10 + 16;
            code.Position = position + new Vector2(x, y);
            code.IsHUD = true;

            this.codeList.Add(code);
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < codeList.Count; i++)
            {
                var code = codeList[i];
                code.Execute();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            var size = new Vector2(0, 100);
            size.X = codeList.Count * (128 + 10) + 10;

            graphics.FillRect2D(position, size, Color.Beige);
            graphics.DrawRect2D(position, size, frameColor);
        }
    }
}
