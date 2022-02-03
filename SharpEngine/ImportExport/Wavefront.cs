using System.Collections.Generic;
using System.IO;
using SharpEngine.Engine;
using SharpEngine.Objects;

namespace SharpEngine.ImportExport
{
    class Wavefront
    {
        public static Object3D ImportFromFile(Core core, Vector position, Texture texture, string file)
        {
            string[] lines = File.ReadAllLines(file);

            List<Vector> vertices = new List<Vector>();
            List<Triangle> triangles = new List<Triangle>();
            List<(float, float)> uv = new List<(float, float)>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(' ');
                if (data.Length >= 4  && data[0] == "v")
                {
                    float x = float.Parse(data[1]);
                    float y = float.Parse(data[2]);
                    float z = float.Parse(data[3]);

                    vertices.Add(new Vector(x, y, z));
                }
                else if (data.Length >= 3 && data[0] == "vt")
                {
                    float u = float.Parse(data[1]);
                    float v = float.Parse(data[2]);
                    if (u > 1)
                        u -= 1;
                    if (v > 1)
                        v -= 1;
                    if (u < 0)
                        u += 1;
                    if (v < 0)
                        v += 1;
                    uv.Add((u, v));
                }
                else if (data.Length >= 4 && data[0] == "f")
                {
                    string[] p1 = data[1].Split('/');
                    string[] p2 = data[2].Split('/');
                    string[] p3 = data[3].Split('/');

                    triangles.Add(new Triangle(core, position,
                        vertices[int.Parse(p1[0]) - 1].UV(uv[int.Parse(p1[1]) - 1]),
                        vertices[int.Parse(p2[0]) - 1].UV(uv[int.Parse(p2[1]) - 1]),
                        vertices[int.Parse(p3[0]) - 1].UV(uv[int.Parse(p3[1]) - 1]),
                        texture
                        ));
                }
            }

            Object3D obj = new Object3D(core, position);
            obj.Vertices = vertices.ToArray();
            obj.Triangles = triangles.ToArray();
            obj.Rotate(Vector.Zero);

            return obj;
        }
    }
}
