using OpenTK;
using System;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;
using System.Configuration;
using System.Threading;
using Tema_5.SceneObjects;
using Tema_5.Enum;
//using static OpenTK.Graphics.OpenGL.GL;

namespace Tema_5
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
        private Cube Cube;
        private readonly string cubeConfig;

        private Randomizer randomizer;

        private KeyboardState lastKeyboard;
        private bool rotate = false;
        private bool showAxis = false;
        private bool showCube = true;


        private Color DEFAULT_BACK_COLOR = Color.Indigo;


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
            cubeConfig = ConfigurationManager.AppSettings["cubeConfig"];

            camera = new Camera(cameraConfig);
            perspective = new Perspective(perspectiveConfig);

            randomizer = new Randomizer();

            triangle = new Triangle(triangleConfig);
            axes = new Axe(30);
            Cube = new Cube(cubeConfig);



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

            float deltaTime = (float)e.Time; // Получаем время, прошедшее с последнего кадра
            Cube.UpdateRotation(deltaTime);


            if(showCube)
                ChangeCubeColor(Cube, keyboard);
            else
                ChangeTriangleColor(triangle, keyboard);
            

            if (keyboard[Key.C] && !lastKeyboard[Key.C])
                Cube.RandomizeColors();

            if (keyboard[Key.S] && !lastKeyboard[Key.S])
                rotate = !rotate;

            if (keyboard[Key.O] && !lastKeyboard[Key.O])
                showAxis = !showAxis;

            if (keyboard[Key.H] && !lastKeyboard[Key.H])
                showCube = !showCube;

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

            if(!showCube)
                triangle.DrawTriangle3D();

            if(showCube)
            {
                if (rotate)
                    Cube.RoatateAndDrawCube();
                else
                    Cube.DrawCube();
            }
            

            if (showAxis)
                axes.DrawAxes();

            SwapBuffers();
        }

        private void DisplayHelp()
        {
            Console.WriteLine(" MENIU");
            Console.WriteLine(" C - Schimbare culoare fete in mod aleatoriu");
            Console.WriteLine(" S - Incepe/Opreste rotire");
            Console.WriteLine(" O - Ascunde/Arata axele Oxyz");
            Console.WriteLine(" H - Interschimba cubul si triunghiul");
            Console.WriteLine(" Schimbare culoare fata (apasare trei taste simultan):");
            Console.WriteLine(" \tAlege fata: 1-6 (triunghi 1-4)");
            Console.WriteLine(" \tAlege canal culoare: R, G, B, A: A");
            Console.WriteLine(" \tAlege valoare: +, _");
            Console.WriteLine();
        }

        private void ChangeTriangleColor(Triangle triangle, KeyboardState keyboard)
        {
            int? value = null;
            CornersTriangle? vertex = null;
            ColorsFigure? color = null;

            if (keyboard[Key.Number1])
                vertex = CornersTriangle.Vertex1;
            else if (keyboard[Key.Number2])
                vertex = CornersTriangle.Vertex2;
            else if (keyboard[Key.Number3])
                vertex = CornersTriangle.Vertex3;
            else if (keyboard[Key.Number4])
                vertex = CornersTriangle.All;

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

        private void ChangeCubeColor(Cube cube, KeyboardState keyboard)
        {
            int? value = null;
            CubeFaces? face = null;
            ColorsFigure? color = null;

            if (keyboard[Key.Number1])
                face = CubeFaces.Front;
            else if (keyboard[Key.Number2])
                face = CubeFaces.Back;
            else if (keyboard[Key.Number3])
                face = CubeFaces.Left;
            else if (keyboard[Key.Number4])
                face = CubeFaces.Right;
            else if (keyboard[Key.Number5])
                face = CubeFaces.Top;
            else if (keyboard[Key.Number6])
                face = CubeFaces.Bottom;
            else if (keyboard[Key.Number7])
                face = CubeFaces.All;

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

            if (face.HasValue && value.HasValue && color.HasValue)
            {
                cube.ChangeFaceColor(face.Value, color.Value, value.Value);
            }
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
