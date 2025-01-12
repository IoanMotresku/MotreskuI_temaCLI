using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace OpenTK_winforms_z02.SceneObjects.Figures
{
    public class Axes
    {
        public void Draw()
        {
            GL.Begin(PrimitiveType.Lines);

            // Ось X (красная)
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(100, 0, 0);

            // Ось Y (желтая)
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 100, 0);

            // Ось Z (зеленая)
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 100);

            GL.End();
        }
    }

}
