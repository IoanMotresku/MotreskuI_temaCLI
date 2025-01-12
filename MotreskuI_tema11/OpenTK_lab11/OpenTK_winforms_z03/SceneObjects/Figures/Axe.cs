
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK_winforms_z02.Enum;

namespace OpenTK_winforms_z02
{
    internal class Axe : SceneObject<Axe>
    {
        float line_length = 100.0f, line_width = 100.0f;
        Color x_color = Color.Red, y_color = Color.Green, z_color = Color.Blue; 
        public Axe(int line_length)
        { 
            this.line_length = line_length;
        }

        public Axe(string fileName)
        {
            Axe desearializedCamera = DeserializeXml(fileName);
            line_length = desearializedCamera.line_length;
            line_width = desearializedCamera.line_width;
            x_color = desearializedCamera.x_color;
            y_color = desearializedCamera.y_color;
            z_color = desearializedCamera.z_color;
        }


        public void DrawAxes()
        {
            GL.LineWidth(line_width);

            // Desenează axa Ox 
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(x_color);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(line_length, 0, 0);


            // Desenează axa Oy 

            GL.Color3(y_color);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, line_length, 0); ;


            // Desenează axa Oz
            GL.Color3(z_color);
            GL.Vertex3(0, 0, 0);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, line_length);
            GL.End();
        }

        public void setAxeColor(Axis axa, Color culoare)
        {
            switch (axa)
            {
                case Axis.X:
                    x_color = culoare;
                    break;
                case Axis.Y: 
                    y_color = culoare;
                    break;
                case Axis.Z:
                    z_color = culoare;
                    break;
                case Axis.all:
                    x_color = culoare;
                    y_color = culoare;
                    z_color = culoare;  
                    break;
            }
        }

        public void setAxeWidth(float width)
        {
            if (width < 0)
                this.line_width = 0;
            else if (width > 255)
                this.line_width = 255;
            else
                this.line_width = width;
        }

        public void setAxeLength(float length)
        {
            if (length < 0)
                this.line_length = 0;
            else
                this.line_length = length;
        }
    }
}
