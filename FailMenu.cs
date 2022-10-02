using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailMenu : MonoBehaviour
{
    public GameObject psObject;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collisionInfo) {
        if(collisionInfo.gameObject.tag == "Untagged") {
            return;
        }
        Destroy(collisionInfo.gameObject);
        GameObject ps = Instantiate(psObject, collisionInfo.gameObject.transform.position, Quaternion.identity);
        Destroy(ps, 2f);
        source.Play();

    }
}
