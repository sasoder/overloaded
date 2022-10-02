using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public static LevelLoader Instance;
    public Animator transition;
    public float transitionTime = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
       Instance = this; 
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadNextLevel(string lev) {
        StartCoroutine(LoadLevel(lev));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
