using UnityEngine;
using System.Collections.Generic;

public class Area : MonoBehaviour
{
    [SerializeField]
    private Config _config;
    
    private void OnDrawGizmos()
    {
        var mesh = GeneratePlayerSpawnZonesMesh(_config.MinRadius, _config.MaxRadius, _config.Angle, _config.Sectors);

        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawWireMesh(mesh, transform.position, transform.rotation);
    }

    private static Mesh GeneratePlayerSpawnZonesMesh(float minRadius, float maxRadius, float angle, int sectors)
    {
        var mesh = new Mesh();

        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var normals = new List<Vector3>();
        
        var vertexId = 0;
        var deltaAngle = angle / sectors;
        
        var currentAngle = Mathf.Deg2Rad * (90.0f + angle / 2);
        var cosAngle = Mathf.Cos(currentAngle);
        var sinAngle = Mathf.Sin(currentAngle);
        
        for (var i = 0; i < sectors; i++)
        {
            var p0 = new Vector3(minRadius * cosAngle, 0, minRadius * sinAngle);
            vertices.Add(p0);
            normals.Add(Vector3.up);

            var p1 = new Vector3(maxRadius * cosAngle, 0, maxRadius * sinAngle);
            vertices.Add(p1);
            normals.Add(Vector3.up);

            currentAngle -= Mathf.Deg2Rad * deltaAngle;
            cosAngle = Mathf.Cos(currentAngle);
            sinAngle = Mathf.Sin(currentAngle);

            var p2 = new Vector3(maxRadius * cosAngle, 0, maxRadius * sinAngle);
            vertices.Add(p2);
            normals.Add(Vector3.up);

            var p3 = new Vector3(minRadius * cosAngle, 0, minRadius * sinAngle);
            vertices.Add(p3);
            normals.Add(Vector3.up);
            
            triangles.Add(vertexId);
            triangles.Add(vertexId + 1);
            triangles.Add(vertexId + 2);

            triangles.Add(vertexId);
            triangles.Add(vertexId + 2);
            triangles.Add(vertexId + 3);

            vertexId += 4;
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.normals = normals.ToArray();

        return mesh;
    }
}