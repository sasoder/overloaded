using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public string scenie;
    private AudioSource source;
    public AudioClip hithurt;
    public GameObject psObject;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        Destroy(collisionInfo.gameObject);
        GameObject ps = Instantiate(psObject, collisionInfo.gameObject.transform.position, Quaternion.identity);
        Destroy(ps, 2f);
        if(scenie.Length > 1) {
            if(scenie == "HardScene") {
                if(PlayerPrefs.GetInt("best") < 15) {
                    GameObject.Find("worthy").GetComponent<TextMeshPro>().text = "you're not worthy,\nyet.";
                    source.PlayOneShot(hithurt);
                    return;
                }
            }
            source.Play();
            LevelLoader.Instance.LoadNextLevel(scenie);
        }
        else {
            source.PlayOneShot(hithurt);
        }
    }
}
