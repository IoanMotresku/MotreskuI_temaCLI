using OpenTK;
using System;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;
using Tema_3;
using System.Configuration;
using Tema_3.Enum;
using System.Threading;
//using static OpenTK.Graphics.OpenGL.GL;

namespace Tema_3
{
    public class Window : GameWindow
    {
        private Camera camera;
        private readonly string cameraConfig;
        private Perspective perspective;
        private readonly string perspectiveConfig;
        private Triangle triangle;
        private readonly string triangleConfig;
        private Axe axes;
        private readonly string axesConfig;

        private Randomizer randomizer;

        private KeyboardState lastKeyboard;

        private Color DEFAULT_BACK_COLOR = Color.Indigo;

        private bool showAxis = false;

        public Window() : base(800, 600, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            int screenWidth = DisplayDevice.Default.Width;
            int screenHeight = DisplayDevice.Default.Height;
            // Calcularea pozitiei de centrare a ferestrei
            int windowX = (screenWidth - Width) / 2;
            int windowY = (screenHeight - Height) / 2;
            // Setarea pozitiei a ferestrei
            Location = new Point(windowX, windowY);
                    
            cameraConfig = ConfigurationManager.AppSettings["cameraConfig"];
            perspectiveConfig = ConfigurationManager.AppSettings["perspectiveConfig"];
            triangleConfig = ConfigurationManager.AppSettings["triangleConfig"];

            camera = new Camera(cameraConfig);
            perspective = new Perspective(perspectiveConfig);

            randomizer = new Randomizer();

            triangle = new Triangle(triangleConfig);
            axes = new Axe(20);


            DisplayHelp();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Indigo);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            //GL.ClearColor(Color.Blue);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);

            MouseState mouse = Mouse.GetState();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            float aspect_ratio = Width / (float)Height;
            perspective.aspect = aspect_ratio;

            Matrix4 perspect = perspective.GetPerspectiveFieldOfView();
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspect);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            ChangeTriangleColor(triangle, keyboard);

            if (keyboard[Key.O] && !lastKeyboard[Key.O])
                showAxis = !showAxis;

            if (keyboard[Key.Escape])
            {
                Exit();
            }

            camera.RotateMouse(mouse);

            lastKeyboard = keyboard;
            
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            
            Matrix4 lookAt = camera.GetLookAt();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookAt);

            if (showAxis)
                axes.DrawAxes();

            triangle.DrawTriangle3D();
            
            SwapBuffers();
        }

        private void DisplayHelp()
        {
            Console.WriteLine(" MENIU");
            Console.WriteLine(" O - Ascunde/Arata axele Oxyz");
            Console.WriteLine(" Schimbare culoare vertex (apasare trei taste simultan):");
            Console.WriteLine(" \tAlege vertex: 1-4");
            Console.WriteLine(" \tAlege canal culoare: R, G, B, A");
            Console.WriteLine(" \tAlege valoare: +, _");
            Console.WriteLine();
        }

        private void ChangeTriangleColor(Triangle triangle, KeyboardState keyboard)
        {
            int? value = null;
            CornersTriangle? vertex = null;
            ColorsFigure? color = null;

            if (keyboard[Key.Number1])
                vertex = CornersTriangle.vertex1;
            else if (keyboard[Key.Number2])
                vertex = CornersTriangle.vertex2;
            else if (keyboard[Key.Number3])
                vertex = CornersTriangle.vertex3;
            else if (keyboard[Key.Number4])
                vertex = CornersTriangle.all;

            if (keyboard[Key.R])
                color = ColorsFigure.red;
            else if (keyboard[Key.G])
                color = ColorsFigure.green;
            else if (keyboard[Key.B])
                color = ColorsFigure.blue;
            else if (keyboard[Key.A])
                color = ColorsFigure.alpha;

            if (keyboard[Key.Plus])
                value = 3;
            else if (keyboard[Key.Minus])
                value = -3;

            if (vertex.HasValue && value.HasValue && color.HasValue)
                triangle.ChangeVertexColor(vertex.Value, color.Value, value.Value);
        }

        //private void DrawSquare()
        //{

        //    GL.Begin(PrimitiveType.Lines);
        //    GL.Color3(Color.Red);
        //    GL.Vertex3(XYZ_SIZE, XYZ_SIZE, 0);
        //    GL.Vertex3(XYZ_SIZE, -XYZ_SIZE, 0);
        //    GL.End();

        //    GL.Begin(PrimitiveType.Lines);
        //    GL.Color3(Color.Green);
        //    GL.Vertex3(XYZ_SIZE, -XYZ_SIZE, 0);
        //    GL.Vertex3(-XYZ_SIZE, -XYZ_SIZE, 0);
        //    GL.End();

        //    GL.Begin(PrimitiveType.Lines);
        //    GL.Color3(Color.Yellow);
        //    GL.Vertex3(-XYZ_SIZE, -XYZ_SIZE, 0);
        //    GL.Vertex3(-XYZ_SIZE, XYZ_SIZE, 0);
        //    GL.End();

        //    GL.Begin(PrimitiveType.Lines);
        //    GL.Color3(Color.Orange);
        //    GL.Vertex3(-XYZ_SIZE, XYZ_SIZE, 0);
        //    GL.Vertex3(XYZ_SIZE, XYZ_SIZE, 0);
        //    GL.End();
        //}
    }
}
