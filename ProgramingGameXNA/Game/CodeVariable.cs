using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramingGameXNA.Game
{
    public class CodeVariable : GameObject
    {
        public static readonly CodeVariable NullObject = new CodeVariable();

        protected float value;
        public virtual float Value {
            get { return value; }
            set { this.value = value; }
        }

        protected CodeStatement parent = CodeStatement.Nop;
        public CodeStatement Parent {
            get { return parent; }
            set { parent = value; }
        }

        public override bool IsCollisionTarget(GameObject target)
        {
            if (target == Parent)
            {
                return false;
            }
            return base.IsCollisionTarget(target);
        }


    }

    public class CodePosition : CodeVariable
    {
        public enum PositionType
        {
            X,
            Y,
        }
        public PositionType Type { get; private set; }

        private Dictionary<PositionType, Func<float>> getValueTable;
        private Dictionary<PositionType, Action<float>> setValueTable;

        public override float Value
        {
            get { return getValueTable[Type](); }
            set { setValueTable[Type](value); }
        }

        public CodePosition(PositionType type)
        {
            this.Type = type;

            getValueTable = new Dictionary<PositionType, Func<float>>();
            getValueTable[PositionType.X] = () => game.Player.Position.X;
            getValueTable[PositionType.Y] = () => game.Player.Position.Y;

            setValueTable = new Dictionary<PositionType, Action<float>>();
            setValueTable[PositionType.X] = (value) =>
            {
                var position = game.Player.Position;
                position.X = value;
                game.Player.Position = position;
            };
            setValueTable[PositionType.Y] = (value) =>
            {
                var position = game.Player.Position;
                position.Y = value;
                game.Player.Position = position;
            };
        }

        #region Image
        private static Dictionary<PositionType, string> imageNameTable;
        public override string ImageName
        {
            get { return imageNameTable[Type]; }
        }
        static CodePosition()
        {
            imageNameTable = new Dictionary<PositionType, string>();
            imageNameTable[PositionType.X] = "PosX";
            imageNameTable[PositionType.Y] = "PosY";
        }
        #endregion
    }

    public class CodeSpeed : CodeVariable
    {
        public enum SpeedType
        {
            Speed01 = 1,
            Speed05 = 3,
            Speed10 = 5,
        }
        public SpeedType Type { get; private set; }

        public override float Value
        {
            get
            {
                return (float)Type;
            }
            set
            {
                return;
            }
        }

        public CodeSpeed(SpeedType type)
        {
            this.Type = type;
        }

        #region Image
        private static Dictionary<SpeedType, string> imageNameTable;
        public override string ImageName
        {
            get { return imageNameTable[Type]; }
        }

        static CodeSpeed()
        {
            imageNameTable = new Dictionary<SpeedType, string>();
            imageNameTable[SpeedType.Speed01] = "Sp01";
            imageNameTable[SpeedType.Speed05] = "Sp05";
            imageNameTable[SpeedType.Speed10] = "Sp10";
        }
        #endregion
    }
}
