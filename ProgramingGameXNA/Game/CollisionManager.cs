using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ProgramingGameXNA.Game
{
    public class CollisionManager : GameObject
    {
        public Field Field { get; set; }
        public Player Player { get; set; }
        public List<GameObject> FieldObjectList { get; set; }
        public Dictionary<GameObject, bool> isHitTable;

        public CollisionManager()
        {
            this.FieldObjectList = new List<GameObject>();
        }

        public override void Update(GameTime gameTime)
        {
            CheckFieldLimit(Player);
            for (int i = 0; i < FieldObjectList.Count; i++)
			{
                var gameObject = FieldObjectList[i];
//                CheckFieldLimit(gameObject);
			}

            isHitTable = new Dictionary<GameObject, bool>();
            for (int i = 0; i < FieldObjectList.Count; i++)
            {
                isHitTable[FieldObjectList[i]] = false;
            }

            count = 0;
            for (int i = 0; i < FieldObjectList.Count; i++)
            {
                CheckBoundingAll(FieldObjectList[i]); 
            }
            base.Update(gameTime);

            Console.WriteLine("hit check count:" + count);
        }
        int count;

        private void CheckFieldLimit(GameObject gameObject)
        {
            var position = gameObject.Position;
            if (position.X < Field.Left) position.X = Field.Right;
            if (position.X > Field.Right) position.X = Field.Left;
            if (position.Y < Field.Top) position.Y = Field.Bottom;
            if (position.Y > Field.Bottom) position.Y = Field.Top;
            gameObject.Position = position;
        }

        private void CheckBoundingAll(GameObject object1)
        {
            for (int i = 0; i < FieldObjectList.Count; i++)
            {
                if (object1 == FieldObjectList[i]) continue;
                if (object1.IsCollisionTarget(FieldObjectList[i]) == false) continue;

                CheckBoundingHit2(object1, FieldObjectList[i]);
            }
        }

        private void CheckBoundingHit(GameObject object1, GameObject object2)
        {
            var box1 = object1.BoundingBox;
            var box2 = object2.BoundingBox;
            var isHit = box1.Intersects(box2);
            if (isHit == false) return;

            var intersects = Utility.GetIntersectsBox(box1, box2);
            var signed = object2.Position - object1.Position;
            signed.Normalize();
            var size = (intersects.Max - intersects.Min);

            var position = object2.Position;
            if (size.X > size.Y) position.Y += size.Y * signed.Y;
            else position.X += size.X * signed.X;
            object2.Position = position;
        }

        private void CheckBoundingHit2(GameObject object1, GameObject object2)
        {
            count++;
            var box1 = object1.BoundingBox;
            var box2 = object2.BoundingBox;
            var isHit = box1.Intersects(box2);
            if (isHit == false) return;

            var intersects = Utility.GetIntersectsBox(box1, box2);
            var center = intersects.Center();
            var size = intersects.Size();
            var center1 = box1.Center();
            var center2 = box2.Center();

            var vector1 = center1 - center;
            var vector2 = center2 - center;
            if (size.X < size.Y) vector1.Y = vector2.Y = 0;
            else vector1.X = vector2.X = 0;
            if (vector1.LengthSquared() == 0 && vector2.LengthSquared() == 0)
            {
                vector1.X = (float)game.random.NextDouble();
                vector1.Y = (float)game.random.NextDouble();
                vector2 = -vector1;
            }
            else if (vector1.LengthSquared() == 0)
            {
                vector1 = -vector2;
            }
            else if (vector2.LengthSquared() == 0)
            {
                vector2 = -vector1;
            }
            vector1.Normalize();
            vector2.Normalize();

            var velocity1 = vector1 * ((size / 2) + new Vector2(0.1f, 0.1f));
            var velocity2 = vector2 * ((size / 2) + new Vector2(0.1f, 0.1f));

            var statement1 = object1 as CodeStatement;
            var statement2 = object2 as CodeStatement;

            if (statement1 == null) object1.Position = object1.Position + velocity1;
            else statement1.OffsetPosition(velocity1);

            if (statement2 == null) object2.Position = object2.Position + velocity2;
            else statement2.OffsetPosition(velocity2);
            
            if (isHitTable[object1] == false)
            {
                isHitTable[object1] = true;
                CheckBoundingAll(object1);
            }
            if (isHitTable[object2] == false)
            {
                isHitTable[object2] = true;
                CheckBoundingAll(object2);
            }
        }

    }
}
