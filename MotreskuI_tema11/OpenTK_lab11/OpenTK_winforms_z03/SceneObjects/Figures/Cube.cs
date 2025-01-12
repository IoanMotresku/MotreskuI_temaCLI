using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenTK_winforms_z02.Enum;
using OpenTK.Graphics;

namespace OpenTK_winforms_z02.SceneObjects
{
    [Serializable]
    public class Cube : SceneObject<Cube>
    {
        // Vertex positions for the cube
        public Vector3[] vertices = new Vector3[]
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
        private bool falling = false;
        private bool rotate = false;
        private bool statusTransparent = false;

        private float transparency = 0.5f;
        private float radius = 5.0f;
        private float angle = 0.0f;

        //private float speed = 1.0f; // Rotație în radiani pe secundă

        private Vector3 position = new Vector3(0, 0, 0);
        private Vector3 initialPosition = new Vector3(0, 0, 0);

        public float speed = 20.0f;
        private Vector3 offset = new Vector3(0, 0, 0);

        public Cube(Vector3 startPosition, float radius, float speed)
        {
            this.initialPosition = startPosition;
            this.position = startPosition;
            this.radius = radius;
            this.speed = speed;
        }

        //public Cube()
        //{}

        public Cube(string fileName)
        {
            try
            {
                Console.WriteLine("Test1");
                Cube deserializedCube = DeserializeXml(fileName);
                //Console.WriteLine(deserializedCube.vertices[1]);
                Console.WriteLine(deserializedCube.speed);
                vertices = deserializedCube.vertices;
                speed = deserializedCube.speed;
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void toogleRotate()
        {
            rotate = !rotate;
        }

        public void setRotate(bool status)
        {
            rotate = status;
        }

        //public void Update(float deltaTime)
        //{
        //    // Actualizează unghiul
        //    angle += speed * deltaTime;
        //    if (angle >= 2 * Math.PI)
        //        angle -= 2 * (float)Math.PI;

        //    // Calculează noile coordonate
        //    float x = initialPosition.X + radius * (float)Math.Cos(angle);
        //    float z = initialPosition.Z + radius * (float)Math.Sin(angle);

        //    // Actualizează poziția
        //    position = new Vector3(x, position.Y, z);
        //}

        public void addOffset(Vector3 offset)
        {
            this.offset += offset;
        }

        public void DrawCube()
        {

            int faceVertexCount = 4;
            
           
            if (!falling)
                DrawNormal();
            else 
                DrawFalling();
        }
        public void Update(float deltaTime)
        {
            // Actualizează unghiul
            angle += speed * deltaTime;
            if (angle >= 2 * Math.PI)
                angle -= 2 * (float)Math.PI;

            // Calculează noile coordonate
            float x = initialPosition.X + radius * (float)Math.Cos(angle);
            float z = initialPosition.Z + radius * (float)Math.Sin(angle);

            // Actualizează poziția
            position = new Vector3(x, position.Y, z);
        }

        private void DrawNormal()
        {
            GL.PushMatrix();
            GL.Translate(position);
            if(statusTransparent == true)
            {
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.One);
            }

            int faceVertexCount = 4;
            for (int face = 0; face < 6; face++)
            {
                Color4 curentColor = faceColors[face];

                if(statusTransparent == true)
                    curentColor = new Color4(curentColor.R, curentColor.G, curentColor.B, 1);

                GL.Begin(PrimitiveType.TriangleStrip);
                GL.Color4(curentColor);

                for (int i = 0; i < faceVertexCount; i++)
                {
                    GL.Vertex3(vertices[face * faceVertexCount + i]);
                }

                GL.End();
            }

            if (statusTransparent == true)
            {
                //GL.BlendFunc(BlendingFactorSrc.Src1Alpha, BlendingFactorDest.One);
                GL.Disable(EnableCap.Blend);
            }

            GL.PopMatrix();
        }

        public void toggleTransparency()
        {
            statusTransparent = !statusTransparent;
        }

        private void DrawFalling()
        {
            Vector3[] transformedVertices = vertices
                .Select(vertex => vertex + offset) 
                .ToArray();

            int faceVertexCount = 4;
            for (int face = 0; face < 6; face++)
            {
                GL.Begin(PrimitiveType.TriangleStrip);
                GL.Color4(faceColors[face]);

                for (int i = 0; i < faceVertexCount; i++)
                {
                    GL.Vertex3(transformedVertices[face * faceVertexCount + i]);
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
        public void StartFalling()
        {
            falling = true;
            Random random = new Random();
            offset[1] = random.Next(20, 30);

            offset[0] = random.Next(0, 20);
            offset[2] = random.Next(0, 20);

        }

        public void StopFalling()
        {
            falling = false;
            offset[0] = 0;
            offset[1] = 0;
            offset[2] = 0;

        }

        public void UpdateFalling(float deltaTime)
        {
            if (!falling)
                return;
            //if (offset[1] <= 0.0f)
            //    falling = false;
            else if (offset[1] - speed * deltaTime > 0.0f)
                offset[1] -= speed* deltaTime;
            else
            {
                offset[1] = 0.0f;
            }
         
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
