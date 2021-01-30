using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

public class WaitRoom : MonoBehaviourPun
{

    [SerializeField]
    private GameObject[] playerUI;

    [SerializeField]
    private Button launchButton;

    [SerializeField]
    private int numberOfPlayer;

    private void OnEnable()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            launchButton.gameObject.SetActive(false);
        }
        else
        {
            launchButton.interactable = false;
        }
    }


    void Update()
    {
        numberOfPlayer = PhotonNetwork.CurrentRoom.PlayerCount;
        Debug.Log(PhotonNetwork.CountOfPlayers);
        NewPlayer();

        if (PhotonNetwork.IsMasterClient)
        {
            if (numberOfPlayer > 1)
            {
                launchButton.interactable = true;
            }
        }
    }

    private void NewPlayer()
    {

        playerUI[numberOfPlayer - 1].GetComponent<PhotonView>().TransferOwnership(numberOfPlayer);
        playerUI[numberOfPlayer - 1].GetComponent<PlayerInterfaceRoom>().SendInfo(PhotonNetwork.PlayerList[numberOfPlayer - 1].NickName, numberOfPlayer);

    }


}
