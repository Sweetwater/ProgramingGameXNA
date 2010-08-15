using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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
        public MyProgram MyProperty { get; set; }

        public Player()
        {
        }
    }
}
