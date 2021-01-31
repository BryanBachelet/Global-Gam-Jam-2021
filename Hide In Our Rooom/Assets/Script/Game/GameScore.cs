 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GuerhoubaGame;
using Photon.Pun;

public class GameScore : MonoBehaviourPun
{
    static public GameScore gameScore;

    public float duractionGame = 15;
    public float timeBetweenGain = 3f;
    public int scoreWinPerFiveSeconds = 15;

    private int playerWin= 1;

    private float timerGame;
    private float timerPoint;

    public PlayerScore playerScoreOne;
    private PlayerState playerStateOne;
    public PlayerScore playerScoreTwo;
    private PlayerState playerStateTwo;


    public void Awake()
    {
        gameScore = this;
    }

    public void GetPlayerScore(GameObject player, int number)
    {
        if (player.GetPhotonView().Owner == PhotonNetwork.PlayerList[0])
        {
            playerScoreOne = player.GetComponent<PlayerScore>();
            playerStateOne = player.GetComponent<PlayerState>();
        }
       else
        {
            playerScoreTwo = player.GetComponent<PlayerScore>();
            playerStateTwo = player.GetComponent<PlayerState>();
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (timerPoint > timeBetweenGain)
        {
            AddScorePlayer(playerWin,scoreWinPerFiveSeconds);
            timerPoint = 0;
        }
        else
        {
            timerPoint += Time.deltaTime;
        }

        if (timerGame > duractionGame)
        {
            playerStateOne.ChangeStatePlayer();
            playerStateTwo.ChangeStatePlayer();
            timerGame = 0;
        }
        else
        {
            timerGame += Time.deltaTime;
;        }
    }

    private void AddScorePlayer(int playerNumber, int score)
    {
        if (playerNumber == 1)
        {
            playerScoreOne.AddScore(score);
        }
        else
        {
            playerScoreTwo.AddScore(score);
        }
    }
}
