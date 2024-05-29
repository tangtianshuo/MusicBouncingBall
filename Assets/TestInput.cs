using System.Collections;
using System.Collections.Generic;
using HeathenEngineering.PhysKit;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    public TrickShot trickShot;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            trickShot.Shoot();
        }
    }
}
