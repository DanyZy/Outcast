using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class HexCellRenderer : MonoBehaviour
{
    private Mesh hexMesh;

    private List<Vector3> vertices;
    private List<int> triangles;
    private List<Vector2> uv;

    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        hexMesh.name = "Hex Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
        uv = new List<Vector2>();
    }

    private void Start()
    {
        Triangulate();

        GetComponent<MeshCollider>().sharedMesh = hexMesh;
    }

    private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }

    private void TextureSetup(Vector2 v1, Vector2 v2, Vector2 v3)
    {
        uv.Add(v1);
        uv.Add(v2);
        uv.Add(v3);
    }

    private void Triangulate()
    {
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                Vector3.zero,
                Vector3.zero + HexMetrics.corners[i],
                Vector3.zero + HexMetrics.corners[i + 1]
            );
            TextureSetup(
                Vector2.zero,
                Vector2.zero + HexMetrics.uvPoints[i],
                Vector2.zero + HexMetrics.uvPoints[i + 1]
            );
        }
        hexMesh.vertices = vertices.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.uv = uv.ToArray();

        hexMesh.RecalculateNormals();
    }
}
