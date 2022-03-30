using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CubeMeshGenerator : MonoBehaviour
{
    [SerializeField] private Material _mat = null;

    [SerializeField] private Vector3 _size;
    [SerializeField] private float _step = 0.1f;
    private Mesh _mesh = null;

    private Vector3 _startPosition;
    
    private void Start()
    {
        _startPosition = transform.position;
    }

    public GameObject CompleteCube()
    {
        GameObject cube = new GameObject();

        cube.AddComponent<MeshFilter>().mesh = Cube(_size.x, _size.z, _size.y);
        cube.AddComponent<MeshRenderer>().material = _mat;
        
        //_mesh.RecalculateNormals();

        return cube;
    }
    
    public GameObject CompleteCube(Vector3 size)
    {
        GameObject cube = new GameObject();

        cube.AddComponent<MeshFilter>().mesh = Cube(size.x, size.z, size.y);
        cube.AddComponent<MeshRenderer>().material = _mat;

        cube.AddComponent<BoxCollider>().size = size;


        //_mesh.RecalculateNormals();

        return cube;
    }

    private void Update()
    {

       //if (Input.GetKeyDown(KeyCode.W))
       //{
       //    _mesh = Cube(_size.x, _size.z, _size.y);

       //    GetComponent<MeshFilter>().mesh = _mesh;
       //    GetComponent<MeshRenderer>().material = _mat;

       //    _mesh.RecalculateNormals();
       //}
    }

    private void Movement()
    {
        float newPosition = Mathf.PingPong(Time.time * 2, 2.4f);

        transform.position = _startPosition + Vector3.back * newPosition;
    }

    private Mesh Quad(Vector3 origin, Vector3 width, Vector3 length)
    {
        var normal = Vector3.Cross(length, width).normalized;
        var mesh = new Mesh
        {
            vertices = new[] {origin, origin + length, origin + length + width, origin + width},
            normals = new[] {normal, normal, normal, normal},
            uv = new[] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0)},
            triangles = new[]
            {
                0, 1, 2,
                0, 2, 3
            }
        };
        
        return mesh;
    }

    private Mesh Cube(float x, float z, float height)
    {
        return Parallelepiped(Vector3.right * x, Vector3.forward * z, Vector3.up * height);
    }

    private Mesh Parallelepiped(Vector3 width, Vector3 length, Vector3 height)
    {
        var corner0 = -width / 2 - length / 2 - height / 2;
        var corner1 = width / 2 + length / 2 + height / 2;

        var combine = new CombineInstance[6];
        combine[0].mesh = Quad(corner0, length, width);
        combine[1].mesh = Quad(corner0, width, height);
        combine[2].mesh = Quad(corner0, height, length);
        combine[3].mesh = Quad(corner1, -width, -length);
        combine[4].mesh = Quad(corner1, -height, -width);
        combine[5].mesh = Quad(corner1, -length, -height);

        var mesh = new Mesh();
        mesh.CombineMeshes(combine, true, false);
        return mesh;
    }
}