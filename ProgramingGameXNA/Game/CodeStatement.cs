﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ProgramingGameXNA.Game
{
    public class CodeStatement : GameObject
    {
        public static readonly CodeStatement Nop = new CodeNop();

        public override bool IsHUD { 
            set { 
                base.IsHUD = value;
                UpdateIsHUD();
            }
        }

        protected CodeStatement next = Nop;
        public CodeStatement Next
        {
            get { return next; }
            set {
                next = value;
                UpdateIsHUD();
                UpdatePosition();
            }
        }

        public override void UpdatePosition()
        {
            next.Position = new Vector2(0, 29) + position;
        }

        public virtual void UpdateIsHUD()
        {
            Next.IsHUD = IsHUD;
        }

        public virtual void Execute()
        {
            next.Execute();
        }

        private class CodeNop : CodeStatement
        {
            public override bool IsHUD
            {
                set {}
            }

            public override void Execute()
            {
            }
            public override void UpdatePosition()
            {
            }
        }
    }

    public class CodeCAO : CodeStatement
    {
        public enum CAOType
        {
            Add,
            Sub,
        }
        public CAOType Type { get; private set; }

        private CodeVariable left = CodeVariable.NullObject;
        private CodeVariable right = CodeVariable.NullObject;

        public CodeVariable Left {
            get { return left; }
            set {
                left = value;
                UpdateIsHUD();
                UpdatePosition();
            }
        }
        public CodeVariable Right
        {
            get { return right; }
            set
            {
                right = value;
                UpdateIsHUD();
                UpdatePosition();
            }
        }

        private Dictionary<CAOType, Action> executeTable;

        public CodeCAO(CAOType type)
        {
            this.Type = type;

            this.executeTable = new Dictionary<CAOType, Action>();
            executeTable[CAOType.Add] = ExecuteAdd;
            executeTable[CAOType.Sub] = ExecuteSub;
        }

        public override void  UpdatePosition()
        {
            Left.Position = new Vector2(33, 11) + position;
            Right.Position = new Vector2(88, 11) + position;
            base.UpdatePosition();
        }

        public override void UpdateIsHUD()
        {
            Left.IsHUD = IsHUD;
            Right.IsHUD = IsHUD;
            base.UpdateIsHUD();
        }

        private void ExecuteAdd()
        {
            Left.Value += Right.Value;
        }

        private void ExecuteSub()
        {
            Left.Value -= Right.Value;
        }

        public override void Execute()
        {
            if (Left == null) return;
            if (Right == null) return;

            var exe = executeTable[Type];
            exe();

            next.Execute();
        }

        #region Image
        private static Dictionary<CAOType, string> imageNameTable;
        public override string ImageName
        {
            get { return imageNameTable[Type]; }
        }
        static CodeCAO()
        {
            imageNameTable = new Dictionary<CAOType, string>();
            imageNameTable[CAOType.Add] = "Add";
            imageNameTable[CAOType.Sub] = "Sub";
        }
        #endregion
    }

    public class CodeEventTrigger : CodeStatement
    {
        public enum EventType
        {
            LeftKey,
            RightKey,
            UpKey,
            DownKey,
        }
        public EventType Type { get; private set; }

        private Dictionary<EventType, Keys> keyTable;

        public CodeEventTrigger(EventType type)
        {
            this.Type = type;

            this.keyTable = new Dictionary<EventType, Keys>();
            this.keyTable[EventType.LeftKey] = Keys.Left;
            this.keyTable[EventType.RightKey] = Keys.Right;
            this.keyTable[EventType.UpKey] = Keys.Up;
            this.keyTable[EventType.DownKey] = Keys.Down;
        }

        public override void Execute()
        {
            var keyboardState = Keyboard.GetState();
            var key = keyTable[Type];
            if (keyboardState.IsKeyDown(key))
            {
                this.next.Execute();
            }
        }

        #region Image
        private static Dictionary<EventType, string> imageNameTable;
        public override string ImageName
        {
            get { return imageNameTable[Type]; }
        }
        static CodeEventTrigger()
        {
            imageNameTable = new Dictionary<EventType, string>();
            imageNameTable[EventType.LeftKey] = "LeftKey";
            imageNameTable[EventType.RightKey] = "RightKey";
            imageNameTable[EventType.UpKey] = "UpKey";
            imageNameTable[EventType.DownKey] = "DownKey";
        }
        #endregion
    }
}
