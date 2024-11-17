
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using Tema_6.Enum;

namespace Tema_6
{
    [Serializable]
    public class Triangle : SceneObject<Triangle>
    {
        public Vector3d vertex1;
        public Vector3d vertex2;
        public Vector3d vertex3;

        Color vertex1_color = Color.Red, 
            vertex2_color = Color.Green, 
            vertex3_color = Color.Blue;

        public Triangle() {}

        public Triangle(string fileName)
        {
            Triangle deserializedTriangle = DeserializeXml(fileName);
            vertex1 = deserializedTriangle.vertex1;
            vertex2 = deserializedTriangle.vertex2;
            vertex3 = deserializedTriangle.vertex3;
        }

        public void DrawTriangle3D()
        {
            GL.Begin(PrimitiveType.Triangles);

            GL.Color4(vertex1_color);
            GL.Vertex3(vertex1);
            GL.Color4(vertex2_color);
            GL.Vertex3(vertex2);
            GL.Color4(vertex3_color);
            GL.Vertex3(vertex3);

            GL.End();
        }

        public void SetVertexColor(CornersTriangle vertex, Color culoare)
        {
            switch (vertex)
            {
                case CornersTriangle.Vertex1:
                    vertex1_color = culoare;
                    break;
                case CornersTriangle.Vertex2:
                    vertex2_color = culoare;
                    break;
                case CornersTriangle.Vertex3:
                    vertex3_color = culoare;
                    break;
                case CornersTriangle.All:
                    vertex1_color = culoare;
                    vertex2_color = culoare;
                    vertex3_color = culoare;
                    break;
            }
        }

        public void ChangeVertexColor(CornersTriangle vertex, ColorsFigure culoare, int value)
        {
            switch (vertex)
            {
                case CornersTriangle.Vertex1:
                    vertex1_color = ChangeColor(vertex1_color, culoare, value);
                    break;
                case CornersTriangle.Vertex2:
                    vertex2_color = ChangeColor(vertex2_color, culoare, value);
                    break;
                case CornersTriangle.Vertex3:
                    vertex3_color = ChangeColor(vertex3_color, culoare, value);
                    break;
                case CornersTriangle.All:
                    vertex1_color = ChangeColor(vertex1_color, culoare, value);
                    vertex2_color = ChangeColor(vertex2_color, culoare, value);
                    vertex3_color = ChangeColor(vertex3_color, culoare, value);
                    break;
            }
        }

        private Color ChangeColor(Color color, ColorsFigure toChange, int value)
        {
            int red = color.R, green = color.G, blue = color.B, alpha = color.A;
            Console.WriteLine("Red:   "+ red + "\tGreen: " + green + "\tBlue:  " + blue + "\tAlpha: " + alpha);
            switch (toChange)
            {
                case ColorsFigure.red:
                    red = Math.Max(0, Math.Min(color.R + value, 255));
                    break;
                case ColorsFigure.green:
                    green = Math.Max(0, Math.Min(color.G + value, 255));
                    break;
                case ColorsFigure.blue:
                    blue = Math.Max(0, Math.Min(color.B + value, 255));
                    break;
                case ColorsFigure.alpha:
                    alpha = Math.Max(0, Math.Min(color.A + value, 255));
                    break;
            }

            return Color.FromArgb(alpha, red, green, blue);
        }

        public void SetDefault()
        {

        }
    }
}
