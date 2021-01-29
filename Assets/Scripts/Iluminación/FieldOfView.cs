using System;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] Transform target;
    [Range(1,360)]
    [SerializeField] float fov = 360f;
    [SerializeField] int numAristas = 360;
    [SerializeField] float startAngle;
    [SerializeField] float visionDistance = 8f;
    [SerializeField] float wallVisionFactor;
    [SerializeField] LayerMask layerMask;
    
    Mesh mesh;
    Vector3 origin;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(target.transform.position,visionDistance);
    }

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
    }

    void Update()
    {
        SetOrigin(target.position);
        MeshGenerator();
    }

    void MeshGenerator()
    {
        var vertices = new Vector3[numAristas + 2];
        var triangles = new int[numAristas * 3];

        var currentAngle = startAngle;
        var incrementAngle = fov / numAristas;

        vertices[0] = origin;

        var verticeIndex = 1;
        var triangleIndex = 0;

        for(var i = 0; i <= numAristas; i++)
        {
            var raycastHit = Physics2D.Raycast(origin, GetVectorFromAngle(currentAngle), visionDistance, layerMask);

            var currentVertice = raycastHit.collider == null
                ? origin + GetVectorFromAngle(currentAngle) * visionDistance
                : (Vector3) raycastHit.point + GetVectorFromAngle(currentAngle).normalized * wallVisionFactor;
            
            vertices[verticeIndex] = currentVertice;
            
            if(i != 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = verticeIndex - 1;
                triangles[triangleIndex + 2] = verticeIndex;

                triangleIndex += 3;
            }

            verticeIndex++;
            currentAngle -= incrementAngle;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    static Vector3 GetVectorFromAngle(float angle)
    {
        var angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    void SetOrigin(Vector3 newOrigin) => origin = newOrigin;
}
