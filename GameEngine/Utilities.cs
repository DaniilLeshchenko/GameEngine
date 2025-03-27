using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class Utilities
{
    public static GameTime GameTime { get; set; }
    public static ContentManager Content { get; set; }
    public static GraphicsDevice GraphicsDevice { get; set; }

    public static BoundingBox CreateBoundingBoxFromModel(Model model)
    {
        List<Vector3> vertices = new List<Vector3>();

        foreach (ModelMesh mesh in model.Meshes)
        {
            foreach (ModelMeshPart part in mesh.MeshParts)
            {
                VertexBuffer vertexBuffer = part.VertexBuffer;
                int vertexCount = vertexBuffer.VertexCount;
                VertexPosition[] vertexData = new VertexPosition[vertexCount];

                vertexBuffer.GetData(vertexData);

                foreach (VertexPosition vertex in vertexData)
                {
                    Vector3 transformedPosition = Vector3.Transform(
                        vertex.Position,
                        mesh.ParentBone.Transform);

                    vertices.Add(transformedPosition);
                }
            }
        }

        return BoundingBox.CreateFromPoints(vertices);
    }
}

