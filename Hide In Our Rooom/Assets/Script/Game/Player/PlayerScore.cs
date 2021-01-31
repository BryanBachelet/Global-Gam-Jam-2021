using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

using UnityEngine;
using Photon.Pun;

public class PlayerScore : MonoBehaviourPun, IPunObservable
{

    public int scorePlayer;

    public RectTransform UiPrefab;
    private Rect rect;
    public Text scoreUI;
    public Text playerName;

    private string namePlayer;


    void Awake()
    {
        if (photonView.Owner.ActorNumber == 1)
        {
            UiPrefab.anchoredPosition = new Vector2(623, -377);


        }
        namePlayer = PlayerPrefs.GetString("PlayerName");

    }


    void Update()
    {
        scoreUI.text = "Score : " + scorePlayer.ToString();
        playerName.text = namePlayer;
    }


    #region Stream Data

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(scorePlayer);
            stream.SendNext(namePlayer);
        }
        else
        {
            this.scorePlayer = (int)stream.ReceiveNext();
            this.namePlayer = (string)stream.ReceiveNext();
        }
    }

    #endregion

    #region Public Methods

    public void AddScore(int scoreWin)
    {
        scorePlayer += scoreWin;
    }

    public void ResetScore()
    {
        scorePlayer = 0;
    }

    #endregion
}
