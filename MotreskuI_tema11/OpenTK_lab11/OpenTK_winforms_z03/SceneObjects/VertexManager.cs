using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTK_winforms_z02.SceneObjects.Scene
{
    public class VertexManager
    {
        public int[,] Vertices { get; private set; }
        public int[] QuadsList { get; private set; }
        public int[] TrianglesList { get; private set; }

        public int VertexCount { get; private set; }
        public int QuadsCount { get; private set; }
        public int TrianglesCount { get; private set; }

        public VertexManager()
        {
            Vertices = new int[50, 3];
            QuadsList = new int[100];
            TrianglesList = new int[100];
        }

        public void LoadVertices(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    VertexCount = int.Parse(reader.ReadLine().Trim());
                    for (int i = 0; i < VertexCount; i++)
                    {
                        string[] parts = reader.ReadLine().Trim().Split(' ');
                        Vertices[i, 0] = int.Parse(parts[0]);
                        Vertices[i, 1] = int.Parse(parts[1]);
                        Vertices[i, 2] = int.Parse(parts[2]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load vertices from {filePath}: {ex.Message}");
            }
        }

        public void LoadQuads(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    QuadsCount = 0;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        QuadsList[QuadsCount++] = int.Parse(line.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load quads from {filePath}: {ex.Message}");
            }
        }

        public void LoadTriangles(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    TrianglesCount = 0;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        TrianglesList[TrianglesCount++] = int.Parse(line.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load triangles from {filePath}: {ex.Message}");
            }
        }
    }


}
