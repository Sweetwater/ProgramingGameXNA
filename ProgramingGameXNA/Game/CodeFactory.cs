using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace ProgramingGameXNA.Game
{
    public class CodeFactory
    {
        private ProgramingGame game;
        private Dictionary<string, Texture2D> imageTable;

        public CodeFactory(ProgramingGame game, Dictionary<string, Texture2D> imageTable)
        {
            this.game = game;
            this.imageTable = imageTable;
        }

        //public CodePosition CreatePositionX()
        //{
        //    var codePos = new CodePosition(game, imageTable["PosX"]);
        //    codePos.type = CodePosition.Type.X;
        //    return codePos;
        //}

        //public CodePosition CreatePositionY()
        //{
        //    var codePos = new CodePosition(game, imageTable["PosY"]);
        //    codePos.type = CodePosition.Type.Y;
        //    return codePos;
        //}

        //public CodeSpeed CreateSpeed10()
        //{
        //    var codeSpeed = new CodeSpeed(game, imageTable["Sp10"]);
        //    codeSpeed.type = CodeSpeed.Type.Speed10;
        //    return codeSpeed;
        //}

        //public CodeSpeed CreateSpeed05()
        //{
        //    var codeSpeed = new CodeSpeed(game, imageTable["Sp05"]);
        //    codeSpeed.type = CodeSpeed.Type.Speed05;
        //    return codeSpeed;
        //}

        //public CodeSpeed CreateSpeed01()
        //{
        //    var codeSpeed = new CodeSpeed(game, imageTable["Sp01"]);
        //    codeSpeed.type = CodeSpeed.Type.Speed01;
        //    return codeSpeed;
        //}

        //public CodeEventTrigger CreateTriggetLeft()
        //{
        //    var codeTrigger = new CodeEventTrigger(game, imageTable["LeftKey"]);
        //    codeTrigger.type = CodeEventTrigger.Type.Left;
        //    return codeTrigger;
        //}

        //public CodeEventTrigger CreateTriggetRight()
        //{
        //    var codeTrigger = new CodeEventTrigger(game, imageTable["RightKey"]);
        //    codeTrigger.type = CodeEventTrigger.Type.Right;
        //    return codeTrigger;
        //}

        //public CodeEventTrigger CreateTriggetUp()
        //{
        //    var codeTrigger = new CodeEventTrigger(game, imageTable["UpKey"]);
        //    codeTrigger.type = CodeEventTrigger.Type.Up;
        //    return codeTrigger;
        //}

        //public CodeEventTrigger CreateTriggetDown()
        //{
        //    var codeTrigger = new CodeEventTrigger(game, imageTable["DownKey"]);
        //    codeTrigger.type = CodeEventTrigger.Type.Down;
        //    return codeTrigger;
        //}

        //public CodeCAO CreateAdd()
        //{
        //    var codeAdd = new CodeCAO(game, imageTable["Add"]);
        //    codeAdd.type = CodeCAO.Type.Add;
        //    return codeAdd;
        //}

        //public CodeCAO CreateSub()
        //{
        //    var codeSub = new CodeCAO(game, imageTable["Sub"]);
        //    codeSub.type = CodeCAO.Type.Sub;
        //    return codeSub;
        //}
    }
}
