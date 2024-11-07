
using OpenTK;
using System;
using System.IO;
using System.Xml.Serialization;
using Tema_5;

namespace Tema_5
{
    [Serializable]
    public class Perspective : SceneObject<Perspective>
    {
        public float fovy = DEFAULT_FOVY;
        public float aspect = DEFAULT_ASPECT;
        public float zNear = DEFAULT_Z_NEAR;
        public float zFar = DEFAULT_Z_FAR;

        public const float DEFAULT_FOVY = MathHelper.PiOver4;
        public const float DEFAULT_ASPECT = 1.33f;
        public const float DEFAULT_Z_NEAR = 1.0f;
        public const float DEFAULT_Z_FAR = 1024.0f;

        public Perspective() { }

        public Perspective(float fovy, float aspect, float zNear, float zFar)
        {
            this.fovy = fovy;
            this.aspect = aspect;
            this.zNear = zNear;
            this.zFar = zFar;
        }

        public Perspective(string fileName)
        {
            Perspective desearializedPerspective = DeserializeXml(fileName);
            fovy = desearializedPerspective.fovy;
            aspect = desearializedPerspective.aspect;
            zNear = desearializedPerspective.zNear;
            zFar = desearializedPerspective.zFar;
        }

        public Matrix4 GetPerspectiveFieldOfView()
        {
            return Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, zNear, zFar);
        }    

        public void SetDefault()
        {
            fovy = DEFAULT_FOVY;
            aspect = DEFAULT_ASPECT;
            zNear = DEFAULT_Z_NEAR;
            zFar = DEFAULT_Z_FAR;
        }
    }
}
