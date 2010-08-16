using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LineBreak;
using Microsoft.Xna.Framework.Input;

namespace ProgramingGameXNA.Game
{
    public class GameObject : DrawableGameComponent
    {
        protected static ProgramingGame game;
        protected static Dictionary<string , Texture2D> imageTable;
        protected static Graphics graphics;
        protected static Camera camera;

        public Vector3 BoxMin { get; protected set; }
        public Vector3 BoxMax { get; protected set; }
        public BoundingBox BoundingBox {
            get {
                var position3D = new Vector3(Position, 0f);
                return new BoundingBox(
                        BoxMin + position3D,
                        BoxMax + position3D);
            }
        }

        protected bool isHUD;
        public virtual bool IsHUD {
            get { return isHUD; }
            set { 
                isHUD = value;
                this.DrawOrder = isHUD ? 10 : 0;
            }
        }
        
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
                var width = Image == null ? 0 : Image.Width / 2;
                var height = Image == null ? 0 : Image.Height / 2;
                var drawPosition = Position - new Vector2(width, height);
                return (drawPosition - camera.Position) + camera.Origin;
            }
        }

        public BoundingBox HitBox { get; set; }

        public virtual string ImageName
        {
            get { return ""; }
        }
        public virtual Texture2D Image
        {
            get { return imageTable == null ? null : imageTable[ImageName]; }
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

        public override void Initialize()
        {
            InitializeBox();
        }

        public virtual void InitializeBox()
        {
            if (Image == null)
            {
                this.BoxMin = Vector3.Zero;
                this.BoxMax = Vector3.Zero;
            }
            else
            {
                var width = Image.Width / 2;
                var height = Image.Height / 2;
                this.BoxMin = new Vector3(-width, -height, 0);
                this.BoxMax = new Vector3(width, height, 0);
            }
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
                var drawPosition = Position - new Vector2(ImageRect.Width/2, ImageRect.Height/2);
                graphics.DrawTexture(Image, drawPosition, ImageRect, Color.White);
            }
            else
            {
                var state = Keyboard.GetState();
                if (state.IsKeyDown(Keys.V) == false)
                    graphics.DrawTexture(Image, DrawPosition, ImageRect, Color.White);

                if (state.IsKeyDown(Keys.C))
                {
                    DrawBoundingBox(BoundingBox, Color.Red);
                    if (HitBox != null)
                    {
                        DrawBoundingBox(HitBox, Color.Yellow);
                    }
                }
            }
        }

        private BoundingBox DrawBoundingBox(BoundingBox box, Color color)
        {
            var pos = new Vector2(box.Min.X, box.Min.Y);
            var size = new Vector2(box.Max.X - box.Min.X, box.Max.Y - box.Min.Y);
            pos = pos - camera.position + camera.Origin;
            graphics.DrawRect2D(pos, size, color);
            return box;
        }

        public virtual bool IsCollisionTarget(GameObject target)
        {
            return true;
        }
    }
}
