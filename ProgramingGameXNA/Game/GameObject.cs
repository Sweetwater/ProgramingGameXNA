using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LineBreak;

namespace ProgramingGameXNA.Game
{
    public class GameObject : DrawableGameComponent
    {
        protected static ProgramingGame game;
        protected static Dictionary<string , Texture2D> imageTable;
        protected static Graphics graphics;
        protected static Camera camera;


        public virtual bool IsHUD { get; set; }
        
        protected Vector2 position;
        public virtual Vector2 Position
        {
            get { return position; }
            set { 
                position = value;
                UpdatePosition();
            }
        }
        public virtual Vector2 DrawPosition {
            get 
            {
                return (Position - camera.Position) + camera.Origin;
            }
        }

        public virtual string ImageName
        {
            get { return ""; }
        }
        public virtual Texture2D Image
        {
            get { return imageTable[ImageName]; }
        }

        public Rectangle ImageRect {
            get
            {
                return new Rectangle(0, 0, Image.Width, Image.Height);
            
            }
        }

        public static void Setup(ProgramingGame game)
        {
            GameObject.game = game;
            GameObject.graphics = game.MyGraphics;
            GameObject.imageTable = game.ImageTable;
            GameObject.camera = game.Camera;
        }

        public GameObject() : base(game)
        {
        }

        public virtual void UpdatePosition()
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Image == null) return;

            if (IsHUD)
            {
                graphics.DrawTexture(Image, Position, ImageRect, Color.White);
            }
            else
            {
                graphics.DrawTexture(Image, DrawPosition, ImageRect, Color.White);
            }
        }
    }
}
