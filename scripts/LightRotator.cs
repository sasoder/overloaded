using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotator : MonoBehaviour
{
    public float heightScale = 1.0f;
    public float horScale = 4.0f;

    public float xScale = 1.0f;
    public float offset = 0.0f;
    public float yValue = 8f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        float height = heightScale * Mathf.PerlinNoise(Time.time * xScale + offset, 0.0f);
        pos.y = height + yValue;

        float z = horScale * Mathf.PerlinNoise(Time.time * xScale, Time.time * xScale + offset);
        pos.z = z - horScale / 2;

        float x = horScale * Mathf.PerlinNoise(0.0f, Time.time * xScale + offset);
        pos.x = x - horScale / 2;

        transform.position = pos;

    }
}
