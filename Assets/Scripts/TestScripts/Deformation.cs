using UnityEngine;
using System.Collections;

public class Deformation : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float scale = 5.0f;
    private Vector3[] baseHeight;
    private Mesh mesh;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        if (baseHeight == null)
            baseHeight = mesh.vertices;
    }

    void Update()
    {

        Vector3[] vertices = new Vector3[baseHeight.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseHeight[i];
            vertex.y += Mathf.Sin(Mathf.PI * Time.time * speed + vertex.x) * Mathf.Sin(Mathf.PI * Time.time * speed + vertex.z) * scale * Time.smoothDeltaTime;
            //vertex.y += Mathf.Sin(Time.time * speed + vertex.x) * Mathf.Sin(Time.time * speed + vertex.z) * scale * Time.smoothDeltaTime;
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
    }
}