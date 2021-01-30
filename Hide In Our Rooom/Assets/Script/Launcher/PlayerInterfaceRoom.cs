using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerInterfaceRoom : MonoBehaviour, IPunObservable
{
    [SerializeField]
    private Text playerName;

    [SerializeField]
    private Text playerNumber;

    [SerializeField]
    private RawImage background;

    [SerializeField]
    private int playerNumb;
    [SerializeField]
    private string namePlayer;

    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(namePlayer);
            stream.SendNext(playerNumb);
        }
        else
        {
            this.namePlayer = (string)stream.ReceiveNext();
            this.playerNumb = (int)stream.ReceiveNext();

        }
    }

    #endregion

    public void Update()
    {
        if(name!= null)
        {

        ActivePlayer(namePlayer, playerNumb);
        }
    }


    public void SendInfo(string name, int number)
    {
        namePlayer = name;
        playerNumb = number;
        Debug.Log(name + number);
    }

    public void ActivePlayer(string Name, int number)
    {
        playerName.text = Name;
        playerNumber.text = "Player " + number.ToString();
        if (number == 1) background.color = Color.blue;
        if (number == 2) background.color = Color.red;
    }


}
