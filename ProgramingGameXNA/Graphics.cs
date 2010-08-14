using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LineBreak
{
    public class Graphics
    {
        private GraphicsDevice device;
        private SpriteBatch spriteBatch;
        private BasicEffect basicEffect;
        private VertexDeclaration vertexDeclaration;

        public void Setup(Microsoft.Xna.Framework.Game game)
        {
            device = game.GraphicsDevice;
            device.RenderState.AlphaBlendEnable = true;
            device.RenderState.SourceBlend = Blend.SourceAlpha;
            device.RenderState.DestinationBlend = Blend.One;
            spriteBatch = new SpriteBatch(device);
            basicEffect = new BasicEffect(device, null);
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter(
                    0,
                    game.Window.ClientBounds.Width,
                    game.Window.ClientBounds.Height,
                    0,
                    -1, 1);
            vertexDeclaration = new VertexDeclaration(device, VertexPositionColor.VertexElements);
        }

        public void SetViewMatrix(Matrix matrix)
        {
            basicEffect.View = matrix;
        }

        public void DrawTexture(
                Texture2D texture,
                Vector2 position,
                Rectangle rect,
                Color color)
        {
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            spriteBatch.Draw(texture, position, rect, color);
            spriteBatch.End();
        }

        public void SetPointSize(float size)
        {
            device.RenderState.PointSize = size;
        }

        public void DrawPoint2D(
                Vector2 p, Color color)
        {
            device.VertexDeclaration = vertexDeclaration;
            basicEffect.Begin();
            VertexPositionColor[] vertices = new VertexPositionColor[1];
            vertices[0] = new VertexPositionColor(new Vector3(p.X, p.Y, 0), color);
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Begin();
                device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.PointList, vertices, 0, 1);
                pass.End();
            }
            basicEffect.End();
        }


        public void DrawRect2D(
                Vector2 leftTop, Vector2 size, Color color)
        {
            device.VertexDeclaration = vertexDeclaration;
            basicEffect.Begin();
            VertexPositionColor[] vertices = new VertexPositionColor[5];
            vertices[0] = new VertexPositionColor(new Vector3(leftTop.X, leftTop.Y, 0), color);
            vertices[1] = new VertexPositionColor(new Vector3(leftTop.X + size.X, leftTop.Y, 0), color);
            vertices[2] = new VertexPositionColor(new Vector3(leftTop.X + size.X, leftTop.Y + size.Y, 0), color);
            vertices[3] = new VertexPositionColor(new Vector3(leftTop.X, leftTop.Y + size.Y, 0), color);
            vertices[4] = new VertexPositionColor(new Vector3(leftTop.X, leftTop.Y, 0), color);
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Begin();
                device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, vertices, 0, 4);
                pass.End();
            }
            basicEffect.End();
        }


        public void FillRect2D(
                Vector2 leftTop, Vector2 size, Color color)
        {
            device.VertexDeclaration = vertexDeclaration;
            basicEffect.Begin();
            VertexPositionColor[] vertices = new VertexPositionColor[4];
            vertices[0] = new VertexPositionColor(new Vector3(leftTop.X, leftTop.Y, 0), color);
            vertices[1] = new VertexPositionColor(new Vector3(leftTop.X + size.X, leftTop.Y, 0), color);
            vertices[2] = new VertexPositionColor(new Vector3(leftTop.X, leftTop.Y + size.Y, 0), color);
            vertices[3] = new VertexPositionColor(new Vector3(leftTop.X + size.X, leftTop.Y + size.Y, 0), color);
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Begin();
                device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, vertices, 0, 2);
                pass.End();
            }
            basicEffect.End();
        }

        public void DrawLine2D(
                Vector2 begin, Color beginColor,
                Vector2 end, Color endColor)
        {
            VertexPositionColor[] vertices = new VertexPositionColor[2];
            vertices[0] = new VertexPositionColor(new Vector3(begin.X, begin.Y, 0), beginColor);
            vertices[1] = new VertexPositionColor(new Vector3(end.X, end.Y, 0), endColor);
            DrawLine2D(vertices);
        }

        public void DrawLine2D(List<VertexPositionColor> vertices)
        {
            DrawLine2D(vertices.ToArray());
        }

        public void DrawLine2D(VertexPositionColor[] vertices)
        {
            device.VertexDeclaration = vertexDeclaration;
            basicEffect.Begin();
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Begin();
                device.DrawUserPrimitives<VertexPositionColor>(
                        PrimitiveType.LineList,
                        vertices,
                        0, vertices.Length / 2);
                pass.End();
            }
            basicEffect.End();
        }
    }
}