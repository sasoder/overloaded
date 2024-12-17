using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpawner : MonoBehaviour
{

    public List<GameObject> boxes;
    public float yHeight;
    public Transform boxContainer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(boxContainer.childCount == 0) {
            SpawnBox();
        }
    }

    private GameObject GetRandomBox() {
        return boxes[Mathf.RoundToInt(Random.Range(0, boxes.Count))];
    }

    private void SpawnBox() {
        Instantiate(GetRandomBox(), new Vector3(transform.position.x, yHeight, transform.position.z), transform.rotation, boxContainer);
    }
}
