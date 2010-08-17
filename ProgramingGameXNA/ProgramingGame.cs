using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using LineBreak;
using ProgramingGameXNA.Game;

namespace ProgramingGameXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ProgramingGame : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager Graphics { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }

        public Graphics MyGraphics { get; private set; }
        public Dictionary<string, Texture2D> ImageTable { get; private set; }

        public Field Field { get; private set; }
        public Camera Camera { get; private set; }

        public Player Player { get; private set; }
        public MyProgram MyProgram1 { get; private set; }
        public MyProgram MyProgram2 { get; private set; }
        public List<GameObject> FieldCodeList { get; private set; }

        public CollisionManager CollisionManager { get; private set; }
        public SnapManager SnapManager { get; private set; }

        public Random random { get; private set; }

        private string[] imageNames = {
            "Player",
            "Sp10",
            "Sp05",
            "Sp01",
            "PosY",
            "PosX",
            "Dir",
            "LeftKey",
            "RightKey",
            "UpKey",
            "DownKey",
            "Add",
            "Sub",
        };

        public ProgramingGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            MyGraphics = new Graphics();
            Content.RootDirectory = "Content";

            Graphics.PreferredBackBufferWidth = 640;
            Graphics.PreferredBackBufferHeight = 480;

            IsFixedTimeStep = true;
            IsMouseVisible = true;

            random = new Random();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            MyGraphics.Setup(this);

            ImageTable = new Dictionary<string, Texture2D>();
            foreach (var name in imageNames)
            {
                ImageTable[name] = Content.Load<Texture2D>(name);
            }
            ImageTable[""] = null;

            Camera = new Camera();
            Camera.Origin = new Vector2(320, 240);
            this.Components.Add(Camera);

            Field = new Field();
            this.Components.Add(Field);

            MyProgram1 = new MyProgram(Color.MediumSeaGreen);
            MyProgram1.Position = new Vector2(10, 20);
            this.Components.Add(MyProgram1);

            MyProgram2 = new MyProgram(Color.Red);
            MyProgram2.Position = new Vector2(10, 200);
            this.Components.Add(MyProgram2);

            var posX1 = new CodePosition(CodePosition.PositionType.X);
            var posX2 = new CodePosition(CodePosition.PositionType.X);
            var posY3 = new CodePosition(CodePosition.PositionType.Y);
            var posY4 = new CodePosition(CodePosition.PositionType.Y);

            var sp1_1 = new CodeSpeed(CodeSpeed.SpeedType.Speed05);
            var sp1_2 = new CodeSpeed(CodeSpeed.SpeedType.Speed01);
            var sp1_3 = new CodeSpeed(CodeSpeed.SpeedType.Speed05);
            var sp1_4 = new CodeSpeed(CodeSpeed.SpeedType.Speed05);

            var sub1 = new CodeCAO(CodeCAO.CAOType.Sub);
            var add2 = new CodeCAO(CodeCAO.CAOType.Add);
            var sub3 = new CodeCAO(CodeCAO.CAOType.Sub);
            var add4 = new CodeCAO(CodeCAO.CAOType.Add);

            var left = new CodeEventTrigger(CodeEventTrigger.EventType.LeftKey);
            var right = new CodeEventTrigger(CodeEventTrigger.EventType.RightKey);
            var up = new CodeEventTrigger(CodeEventTrigger.EventType.UpKey);
            var down = new CodeEventTrigger(CodeEventTrigger.EventType.DownKey);

            this.Components.Add(posX1);
            this.Components.Add(posX2);
            this.Components.Add(posY3);
            this.Components.Add(posY4);

            this.Components.Add(sp1_1);
            this.Components.Add(sp1_2);
            this.Components.Add(sp1_3);
            this.Components.Add(sp1_4);

            this.Components.Add(sub1);
            this.Components.Add(add2);
            this.Components.Add(sub3);
            this.Components.Add(add4);

            this.Components.Add(left);
            this.Components.Add(right);
            this.Components.Add(up);
            this.Components.Add(down);

            this.FieldCodeList = new List<GameObject>();

            Player = new Player();
            Player.Position = new Vector2(0, 0);
            this.Components.Add(Player);
            this.FieldCodeList.Add(Player);

            var factoryTable = new Dictionary<int, Func<GameObject>>();
            factoryTable[0] = () => new CodeEventTrigger(CodeEventTrigger.EventType.LeftKey); 
            factoryTable[1] = () => new CodeEventTrigger(CodeEventTrigger.EventType.RightKey); 
            factoryTable[2] = () => new CodeEventTrigger(CodeEventTrigger.EventType.UpKey); 
            factoryTable[3] = () => new CodeEventTrigger(CodeEventTrigger.EventType.DownKey); 

            factoryTable[4] = () => new CodeCAO(CodeCAO.CAOType.Add); 
            factoryTable[5] = () => new CodeCAO(CodeCAO.CAOType.Sub); 

            factoryTable[6] = () => new CodePosition(CodePosition.PositionType.X);
            factoryTable[7] = () => new CodePosition(CodePosition.PositionType.Y);

            factoryTable[8] = () => new CodeSpeed(CodeSpeed.SpeedType.Speed01);
            factoryTable[9] = () => new CodeSpeed(CodeSpeed.SpeedType.Speed05);
            factoryTable[10] = () => new CodeSpeed(CodeSpeed.SpeedType.Speed10);

            var statementList = new List<CodeStatement>();
            var variableList = new List<CodeVariable>();

            for (int i = 0; i < 10; i++)
            {
                int type = i;// random.Next(factoryTable.Count);
                var code = factoryTable[type]();
                code.Position = GetRandomPosition();
                this.FieldCodeList.Add(code);
                this.Components.Add(code);

                if (type < 6) statementList.Add((CodeStatement)code);
                else variableList.Add((CodeVariable)code);
            }

            this.SnapManager = new SnapManager(this);
            SnapManager.StatementList = statementList;
            SnapManager.VariableList = variableList;
            this.Components.Add(SnapManager);

            CollisionManager = new CollisionManager();
            CollisionManager.Field = Field;
            CollisionManager.Player = Player;
            CollisionManager.FieldObjectList = FieldCodeList;
            this.Components.Add(CollisionManager);

            GameObject.Setup(this);

            for (int i = 0; i < Components.Count; i++)
			{
			    Components[i].Initialize();
			}

            sub1.Left = posX1;
            sub1.Right = sp1_1;
            left.Next = sub1;
            MyProgram1.Add(left);

            add2.Left = posX2;
            add2.Right = sp1_2;
            right.Next = add2;
            MyProgram2.Add(right);

            sub3.Left = posY3;
            sub3.Right = sp1_3;
            up.Next = sub3;
            MyProgram1.Add(up);

            add4.Left = posY4;
            add4.Right = sp1_4;
            down.Next = add4;
            MyProgram1.Add(down);

            Player.MyProgram = MyProgram2;
            Player.StatementList = statementList;
        }

        private Vector2 GetRandomPosition()
        {
            var x = GetRandom(Field.Width * 2 / 3);
            var y = GetRandom(Field.Height * 2 / 3);
            return new Vector2(x, y);
        }

        private float GetRandom(float range)
        {
            return (float)(random.NextDouble() * range) - (range / 2);
        }

        private void CreatePresetCode(CodeFactory factory)
        {
            //var codeUp = factory.CreateTriggetUp();
            //var codeSub = factory.CreateSub();
            //var posY = factory.CreatePositionY();
            //var speed5 = factory.CreateSpeed05();
            //codeUp.Next = codeSub;
            //codeSub.Left = posY;
            //codeSub.Right = speed5;

            //myCode.Add(codeUp);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            // TODO: Add your update logic here
            Camera.Position = Player.Position;


//            if (keyboardState.IsKeyDown(Keys.A))
//            {
//                if (myCode.partsList.Count == 0)
//                {
//                    myCode.position = new Vector2(100, 100);
////                    myCode.Add(addStatement);
//                    //                    addStatement.SetVariable(myPositionX, speed10);
//                }
//            }

//            myCode.Execute();


            //var speed = 2;
            //var position = Player.Position;
            //if (keyboardState.IsKeyDown(Keys.Left)) position.X -= speed;
            //if (keyboardState.IsKeyDown(Keys.Right)) position.X += speed;
            //if (keyboardState.IsKeyDown(Keys.Up)) position.Y -= speed;
            //if (keyboardState.IsKeyDown(Keys.Down)) position.Y += speed;
            //Player.Position = position;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            //var imageRect = new Rectangle(0, 0, playerImage.Width, playerImage.Height);
            //myGraphics.DrawTexture(playerImage, playerPosition, imageRect, Color.White);

            //myCode.DrawImage(myGraphics);

            //for (int i = 0; i < fieldPartsList.Count; i++)
            //{
            //    var parts = fieldPartsList[i];
            //    parts.Draw(myGraphics);
            //}

            base.Draw(gameTime);
        }
    }
}
