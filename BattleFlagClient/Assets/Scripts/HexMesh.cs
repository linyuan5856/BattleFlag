using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    private Mesh hexMesh;
    private MeshCollider collider;
    private List<Vector3> vertices;
    private List<int> triangles;


    void Start()
    {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        collider = gameObject.AddComponent<MeshCollider>();
        vertices = new List<Vector3>();
        triangles = new List<int>();
    }

    public void Triangulate(HexCell[] cells)
    {
        hexMesh.Clear();
        vertices.Clear();
        triangles.Clear();

        foreach (var cell in cells)
            Triangulate(cell);

        hexMesh.vertices = vertices.ToArray();
        hexMesh.triangles = triangles.ToArray();
        collider.sharedMesh = hexMesh;
    }

    void Triangulate(HexCell cell)
    {
        var center = cell.transform.localPosition;
        for (int i = 0; i < 6; i++)
            AddTriangle(center, center + HexMetrics.Corners[i], center + HexMetrics.Corners[i + 1]);
    }

    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        var index = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);


        triangles.Add(index);
        triangles.Add(index + 1);
        triangles.Add(index + 2);
    }
}