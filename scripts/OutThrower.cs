using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutThrower : MonoBehaviour
{
    private bool open;
    private BoxCollider col;
    private Vector3 zZero;
    private bool playOnce;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
        col = GetComponent<BoxCollider>();
        zZero = new Vector3(1, 1, 0);
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        open = Input.GetKey("space");
        if(playOnce && open) {
            source.Play();
            playOnce = false;
        } else if (!open) {
            playOnce = true;
        }
    }

    void FixedUpdate() {
        if(open) {
            // col.enabled = false;
            transform.localScale = Vector3.Lerp(transform.localScale, zZero, Time.deltaTime * 20);
        } else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * 20);
        }
    }
}
