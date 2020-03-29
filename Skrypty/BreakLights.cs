using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakLights : MonoBehaviour
{
    public CarController car;
    private Renderer render;
    void Start()
    {
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        render.enabled = car.breaking;
    }
}
