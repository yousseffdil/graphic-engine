using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Threading;
using System.Windows.Forms;

namespace MiMotorGrafico
{
    public class Ventana : Form
    {
        private System.Threading.Timer timer;
        private Square square;
        private Plane plane;

        public Ventana()
        {
            // Configuraci�n de la ventana
            this.ClientSize = new Size(800, 600);
            this.Text = "Mi Motor Gr�fico";
            this.DoubleBuffered = true;

            // Creaci�n del cuadrado y el plano
            square = new Square(new Vector2(400, 100), 50, 10);
            plane = new Plane(new Vector2(0, 400), 800, 200);

            // Configuraci�n del temporizador
            timer = new System.Threading.Timer(Timer_Tick, null, 0, 16);
        }

        private void Timer_Tick(object state)
        {
            // Actualizar la posici�n del cuadrado
            square.UpdatePosition();

            // Verificar colisi�n con el plano
            if (square.IsCollidingWith(plane))
            {
                // Simular rebote cambiando la direcci�n del cuadrado
                square.ReverseDirection();
            }

            // Volver a dibujar la escena
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Obtener el objeto Graphics para dibujar
            Graphics g = e.Graphics;

            // Limpiar el fondo
            g.Clear(Color.Black);

            // Dibujar el plano
            g.FillRectangle(Brushes.Gray, plane.Position.X, plane.Position.Y, plane.Width, plane.Height);

            // Dibujar el cuadrado
            g.FillRectangle(Brushes.GreenYellow, square.Position.X, square.Position.Y, square.Size, square.Size);
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
            Acceleration = new Vector2(0, 1); // Simulamos una aceleraci�n gravitatoria hacia abajo
        }

        public void UpdatePosition()
        {
            // Actualizamos la velocidad y posici�n en funci�n de la aceleraci�n
            Velocity += Acceleration;
            Position += Velocity;
        }

        public void ReverseDirection()
        {
            // Invertimos la direcci�n de la velocidad para simular el rebote
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