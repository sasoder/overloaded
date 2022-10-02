using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoxSpawner : MonoBehaviour
{
    public float timeSince;
    public int timeUntilMoreBoxes;
    public float ten;
    private float timeSinceStart;
    public float warningTime;
    public float yHeight;
    public List<GameObject> boxes;
    public GameObject warningBox;
    public AudioSource warning;
    public AudioClip payloadSound;
    public AudioClip spawnBlockSound;
    public TextMeshPro timer;
    public TextMeshPro payload;
    private int payloadSize;
    float xPos;
    float zPos;
    bool hasSpawnedGuide;
    // Start is called before the first frame update
    void Start()
    {
        timeSince = 0;
        payloadSize = 1;
        timeSinceStart = 0;
        hasSpawnedGuide = false;
        // warning = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.GetGameOver()) {
            return;
        }
        CheckPayload();
        UpdateText();
        UpdatePayloadText();
        timeSince += Time.deltaTime;
        timeSinceStart += Time.deltaTime;
        if(WarningExceeded()) {
            if(!hasSpawnedGuide) {
                xPos = Random.Range(-3f, 3f);
                zPos = Random.Range(-3f, 3f);
                SpawnGuide();
                hasSpawnedGuide = true;
            }
            if(TimeExceeded()) {
                timeSince = 0;
                SpawnBox();
                hasSpawnedGuide = false;
            }
        }
    }

    void CheckPayload(){
        if(Mathf.RoundToInt((timeSinceStart / timeUntilMoreBoxes)) > payloadSize) {
            payloadSize++;
            warning.PlayOneShot(payloadSound);
        }
    }

    void UpdateText() {
        timer.text = "delivery in " + Mathf.CeilToInt((ten - timeSince)).ToString() + "s...";
    }

    void UpdatePayloadText() {
        payload.text = "payload size: " + payloadSize.ToString();
    }

    private bool TimeExceeded() {
        return timeSince >= ten;
    }

    private bool WarningExceeded() {
        return timeSince >= ten - warningTime;
    }
    private GameObject GetRandomBox() {
        return boxes[Mathf.RoundToInt(Random.Range(0, boxes.Count))];
    }

    private void SpawnBox() {
        for (int i = 0; i < payloadSize; i++)
        {
            Instantiate(GetRandomBox(), new Vector3(xPos, yHeight + i, zPos), transform.rotation);
        }
        warning.PlayOneShot(spawnBlockSound);
    }

    private void SpawnGuide() {
        warning.Play();
        GameObject guide = Instantiate(warningBox, new Vector3(xPos, yHeight, zPos), transform.rotation * Quaternion.Euler(90, 0, 0));
        Destroy(guide, warningTime);
    }
}
