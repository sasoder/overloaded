using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using LootLocker.Requests;

public class PlayerID : MonoBehaviour
{
    private string playerID;
    private bool inputActivated;
    public TextMeshPro playerText;
    public TextMeshPro typeText;

    void Awake() {
    }
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("playerID")) {
            playerID = PlayerPrefs.GetString("playerID");
            playerText.text = playerID;
            typeText.text = "looking good! (backspace to edit)";
        }
        else {
            playerID = "";
        }

    }



    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            if(playerID.Length == 0) {
                inputActivated = true;
                typeText.text = "type your username NOW.";
                Debug.Log("Input activated");
                return;
            }
            playerID = playerText.text;
            inputActivated = false;
            typeText.text = "looking good! (backspace to edit)";
            PlayerPrefs.SetString("playerID", playerID);
        }



        if(inputActivated) {

            if(Input.anyKeyDown) {
                if(Input.inputString == "\b") {
                    if(playerID.Length == 0) return;
                    playerID = playerID.Remove(playerID.Length - 1);
                    playerText.text = playerID;
                    return;
                }
                foreach (char c in Input.inputString)
                {
                    if(playerText.text.Length < 12) {
                        playerID += c;
                        playerText.text = playerID;
                    }
                }
            }
     
        }

        if (Input.inputString == "\b")
        {
            if(typeText.text == "looking good! (backspace to edit)") {
                typeText.text = "not happy?";
                inputActivated = true;
            }
        }
  
     
    }
}
