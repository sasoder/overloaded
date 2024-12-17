using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    public float moveSpeed;
    float xSpeed;
    float zSpeed;
    public float friction;
    Vector3 dir;
    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }

    void FixedUpdate() {
        MoveChar();  
    }

    private void MyInput() {
        xSpeed = Input.GetAxisRaw("Horizontal");
        zSpeed = Input.GetAxisRaw("Vertical");
    }

    private void MoveChar() {
        dir.x = xSpeed;
        dir.z = zSpeed;

        rb.AddForce(dir * moveSpeed, ForceMode.Force);
    }

}
