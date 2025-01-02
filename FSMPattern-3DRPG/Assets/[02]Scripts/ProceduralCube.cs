using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class ProceduralCube : MonoBehaviour
{
    public Vector3 size = new Vector3(1.0f, 1.0f, 1.0f);
    public Vector3 offset = new Vector3(0, 0, 0);

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public enum Direction
    {
        FORWARD, // (0, 0, +1)
        RIGHT,   // (+1, 0, 0)
        UP,      // (0, +1, 0)
        BACK,    // (0, 0, -1)
        LEFT,    // (-1, 0, 0)
        DOWN     // (0, -1, 0)
    }

    public int[][] faceNumber =
    {
        new int[] {0, 1, 2, 3}, // FORWARD
        new int[] {5, 0, 3, 6}, // RIGHT 
        new int[] {5, 4, 1, 0}, // UP     
        new int[] {4, 5, 6, 7}, // BACK  
        new int[] {1, 4, 7, 2}, // LEFT 
        new int[] {3, 2, 7, 6}, // DOWN    
    };

    void OnValidate()
    {
        if (mesh == null) return;

        if (size.magnitude > 0 || offset.magnitude > 0)
        {
            setMeshData(size);
            createProceduralMesh();
        }
    }

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        setMeshData(size);
        createProceduralMesh();
    }

    void setMeshData(Vector3 size)
    {
        vertices = new Vector3[24];
        
        Vector3 v0 = Vector3.Scale(new Vector3(+0.5f, +0.5f, +0.5f), size) + offset;
        Vector3 v1 = Vector3.Scale(new Vector3(-0.5f, +0.5f, +0.5f), size) + offset;
        Vector3 v2 = Vector3.Scale(new Vector3(-0.5f, -0.5f, +0.5f), size) + offset;
        Vector3 v3 = Vector3.Scale(new Vector3(+0.5f, -0.5f, +0.5f), size) + offset;
        Vector3 v4 = Vector3.Scale(new Vector3(-0.5f, +0.5f, -0.5f), size) + offset;
        Vector3 v5 = Vector3.Scale(new Vector3(+0.5f, +0.5f, -0.5f), size) + offset;
        Vector3 v6 = Vector3.Scale(new Vector3(+0.5f, -0.5f, -0.5f), size) + offset;
        Vector3 v7 = Vector3.Scale(new Vector3(-0.5f, -0.5f, -0.5f), size) + offset;

        Vector3[] vSet = new Vector3[] {
            v0, v1, v2, v3, v4, v5, v6, v7
        };

        triangles = new int[3 * 12];

        int vIdx, tIdx;

        vIdx = tIdx = 0;
        for(int dir = 0; dir < 6; dir++)
        {
            vertices[vIdx++] = vSet[faceNumber[dir][0]];
            vertices[vIdx++] = vSet[faceNumber[dir][1]];
            vertices[vIdx++] = vSet[faceNumber[dir][2]];
            vertices[vIdx++] = vSet[faceNumber[dir][3]];

            triangles[tIdx++] = vIdx - 4 + 0;
            triangles[tIdx++] = vIdx - 4 + 1;
            triangles[tIdx++] = vIdx - 4 + 3;

            triangles[tIdx++] = vIdx - 4 + 1;
            triangles[tIdx++] = vIdx - 4 + 2;
            triangles[tIdx++] = vIdx - 4 + 3;
        }
    }

    void createProceduralMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        Destroy(this.GetComponent<BoxCollider>());
        this.gameObject.AddComponent<BoxCollider>();
    }
}