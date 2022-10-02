using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderElongatorVert : MonoBehaviour
{
    // 4.2 is player max to the side
    public GameObject player;
    private Vector3 scale;
    public float side;
    // Start is called before the first frame update
    void Start()
    {
        scale = new Vector3(1,1,1);
        transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.localScale = new Vector3(1, 1, (side * 4.2f - player.transform.position.z) * side * 0.693f + 0.25f);
    }
}
