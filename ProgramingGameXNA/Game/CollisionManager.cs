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
                CheckBoundingHit2(object1, FieldObjectList[i]);
            }
        }

        private void CheckBoundingHit(GameObject object1, GameObject object2)
        {
            var box1 = object1.BoundingBox;
            var box2 = object2.BoundingBox;
            var isHit = box1.Intersects(box2);
            if (isHit == false) return;

            var intersects = GetIntersectsBox(box1, box2);
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

            var intersects = GetIntersectsBox(box1, box2);
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

            if (float.IsNaN(vector1.X) || float.IsNaN(vector1.Y))
            {
                var a = 0;
            }
            if (float.IsNaN(vector2.X) || float.IsNaN(vector2.Y))
            {
                int b = 0;
            }

            object1.Position = object1.Position + velocity1;
            object2.Position = object2.Position + velocity2;
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

        private BoundingBox GetIntersectsBox(BoundingBox box1, BoundingBox box2)
        {
            var box = new BoundingBox();
            box.Min.X = (float)Math.Max(box1.Min.X, box2.Min.X);
            box.Min.Y = (float)Math.Max(box1.Min.Y, box2.Min.Y);
            box.Max.X = (float)Math.Min(box1.Max.X, box2.Max.X);
            box.Max.Y = (float)Math.Min(box1.Max.Y, box2.Max.Y);
            return box;
        }
    }

    public static class Utils
    {
        public static Vector2 Center(this BoundingBox self)
        {
            var center3 = self.Min + (self.Max - self.Min) / 2;
            var center2 = new Vector2(center3.X, center3.Y);
            return center2;
        }

        public static Vector2 Size(this BoundingBox self)
        {
            var size2 = (self.Max - self.Min);
            var size3 = new Vector2(size2.X, size2.Y);
            return size3;
        }
    }
}
