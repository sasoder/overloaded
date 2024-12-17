using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fail : MonoBehaviour
{
    public GameObject psObject;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collisionInfo) {
        Destroy(collisionInfo.gameObject);
        GameObject ps = Instantiate(psObject, collisionInfo.gameObject.transform.position, Quaternion.identity);
        Destroy(ps, 2f);
        GameManager.Instance.LoseLife();
    }
}
