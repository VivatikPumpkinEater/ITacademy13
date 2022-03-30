using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CubeGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 _size;
    [SerializeField] private float _triangleCount = 12;

    private Mesh _mesh = null;

    private int _vericalsCount;

    private void Start()
    {
        _mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = _mesh;
        _mesh.vertices = GenerateVertices(1,10);
        _mesh.triangles = GenerateTriangulate2(_vericalsCount,10);

        _mesh.RecalculateNormals();
    }

    private Vector3[] GenerateVertices()
    {
        return new Vector3[]
        {
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),

            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),

            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),
        };
    }

    private int[] GenerateTriangulate()
    {
        return new int[]
        {
            0, 1, 2, 1, 3, 2
        };
    }

    private Vector3[] GenerateVertices(float size, int devil)
    {
        float shag = size / devil;

        List<Vector3> vertices = new List<Vector3>();

        Vector3 point = new Vector3(size / 2 * -1, 0, size / 2 * -1);

        for (int x = 0; x < devil; x++)
        {
            for (int z = 0; z < devil; z++)
            {
                point += new Vector3(0, 0, shag);
                
                vertices.Add(point);
            }

            point = new Vector3(point.x, 0, size / 2 * -1);
            point += new Vector3(shag, 0, 0);
        }
        
        Vector3[] vertic = new Vector3[vertices.Count];
        
        for (int i = 0; i < vertices.Count; i++)
        {
            vertic[i] = vertices[i];
        }

        _vericalsCount = vertices.Count;

        return vertic;
    }

    private int[] GenerateTriangulate2(int verticals, int devil)
    {
        List<int> triangulate = new List<int>();

        for (int x = 0; x < verticals - 21; x++)
        {
            triangulate.Add(x);
            triangulate.Add(x + 1);
            triangulate.Add(x + 1 + devil);
                
            triangulate.Add(x + 1);
            triangulate.Add(x + devil + 2);
            triangulate.Add(x + devil + 1);
        }

        int[] triang = new int[triangulate.Count];
        
        for (int i = 0; i < triangulate.Count; i++)
        {
            triang[i] = triangulate[i];
        }
        
        Debug.Log(triang.Length);

        return triang;
    }

    private void GenerateQuad()
    {
        
    }
}