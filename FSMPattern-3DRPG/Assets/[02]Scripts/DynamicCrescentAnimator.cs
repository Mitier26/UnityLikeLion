using System.Collections;
using UnityEngine;

public class DynamicCrescentAnimator : MonoBehaviour
{
    [Header("Crescent Position(necessary!)")]
    public Transform crescentPosition;        

    [Header("Crescent Settings")]
    public float initialInnerRadius = 0.3f;   
    public float initialOuterRadius = 0.5f;   
    public float targetOuterRadius = 1.0f;    
    public float angle = 180.0f;              
    public int segments = 36;                
    public float animationDuration = 0.2f;   


    [Header("Material Settings")]
    public Color crescentColor = Color.white;
    public Shader crescentShader;
  

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            CreateAndAnimateCrescent();
        }
    }

    void CreateAndAnimateCrescent()
    {
        GameObject crescentObject = new GameObject("Crescent");
        crescentObject.transform.position = crescentPosition.position;
        crescentObject.transform.rotation = crescentPosition.rotation;

        MeshFilter meshFilter = crescentObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = crescentObject.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        mesh.MarkDynamic();
        meshFilter.mesh = mesh;

        if (crescentShader == null)
            crescentShader = Shader.Find("Unlit/Color");
        Material material = new Material(crescentShader);
        material.color = crescentColor;

        meshRenderer.material = material;

        StartCoroutine(AnimateCrescent(crescentObject, mesh, initialInnerRadius, initialOuterRadius, targetOuterRadius, 0, 180));
    }

    IEnumerator AnimateCrescent(GameObject obj, Mesh mesh, float innerRadius, float startOuterRadius, float endOuterRadius, float startAngle, float endAngle)
    {
        float elapsedTime = 0f;

        // 애니메이션 루프
        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            float currentOuterRadius = Mathf.Lerp(startOuterRadius, endOuterRadius, t);

            UpdateCrescentMesh(mesh, innerRadius, currentOuterRadius, Mathf.Lerp(startAngle, endAngle, t));
            
            //UpdateLineMesh(mesh, new Vector3(0, 0, 0), new Vector3(0.1f, 0, 0), currentOuterRadius);

            //UpdateTriangleMesh(mesh, new Vector3(0, 0, 0), new Vector3(0.5f, 0, 0), new Vector3(0, t, 0));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(obj);
    }

    void UpdateCrescentMesh(Mesh crescentMesh, float innerRadius, float outerRadius, float angle = 180.0f)
    {
        float deltaAngle = angle / segments * Mathf.Deg2Rad;

        Vector3[] vertices = new Vector3[(segments + 1) * 2];
        int[] triangles = new int[segments * 6];

        for (int i = 0; i <= segments; i++)
        {
            float currentAngle = deltaAngle * i;

            // 외부 링
            vertices[i] = new Vector3(
                Mathf.Cos(currentAngle) * outerRadius,
                Mathf.Sin(currentAngle) * outerRadius,
                0
            );

            // 내부 링
            vertices[segments + 1 + i] = new Vector3(
                Mathf.Cos(currentAngle) * innerRadius,
                Mathf.Sin(currentAngle) * innerRadius,
                0
            );
        }

        int t = 0;
        for (int i = 0; i < segments; i++)
        {
            int outer1 = i;
            int outer2 = i + 1;
            int inner1 = segments + 1 + i;
            int inner2 = segments + 1 + i + 1;

            triangles[t++] = outer1;
            triangles[t++] = outer2;
            triangles[t++] = inner1;

            triangles[t++] = outer2;
            triangles[t++] = inner2;
            triangles[t++] = inner1;
        }

        // **뒷면 추가** (삼각형 순서 반대로)
        int[] backTriangles = new int[triangles.Length];
        for (int i = 0; i < triangles.Length; i += 3)
        {
            backTriangles[i] = triangles[i];
            backTriangles[i + 1] = triangles[i + 2];
            backTriangles[i + 2] = triangles[i + 1];
        }

        int[] combinedTriangles = new int[triangles.Length + backTriangles.Length];
        triangles.CopyTo(combinedTriangles, 0);
        backTriangles.CopyTo(combinedTriangles, triangles.Length);

        crescentMesh.Clear();
        crescentMesh.vertices = vertices;
        crescentMesh.triangles = triangles;
        crescentMesh.RecalculateNormals();
        crescentMesh.RecalculateBounds();
    }

    void UpdateLineMesh(Mesh lineMesh, Vector3 start, Vector3 end, float width)
    {
        Vector3[] vertices = new Vector3[4];
        int[] triangles = new int[6];

        Vector3 direction = (end - start).normalized;
        Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0) * width;

        vertices[0] = start + perpendicular;
        vertices[1] = start - perpendicular;
        vertices[2] = end + perpendicular;
        vertices[3] = end - perpendicular;

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;

        lineMesh.Clear();
        lineMesh.vertices = vertices;
        lineMesh.triangles = triangles;
    }

    void UpdateTriangleMesh(Mesh triangleMesh, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        Vector3[] vertices = new Vector3[3];
        int[] triangles = new int[3];

        vertices[0] = p0;
        vertices[1] = p1;
        vertices[2] = p2;

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangleMesh.Clear();
        triangleMesh.vertices = vertices;
        triangleMesh.triangles = triangles;
    }
}