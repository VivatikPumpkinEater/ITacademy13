using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubeMeshGenerator _cube = null;
    [SerializeField] private float _actualY = 0.65f;
    [SerializeField] private float speed = 2f;

    [SerializeField] private GameObject _base = null;

    [SerializeField] private Sliced _sliced = null;

    private Vector3 _spawnPoint;
    private Vector3 _startPosition;
    private bool _rightSpawn = true;

    private GameObject _activeTile = null;

    public float ActiveTailY
    {
        get => _activeTile.transform.position.y;
    }

    private bool _gameOver = false;

    private void Start()
    {
        Spawn();
        _actualY += 0.25f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FindOffset();
        }

        Movement();
    }

    private void Movement()
    {
        if (_activeTile != null && !_gameOver)
        {
            float newPosition = Mathf.PingPong(Time.time * 2, 3f);

            if (!_rightSpawn)
            {
                _activeTile.transform.position = _startPosition + Vector3.back * newPosition;
            }
            else
            {
                _activeTile.transform.position = _startPosition + Vector3.right * newPosition;
            }
        }
    }

    private void Spawn()
    {
        //_activeTile = _cube.CompleteCube();
        _activeTile = _sliced.Spawn();
        _activeTile.transform.position = FindDirection();

        _startPosition = _activeTile.transform.position;
    }

    private Vector3 FindDirection()
    {
        switch (_rightSpawn)
        {
            case true:
                _spawnPoint = new Vector3(_base.transform.position.x, _actualY, _base.transform.position.z + 1f);
                break;
            case false:
                _spawnPoint = new Vector3(_base.transform.position.x - 1f, _actualY, _base.transform.position.z);
                break;
        }

        _rightSpawn = !_rightSpawn;

        return _spawnPoint;
    }

    private void FindOffset()
    {
        float offsetX = _activeTile.transform.position.x - _base.transform.position.x;
        float offsetZ = _activeTile.transform.position.z - _base.transform.position.z;

        Debug.Log(offsetX + " X");
        Debug.Log(offsetZ + " Z");
        
        _base = _sliced.Slice(offsetX, offsetZ, _actualY, _rightSpawn, _base.transform.position);

        if (_base != null)
        {
            Spawn();
            _actualY += 0.25f;
        }
        else
        {
            _gameOver = true;

            _activeTile.AddComponent<Rigidbody>();
            
            Debug.Log("GameOver");
        }
    }
}