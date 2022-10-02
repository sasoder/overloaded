using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{

    public int leaderboardID;
    public TextMeshPro playerNames;
    public TextMeshPro playerScores;
    


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetupRoutine());
    }

    IEnumerator SetupRoutine() {
        yield return LoginRoutine();
        yield return FetchTopHiScoresRoutine();
    }

        private IEnumerator LoginRoutine()
    {
        
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response)=>
        {
            if(response.success) {
                Debug.Log("Logged in!!!");
                // PlayerPrefs.SetString("hiScoreID", response.player_id.ToString());
                done = true;
            } else {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator FetchTopHiScoresRoutine() {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardID, 5, 0, (response) => {
            if(response.success) {
                string tempPlayerNames = "";
                string tempPlayerScores = "";

                LootLockerLeaderboardMember [] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if(members[i].player.name != "") {
                        tempPlayerNames += members[i].player.name;
                    }
                    else {
                        tempPlayerNames += members[i].player.id;
                    }
                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;
                playerNames.text = tempPlayerNames;
                playerScores.text = tempPlayerScores;
            }
            else {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
