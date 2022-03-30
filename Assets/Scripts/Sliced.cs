using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro.EditorUtilities;
using UnityEngine;

public class Sliced : MonoBehaviour
{
    [SerializeField] private CubeMeshGenerator _cubeMesh = null;

    [SerializeField] private float _slice;

    private GameObject _cube = null;

    private Vector3 _cubeSize;

    private List<Color32> _spectrum = new List<Color32>()
    {
        new Color32(0, 255, 33, 255),
        new Color32(167, 255, 0, 255),
        new Color32(230, 255, 0, 255),
        new Color32(255, 237, 0, 255),
        new Color32(255, 206, 0, 255),
        new Color32(255, 185, 0, 255),
        new Color32(255, 142, 0, 255),
        new Color32(255, 111, 0, 255),
        new Color32(255, 58, 0, 255),
        new Color32(255, 0, 0, 255),
        new Color32(255, 0, 121, 255),
        new Color32(255, 0, 164, 255),
        new Color32(241, 0, 255, 255),
        new Color32(209, 0, 255, 255),
        new Color32(178, 0, 255, 255)
    };

    private int _modifier;
    private int _colorIndex;

    private void Awake()
    {
        _cubeSize = new Vector3(1, 0.25f, 1f);

        _modifier = 1;
        _colorIndex = 0;
    }

    public GameObject Spawn()
    {
        _cube = _cubeMesh.CompleteCube(_cubeSize);

        _colorIndex += _modifier;
        if (_colorIndex == _spectrum.Count || _colorIndex == -1)
        {
            _modifier *= -1;
            _colorIndex += 2 * _modifier;
        }

        _cube.GetComponent<Renderer>().material.color = _spectrum[_colorIndex];

        return _cube;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Slice(_slice, 'x');
        }
    }

    public GameObject Slice(float offsetX, float offsetZ, float actualY, bool right, Vector3 baseTail)
    {
        actualY -= _cubeSize.y;

        float secondOffsetX = 0f, secondOffsetZ = 0f;

        float sliceX = 0, sliceZ = 0;

        if (right)
        {
            secondOffsetX = _cubeSize.x - offsetX;
            sliceX = offsetX;
        }
        else
        {
            secondOffsetZ = _cubeSize.z - offsetZ;
            sliceZ = offsetZ;
        }

        offsetX = Mathf.Abs(offsetX);
        offsetZ = Mathf.Abs(offsetZ);

        if (_cubeSize.x - offsetX > 0 && _cubeSize.z > offsetZ)
        {
            var first = _cubeMesh.CompleteCube(new Vector3(_cubeSize.x - offsetX,
                _cubeSize.y,
                _cubeSize.z - offsetZ));
            var second = _cubeMesh.CompleteCube(new Vector3(Mathf.Abs(_cubeSize.x - secondOffsetX),
                _cubeSize.y,
                Mathf.Abs(_cubeSize.z - secondOffsetZ)));

            Debug.Log(new Vector3(_cubeSize.x - offsetX,
                _cubeSize.y,
                _cubeSize.z - offsetZ) + " first");
            
            Debug.Log(new Vector3(_cubeSize.x - secondOffsetX,
                _cubeSize.y,
                _cubeSize.z - secondOffsetZ) + " second");

            Destroy(_cube);

            first.transform.position = new Vector3(baseTail.x, actualY, baseTail.z);
            //second.transform.position = baseTail;

            if (sliceZ > 0 | sliceX > 0)
            {
                first.transform.position -= new Vector3(-offsetX / 2f, 0, -offsetZ / 2f);
                second.transform.position = first.transform.position + new Vector3(offsetX, 0, offsetZ);
            }
            else
            {
                first.transform.position -= new Vector3(offsetX / 2f, 0, offsetZ / 2f);
                second.transform.position = first.transform.position + new Vector3(-offsetX, 0, -offsetZ);
            }

            first.GetComponent<Renderer>().material.color = _spectrum[_colorIndex];
            second.GetComponent<Renderer>().material.color = _spectrum[_colorIndex];

            //second.AddComponent<BoxCollider>();
            second.AddComponent<Rigidbody>();

            _cube = first.gameObject;

            _cubeSize -= new Vector3(offsetX, 0, offsetZ);

            return first;
        }

        return null;
    }
}