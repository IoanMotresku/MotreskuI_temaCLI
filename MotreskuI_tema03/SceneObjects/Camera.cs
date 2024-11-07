using OpenTK;
using System;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Xml.Serialization;
using OpenTK.Input;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;

namespace Tema_3
{
    [Serializable]
    public class Camera : SceneObject<Camera>
    {
        public float eyeX = 30.0f;
        public float eyeY = 30.0f;
        public float eyeZ = 30.0f;

        public float targetX = 0;
        public float targetY = 0;
        public float targetZ = 0;

        public float upX = 0.0f;
        public float upY = 1.0f;
        public float upZ = 0.0f;

        protected float yaw = 0;
        protected float pitch = 0;

        protected float mouseSensitivity = 0.005f;

        protected int lastMouseX;
        protected int lastMouseY;
        protected bool firstMove = true;

        public Camera() 
        {
            InitializeDirection();
        }

        public Camera(string fileName)
        {
            Camera desearializedCamera = DeserializeXml(fileName);
            eyeX = desearializedCamera.eyeX;
            eyeY = desearializedCamera.eyeY;
            eyeZ = desearializedCamera.eyeZ;
            targetX = desearializedCamera.targetX;
            targetY = desearializedCamera.targetY;
            targetZ = desearializedCamera.targetZ;
            upX = desearializedCamera.upX;
            upY = desearializedCamera.upY;
            upZ = desearializedCamera.upZ;

            InitializeDirection();
        }

        public Camera(float eyeX, float eyeY, float eyeZ, float targetX, float targetY, float targetZ, float upX, float upY, float upZ)
        {
            this.eyeX = eyeX;
            this.eyeY = eyeY;
            this.eyeZ = eyeZ;
            this.targetX = targetX;
            this.targetY = targetY;
            this.targetZ = targetZ;
            this.upX = upX;
            this.upY = upY;
            this.upZ = upZ;

            InitializeDirection();
        }

        public Matrix4 GetLookAt()
        {
            return Matrix4.LookAt(eyeX, eyeY, eyeZ, targetX, targetY, targetZ, upX, upY, upZ);
        }

        public void Rotate(float yawOffset, float pitchOffset)
        {
            yaw += yawOffset;
            pitch += pitchOffset;

            pitch = Clamp(pitch, -89f, 89f);

            float yawRad = MathHelper.DegreesToRadians(yaw);
            float pitchRad = MathHelper.DegreesToRadians(pitch);

            float cosPitch = (float)Math.Cos(pitchRad);
            float sinPitch = (float)Math.Sin(pitchRad);
            float cosYaw = (float)Math.Cos(yawRad);
            float sinYaw = (float)Math.Sin(yawRad);

            targetX = eyeX + cosPitch * cosYaw;
            targetY = eyeY + sinPitch;
            targetZ = eyeZ + cosPitch * sinYaw;
        }

        public async void RotateMouse(MouseState mouse)
        {

            int mouseX = mouse.X;
            int mouseY = mouse.Y;

            if (firstMove && mouseX != 0 && mouseY != 0)
            {
                //Thread.Sleep(1000);
                lastMouseX = mouseX;
                lastMouseY = mouseY;
                //Console.WriteLine(lastMouseX + ": " + lastMouseX);
                firstMove = false;
                return;
            }
         
            //if (lastMouseX == mouseX || lastMouseY) {
            int deltaX = mouseX - lastMouseX;
            int deltaY = mouseY - lastMouseY;
            //Console.WriteLine(deltaX + ": " + deltaY);

            //if (deltaX != 0 || deltaY != 0)
            //{
            //    Console.WriteLine(deltaX + ", " + deltaY);
            //}

            lastMouseX = mouseX;
            lastMouseY = mouseY;
            //Console.WriteLine(lastMouseX + ", " + lastMouseX);

            float yawOffset = deltaX * mouseSensitivity;
            float pitchOffset = -deltaY * mouseSensitivity;

            yaw += yawOffset;
            pitch += pitchOffset;

            //Console.WriteLine(yaw + ": " + pitch);

            pitch = Clamp(pitch, -89f, 89f);

            float yawRad = MathHelper.DegreesToRadians(yaw);
            float pitchRad = MathHelper.DegreesToRadians(pitch);

            float cosPitch = (float)Math.Cos(pitchRad);
            float sinPitch = (float)Math.Sin(pitchRad);
            float cosYaw = (float)Math.Cos(yawRad);
            float sinYaw = (float)Math.Sin(yawRad);

            targetX = eyeX + cosPitch * cosYaw;
            targetY = eyeY + sinPitch;
            targetZ = eyeZ + cosPitch * sinYaw;
        }


        private float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
        private void InitializeDirection()
        {
            Vector3 direction = new Vector3(targetX - eyeX, targetY - eyeY, targetZ - eyeZ);
            yaw = MathHelper.RadiansToDegrees((float)Math.Atan2(direction.Z, direction.X));
            pitch = MathHelper.RadiansToDegrees((float)Math.Asin(direction.Y / direction.Length));
        }
    }
}