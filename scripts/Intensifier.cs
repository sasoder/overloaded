using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intensifier : MonoBehaviour
{
    private Light l;
    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        l.intensity += Time.deltaTime * 3;
    }
}
