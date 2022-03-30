using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    [SerializeField] private Spawner _spawner = null;

    private float startY = 1.6f;
    
    private void Start()
    {
        //startY = transform.position.y;
    }

    private void Update()
    {
        float distance = (_spawner.ActiveTailY + startY) - transform.position.y;

        if (distance > 0)
        {
            transform.Translate(Vector3.up * 1 * Time.deltaTime);
        }
    }
}
