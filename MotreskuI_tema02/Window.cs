using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Tema_2
{
    internal class Window : GameWindow
    {
        float lineLength = 15;
        int lineWidth = 1;
        private bool isMouseDragging = false;
        private int lastMouseY;

        public Window() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            Console.WriteLine("OpenGl versiunea: " + GL.GetString(StringName.Version));
            Title = "OpenGl versiunea: " + GL.GetString(StringName.Version) + " (mod imediat)";

            //KeyDown += Keyboard_KeyDown;
            MouseDown += Mouse_ButtonDown;
            MouseUp += Mouse_ButtonUp;
            MouseMove += Mouse_Move;

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Blue);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            Matrix4 lookat = Matrix4.LookAt(30, 30, 30, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);


        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyboard.IsKeyDown(Key.W) && keyboard.IsKeyDown(Key.Up))
            {
                lineWidth += 1;
            }

            if (keyboard.IsKeyDown(Key.W) && keyboard.IsKeyDown(Key.Down))
            {
                lineWidth -= 1;
            }

            //if (mouse.IsButtonDown(MouseButton.Left))
            //{
            //    float deltaY = mouse.Y - lastMouseY;

            //    lineLength += deltaY / 5;

            //    lastMouseY = mouse.Y;
            //}

            if (keyboard[Key.Escape])
            {
                Exit();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);




            //DrawAxes();

            DrawSquare();



            SwapBuffers();
        }

        private void DrawSquare()
        {
            GL.LineWidth(lineWidth);

            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(lineLength, lineLength, 0);
            GL.Vertex3(lineLength, -lineLength, 0);
            GL.End();

            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Green);
            GL.Vertex3(lineLength, -lineLength, 0);
            GL.Vertex3(-lineLength, -lineLength, 0);
            GL.End();

            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Yellow);
            GL.Vertex3(-lineLength, -lineLength, 0);
            GL.Vertex3(-lineLength, lineLength, 0);
            GL.End();

            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Orange);
            GL.Vertex3(-lineLength, lineLength, 0);
            GL.Vertex3(lineLength, lineLength, 0);
            GL.End();
        }

        void Mouse_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                isMouseDragging = true;  
                lastMouseY = e.Y;        
            }
        }

        void Mouse_ButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                isMouseDragging = false;  
            }
        }
        void Mouse_Move(object sender, MouseMoveEventArgs e)
        {
            if (isMouseDragging)
            {
                float deltaY = e.Y - lastMouseY;

                lineLength += deltaY / 5;

                lastMouseY = e.Y;

            }
        }

        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();

            if (e.Key == Key.F)
                if (this.WindowState == WindowState.Fullscreen)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Fullscreen;

            if (e.Key == Key.Up)
                lineWidth += 1;
            else if (e.Key == Key.Down)
                lineWidth -= 1;


        }


        [STAThread]
        static void Main(string[] args)
        {

            /**Utilizarea cuvântului-cheie "using" va permite dealocarea memoriei o dată ce obiectul nu mai este
               în uz (vezi metoda "Dispose()").
               Metoda "Run()" specifică cerința noastră de a avea 30 de evenimente de tip UpdateFrame per secundă
               și un număr nelimitat de evenimente de tip randare 3D per secundă (maximul suportat de subsistemul
               grafic). Asta nu înseamnă că vor primi garantat respectivele valori!!!
               Ideal ar fi ca după fiecare UpdateFrame să avem si un RenderFrame astfel încât toate obiectele generate
               în scena 3D să fie actualizate fără pierderi (desincronizări între logica aplicației și imaginea randată
               în final pe ecran). */
            using (Window example = new Window())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}
