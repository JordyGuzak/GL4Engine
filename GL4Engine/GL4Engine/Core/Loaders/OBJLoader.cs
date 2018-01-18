using System.IO;
using System.Collections.Generic;
using OpenTK;
using GL4Engine.Core;
using System.Globalization;

namespace GL4Engine.Graphics
{
    class OBJLoader
    {
        public string ResourcesFolder { get; private set; }

        public OBJLoader(string resourcesFolder)
        {
            ResourcesFolder = resourcesFolder;
        }

        public Mesh LoadMesh(string fileName, Loader loader)
        {
            string path = $@"{ResourcesFolder}Models\" + fileName;

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Unable to open \"" + path + "\", does not exist.");
            }

            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> textures = new List<Vector2>();
            List<Vector3> normals = new List<Vector3>();
            List<uint> indices = new List<uint>();

            float[] verticesArray = null;
            float[] normalsArray = null;
            float[] textureArray = null;
            uint[] indicesArray = null;

            string line;
            bool reachedFases = false;

            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    string[] currentLine = line.Split(' ');


                    switch (currentLine[0])
                    {
                        case "v":
                            Vector3 vertex = new Vector3(float.Parse(currentLine[1], CultureInfo.InvariantCulture), float.Parse(currentLine[2], CultureInfo.InvariantCulture), float.Parse(currentLine[3], CultureInfo.InvariantCulture));
                            vertices.Add(vertex);
                            break;

                        case "vt":
                            Vector2 textureCoords = new Vector2(float.Parse(currentLine[1], CultureInfo.InvariantCulture), float.Parse(currentLine[2], CultureInfo.InvariantCulture));
                            textures.Add(textureCoords);
                            break;

                        case "vn":
                            Vector3 normal = new Vector3(float.Parse(currentLine[1], CultureInfo.InvariantCulture), float.Parse(currentLine[2], CultureInfo.InvariantCulture), float.Parse(currentLine[3], CultureInfo.InvariantCulture));
                            normals.Add(normal);
                            break;

                        case "f":
                            if (!reachedFases)
                            {
                                textureArray = new float[vertices.Count * 2];
                                normalsArray = new float[vertices.Count * 3];
                                reachedFases = true;
                            }

                            string[] vertex1 = currentLine[1].Split('/');
                            string[] vertex2 = currentLine[2].Split('/');
                            string[] vertex3 = currentLine[3].Split('/');

                            ProcessVertex(vertex1, indices, textures, normals, textureArray, normalsArray);
                            ProcessVertex(vertex2, indices, textures, normals, textureArray, normalsArray);
                            ProcessVertex(vertex3, indices, textures, normals, textureArray, normalsArray);
                            break;

                        default:
                            break;
                    }
                }
            }

            verticesArray = new float[vertices.Count * 3];
            indicesArray = indices.ToArray();

            int vertexCounter = 0;
            foreach (Vector3 vertex in vertices)
            {
                verticesArray[vertexCounter++] = vertex.X;
                verticesArray[vertexCounter++] = vertex.Y;
                verticesArray[vertexCounter++] = vertex.Z;
            }

            return loader.LoadToVAO(verticesArray, indicesArray, textureArray, normalsArray);
        }

        private void ProcessVertex(string[] vertexData, List<uint> indices, List<Vector2> textures, List<Vector3> normals, float[] textureArray, float[] normalsArray)
        {
            int currentVertexPointer = int.Parse(vertexData[0]) - 1;
            indices.Add((uint)currentVertexPointer);

            // Texture coordinates
            Vector2 currentTexture = textures[int.Parse(vertexData[1]) - 1];
            textureArray[currentVertexPointer * 2] = currentTexture.X;
            textureArray[currentVertexPointer * 2 + 1] = 1 - currentTexture.Y;

            // Normal
            Vector3 currentNormal = normals[int.Parse(vertexData[2]) - 1];
            normalsArray[currentVertexPointer * 3] = currentNormal.X;
            normalsArray[currentVertexPointer * 3 + 1] = currentNormal.Y;
            normalsArray[currentVertexPointer * 3 + 2] = currentNormal.Z;
        }
    }
}
