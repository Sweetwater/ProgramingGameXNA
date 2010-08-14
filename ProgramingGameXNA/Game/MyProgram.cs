﻿using System;
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
        public MyProgram()
        {
        }

        public void Add(CodeStatement code)
        {
            this.codeList.Add(code);
            code.Position = position + new Vector2(10, 10);
            code.IsHUD = true;
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

            graphics.DrawRect2D(position, size, Color.MediumSeaGreen);
        }
    }
}