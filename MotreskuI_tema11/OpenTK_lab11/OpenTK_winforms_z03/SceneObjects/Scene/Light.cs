using OpenTK.Graphics.OpenGL;
using System;

namespace OpenTK_winforms_z03.SceneObjects.Scene
{
    public class Light
    {
        public bool IsEnabled { get; private set; }
        public float[] Ambient { get; private set; }
        public float[] Diffuse { get; private set; }
        public float[] Specular { get; private set; }
        public float[] Position { get; private set; }

        public Light(float[] ambient, float[] diffuse, float[] specular, float[] position)
        {
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Position = position;
            IsEnabled = true;
        }

        public void Enable()
        {
            IsEnabled = true;
        }

        public void Disable()
        {
            IsEnabled = false;
        }

        public void Toggle()
        {
            IsEnabled = !IsEnabled;
        }

        public void SetPosition(float x, float y, float z)
        {
            Position[0] = x;
            Position[1] = y;
            Position[2] = z;
        }

        public void ApplyLight(LightName lightName)
        {
            if (IsEnabled)
            {
                GL.Enable(EnableCap.Lighting);
                GL.Enable((EnableCap)lightName);
                GL.Light(lightName, LightParameter.Ambient, Ambient);
                GL.Light(lightName, LightParameter.Diffuse, Diffuse);
                GL.Light(lightName, LightParameter.Specular, Specular);
                GL.Light(lightName, LightParameter.Position, Position);
            }
            else
            {
                GL.Disable((EnableCap)lightName);
            }
        }
        public void DrawLightSourceMarker()
        {
            GL.PushMatrix();
            GL.Disable(EnableCap.Lighting); // Выключаем освещение, чтобы маркер был всегда видим
            GL.Color3(1.0f, 1.0f, 0.0f); // Задаем цвет маркера (желтый)

            // Перемещаемся в позицию источника света
            GL.Translate(Position[0], Position[1], Position[2]);

            // Рисуем небольшой шарик
            DrawSphere(2f, 16, 16);

            GL.Enable(EnableCap.Lighting); // Включаем освещение обратно
            GL.PopMatrix();
        }

        private void DrawSphere(float radius, int slices, int stacks)
        {
            for (int i = 0; i < stacks; i++)
            {
                double latitude1 = Math.PI * (-0.5 + (double)(i) / stacks);
                double latitude2 = Math.PI * (-0.5 + (double)(i + 1) / stacks);

                double sinLat1 = Math.Sin(latitude1);
                double cosLat1 = Math.Cos(latitude1);
                double sinLat2 = Math.Sin(latitude2);
                double cosLat2 = Math.Cos(latitude2);

                GL.Begin(PrimitiveType.QuadStrip);
                for (int j = 0; j <= slices; j++)
                {
                    double longitude = 2 * Math.PI * (double)(j) / slices;
                    double sinLon = Math.Sin(longitude);
                    double cosLon = Math.Cos(longitude);

                    double x1 = cosLon * cosLat1;
                    double y1 = sinLon * cosLat1;
                    double z1 = sinLat1;

                    double x2 = cosLon * cosLat2;
                    double y2 = sinLon * cosLat2;
                    double z2 = sinLat2;

                    GL.Normal3(x1, y1, z1);
                    GL.Vertex3(radius * x1, radius * y1, radius * z1);

                    GL.Normal3(x2, y2, z2);
                    GL.Vertex3(radius * x2, radius * y2, radius * z2);
                }
                GL.End();
            }
        }

    }
}
