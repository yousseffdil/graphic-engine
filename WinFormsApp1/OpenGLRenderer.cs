using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace MiMotorGraficoOpenGL
{
    public class OpenGLRenderer
    {
        private int textureID;

        public OpenGLRenderer()
        {
        }

        public void Initialize()
        {
            // Configurar OpenGL
            OpenTK.Graphics.OpenGL.GL.ClearColor(OpenTK.Graphics.Color4.Black);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 0.0f, 0.0f, 1.0f, 0.0f });

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, 800, 600, 0, -1, 1);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        public void LoadTexture(string path)
        {
            textureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureID);

            // Cargar la imagen de la textura
            Bitmap bitmap = new Bitmap(path); // Reemplaza "emissive_texture.png" con la ruta de tu propia textura emisiva

            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);

            // Configurar los parámetros de la textura
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }

        public void Render(Square square, Plane plane)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Renderizar el cuadrado con la textura emisiva
            GL.BindTexture(TextureTarget.Texture2D, textureID);

            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(square.Position.X, square.Position.Y);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(square.Position.X + square.Size, square.Position.Y);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(square.Position.X + square.Size, square.Position.Y + square.Size);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(square.Position.X, square.Position.Y + square.Size);
            GL.End();

            // Renderizar el plano
            // Renderizar el plano
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.5f, 0.5f, 0.5f);
            GL.Vertex2(plane.Position.X + plane.Width, plane.Position.Y + plane.Height);
            GL.Vertex2(plane.Position.X, plane.Position.Y + plane.Height);
            GL.Vertex2(plane.Position.X, plane.Position.Y);
            GL.Vertex2(plane.Position.X + plane.Width, plane.Position.Y);
            GL.EndRender();

            GL.Flush();
        }
    }
}
