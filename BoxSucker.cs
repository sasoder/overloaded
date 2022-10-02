using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSucker : MonoBehaviour
{
    public string colour;
    public GameObject psObject;
    public GameObject psObjectLoss;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void addScore() {
        GameManager.Instance.IncreaseScore();
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        GameObject collider = collisionInfo.gameObject;
        Destroy(collider);
        if(collider.tag == colour) {
            addScore();
            GameObject ps = Instantiate(psObject, transform.position, Quaternion.identity);
            Destroy(ps, 2f);
        } else {
            GameObject ps = Instantiate(psObjectLoss, transform.position, Quaternion.identity);
            Destroy(ps, 2f);
            GameManager.Instance.LoseLife();
        }
    }
}
