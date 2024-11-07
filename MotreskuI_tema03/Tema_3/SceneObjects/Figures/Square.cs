
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using Tema_3.Enum;

namespace Tema_3
{
    [Serializable]
    internal class Square : SceneObject<Square>
    {
        public Vector3d vertex1;
        public Vector3d vertex2;
        public Vector3d vertex3;
        public Vector3d vertex4;

        Color vertex1_color = Color.Red,
            vertex2_color = Color.Green,
            vertex3_color = Color.Blue,
            vertex4_color = Color.Yellow;

        public Square() { }

        public Square(string fileName)
        {
            Square deserializedTriangle = DeserializeXml(fileName);
            vertex1 = deserializedTriangle.vertex1;
            vertex2 = deserializedTriangle.vertex2;
            vertex3 = deserializedTriangle.vertex3;
            vertex4 = deserializedTriangle.vertex4;
        }

        public void DrawSquare3D()
        {
            GL.Begin(PrimitiveType.TriangleStrip);

            GL.Color3(vertex1_color);
            GL.Vertex3(vertex1);
            GL.Color3(vertex2_color);
            GL.Vertex3(vertex2);
            GL.Color3(vertex3_color);
            GL.Vertex3(vertex3);
            GL.Color3(vertex4_color);
            GL.Vertex3(vertex4);

            GL.End();
        }

        public void SetVertexColor(CornersSquare vertex, Color culoare)
        {
            switch (vertex)
            {
                case CornersSquare.vertex1:
                    vertex1_color = culoare;
                    break;
                case CornersSquare.vertex2:
                    vertex2_color = culoare;
                    break;
                case CornersSquare.vertex3:
                    vertex3_color = culoare;
                    break;
                case CornersSquare.vertex4:
                    vertex4_color = culoare;
                    break;
                case CornersSquare.all:
                    vertex1_color = culoare;
                    vertex2_color = culoare;
                    vertex3_color = culoare;
                    vertex4_color = culoare;
                    break;
            }
        }

        public void ChangeVertexColor(CornersSquare vertex, ColorsFigure culoare, int value)
        {
            switch (vertex)
            {
                case CornersSquare.vertex1:
                    vertex1_color = ChangeColor(vertex1_color, culoare, value);
                    break;
                case CornersSquare.vertex2:
                    vertex2_color = ChangeColor(vertex2_color, culoare, value);
                    break;
                case CornersSquare.vertex3:
                    vertex3_color = ChangeColor(vertex3_color, culoare, value);
                    break;
                case CornersSquare.vertex4:
                    vertex4_color = ChangeColor(vertex4_color, culoare, value);
                    break;
                case CornersSquare.all:
                    vertex1_color = ChangeColor(vertex1_color, culoare, value);
                    vertex2_color = ChangeColor(vertex2_color, culoare, value);
                    vertex3_color = ChangeColor(vertex3_color, culoare, value);
                    vertex4_color = ChangeColor(vertex4_color, culoare, value);
                    break;
            }
        }

        private Color ChangeColor(Color color, ColorsFigure toChange, int value)
        {
            int red = color.R, green = color.G, blue = color.B, alpha = color.A;
            Console.WriteLine("Red:   " + red + "\tGreen: " + green + "\tBlue:  " + blue + "\tAlpha: " + alpha);
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
