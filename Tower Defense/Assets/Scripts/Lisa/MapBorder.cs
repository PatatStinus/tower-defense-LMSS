using System.Collections.Generic;
using UnityEngine;

public class MapBorder : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private List<Vector3> vertices;
    public static List<Vector3> VerticesPositions { get; private set; }

    public enum Corners
    {
        topRight,
        bottomRight,
        bottomLeft,
        topLeft
    }

    void Start()
    {
        mesh = this.gameObject.GetComponent<MeshFilter>().mesh;

        // Loop through all the vertices in the mesh
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            // Add the new scaled vertices to the list after multiplying them with the scale
            vertices.Add(new Vector3(
                mesh.vertices[i].x * transform.localScale.x, 
                mesh.vertices[i].y, 
                mesh.vertices[i].z * transform.localScale.z) + transform.position);
        }

        // Give the vertices list to the verticies Positions 
        VerticesPositions = vertices;
    }

    void Update()
    {
        mesh.RecalculateBounds();

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            // Add the new scaled vertices to the list after multiplying them with the scale
            vertices[i] = new Vector3(
                mesh.vertices[i].x * transform.localScale.x,
                mesh.vertices[i].y,
                mesh.vertices[i].z * transform.localScale.z)
                + transform.position;

            Debug.DrawLine(vertices[i], vertices[i] + Vector3.up * 30);
        }
    }
}
