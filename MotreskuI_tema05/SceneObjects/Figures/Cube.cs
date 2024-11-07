using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using Tema_5.Enum;

namespace Tema_5.SceneObjects
{
    [Serializable]
    public class Cube : SceneObject<Cube>
    {
        // Vertex positions for the cube
        protected readonly List<Vector3> vertices = new List<Vector3>
        {
            // Front face
            new Vector3(-10f, 10f, 10f),   // top left
            new Vector3(10f, 10f, 10f),    // top right
            new Vector3(-10f, -10f, 10f),  // bottom left
            new Vector3(10f, -10f, 10f),   // bottom right

            // Back face
            new Vector3(10f, 10f, -10f),   // top right
            new Vector3(-10f, 10f, -10f),  // top left
            new Vector3(10f, -10f, -10f),  // bottom right
            new Vector3(-10f, -10f, -10f), // bottom left

            // Left face
            new Vector3(-10f, 10f, -10f),  // top left (back)
            new Vector3(-10f, 10f, 10f),   // top left (front)
            new Vector3(-10f, -10f, -10f), // bottom left (back)
            new Vector3(-10f, -10f, 10f),  // bottom left (front)

            // Right face
            new Vector3(10f, 10f, 10f),    // top right (front)
            new Vector3(10f, 10f, -10f),   // top right (back)
            new Vector3(10f, -10f, 10f),   // bottom right (front)
            new Vector3(10f, -10f, -10f),  // bottom right (back)

            // Top face
            new Vector3(-10f, 10f, -10f),  // top left (back)
            new Vector3(10f, 10f, -10f),   // top right (back)
            new Vector3(-10f, 10f, 10f),   // top left (front)
            new Vector3(10f, 10f, 10f),    // top right (front)

            // Bottom face
            new Vector3(-10f, -10f, 10f),  // bottom left (front)
            new Vector3(10f, -10f, 10f),   // bottom right (front)
            new Vector3(-10f, -10f, -10f), // bottom left (back)
            new Vector3(10f, -10f, -10f),  // bottom right (back)
        };

        private Color[] faceColors = new Color[]
        {
            Color.Red, Color.Green, Color.Blue, Color.Yellow,
            Color.Cyan, Color.Magenta
        };

        private float rotationAngle = 0.0f;
        private readonly Randomizer randomizer = new Randomizer();

        public Cube() { }

        public Cube(string fileName)
        {
            Cube deserializedCube = DeserializeXml(fileName);
            vertices = deserializedCube.vertices;
        }

        public void DrawCube()
        {

            int faceVertexCount = 4;
            for (int face = 0; face < 6; face++)
            {
                GL.Begin(PrimitiveType.TriangleStrip);
                GL.Color4(faceColors[face]);

                for (int i = 0; i < faceVertexCount; i++)
                {
                    GL.Vertex3(vertices[face * faceVertexCount + i]);
                }

                GL.End();
            }
        }

        public void RoatateAndDrawCube()
        {
            GL.PushMatrix();
            GL.Rotate(rotationAngle, 0.0f, 1.0f, 0.0f);
            GL.Rotate(rotationAngle, 1.0f, 0.0f, 0.0f);

            int faceVertexCount = 4;
            for (int face = 0; face < 6; face++)
            {
                GL.Begin(PrimitiveType.TriangleStrip);
                GL.Color4(faceColors[face]);

                for (int i = 0; i < faceVertexCount; i++)
                {
                    GL.Vertex3(vertices[face * faceVertexCount + i]);
                }

                GL.End();
            }

            GL.PopMatrix();
        }

        public void UpdateRotation(float deltaTime)
        {
            rotationAngle += 45.0f * deltaTime;
            if (rotationAngle > 360.0f)
                rotationAngle -= 360.0f;
        }

        public void SetFaceColor(CubeFaces face, Color color)
        {
            if (face == CubeFaces.All)
            {
                for (int i = 0; i < faceColors.Length; i++)
                {
                    faceColors[i] = color;
                }
            }
            else
            {
                faceColors[(int)face] = color;
            }
        }

        public void ChangeFaceColor(CubeFaces face, ColorsFigure toChange, int value)
        {
            if (face == CubeFaces.All)
            {
                for (int i = 0; i < faceColors.Length; i++)
                {
                    faceColors[i] = ChangeColor(faceColors[i], toChange, value);
                }
            }
            else
            {
                faceColors[(int)face] = ChangeColor(faceColors[(int)face], toChange, value);
            }
        }

        private Color ChangeColor(Color color, ColorsFigure toChange, int value)
        {
            int red = color.R, green = color.G, blue = color.B, alpha = color.A;

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

        public void RandomizeColors()
        {
            for (int i = 0; i < faceColors.Length; i++)
            {
                faceColors[i] = randomizer.GenerateRandomColor();
            }
        }

    }
}
