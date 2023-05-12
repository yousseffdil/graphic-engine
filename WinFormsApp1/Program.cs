using System;
using System.Drawing;
using System.Numerics;
using System.Threading;
using System.Windows.Forms;

namespace MiMotorGraficoOpenGL
{
    public class Ventana : Form
    {
        private System.Threading.Timer timer;
        private Square square;
        private Plane plane;
        private OpenGLRenderer renderer;

        public Ventana()
        {
            // Configuración de la ventana
            this.ClientSize = new Size(800, 600);
            this.Text = "Mi Motor Gráfico OpenGL";
            this.DoubleBuffered = true;

            // Creación del cuadrado y el plano
            square = new Square(new Vector2(400, 100), 50, 10);
            plane = new Plane(new Vector2(0, 400), 800, 200);

            // Inicializar el renderizador de OpenGL
            renderer = new OpenGLRenderer();
            renderer.Initialize();

            // Cargar la textura emisiva
            string texturePath = "C:\\Users\\fdily\\source\\repos\\WinFormsApp1\\WinFormsApp1\\emissive_texture.png"; // Reemplaza con la ruta de tu propia textura emisiva
            renderer.LoadTexture(texturePath);

            // Configuración del temporizador
            timer = new System.Threading.Timer(Timer_Tick, null, 0, 16);
        }

        private void Timer_Tick(object state)
        {
            // Actualizar la posición del cuadrado
            square.UpdatePosition();

            // Verificar colisión con el plano
            if (square.IsCollidingWith(plane))
            {
                // Simular rebote cambiando la dirección del cuadrado
                square.ReverseDirection();
            }

            // Volver a dibujar la escena
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Obtener el objeto Graphics para dibujar
            Graphics g = e.Graphics;

            // Llamar al renderizador de OpenGL para dibujar la escena
            renderer.Render(square, plane);

            // Actualizar el contenido del Graphics con lo dibujado por OpenGL
            g.Flush();
        }

        [STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new Ventana());
        }
    }

    public class Square
    {
        public Vector2 Position { get; private set; }
        public int Size { get; private set; }
        public int Mass { get; private set; }
        public Vector2 Velocity { get; private set; }
        public Vector2 Acceleration { get; private set; }

        public Square(Vector2 position, int size, int mass)
        {
            Position = position;
            Size = size;
            Mass = mass;
            Velocity = Vector2.Zero;
            Acceleration = new Vector2(0, 2); // Simulamos una aceleración gravitatoria hacia abajo
        }

        public void UpdatePosition()
        {
            // Actualizamos la velocidad y posición en función de la aceleración
            Velocity += Acceleration;
            Position += Velocity;
        }

        public void ReverseDirection()
        {
            // Invertimos la dirección de la velocidad para simular el rebote
            Velocity = new Vector2(Velocity.X, -Velocity.Y);
        }

        public bool IsCollidingWith(Plane plane)
        {
            // Verificamos si el cuadrado colisiona con el plano
            return Position.Y + Size >= plane.Position.Y;
        }
    }

    public class Plane
    {
        public Vector2 Position { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Plane(Vector2 position, int width, int height)
        {
            Position = position;
            Width = width;
            Height = height;
        }
    }
}

