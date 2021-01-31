using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Photon.Pun;


public class PlayerState : MonoBehaviourPun, IPunObservable
{
    public enum PlayerCurrentState { Search, Wait }

    public PlayerCurrentState currentState = PlayerCurrentState.Search;

    #region Private Public

    [SerializeField]
    private GameObject playerSprite;

    [SerializeField]
    private GameObject playerUI;

    #endregion

    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentState);

        }
        else
        {
            this.currentState = (PlayerCurrentState)stream.ReceiveNext();
        }
    }

    #endregion

    public void Start()
    {
        //----- Misc ---- 
        if (PhotonNetwork.PlayerList[0] == photonView.Owner)
        {
            currentState = PlayerCurrentState.Wait;
        }
    }

    void Update()
    {

        if (currentState == PlayerCurrentState.Wait)
        {
            ChangeSpriteState(false);
            UIPlayer(true);

        }
        if (currentState == PlayerCurrentState.Search)
        {
            ChangeSpriteState(true);
            UIPlayer(false);
        }
    }

    private void ChangeSpriteState(bool isVisible)
    {
        playerSprite.SetActive(isVisible);

    }

    private void UIPlayer(bool isVisible)
    {
        if (photonView.IsMine)
        {
            playerUI.SetActive(isVisible);
        }
    }

    #region Public Methods

    public void ChangeStatePlayer(PlayerCurrentState playerCurrent)
    {
        currentState = playerCurrent;
    }

    public void ChangeStatePlayer()
    {
        if (currentState == PlayerCurrentState.Wait)
        {
            currentState = PlayerCurrentState.Search;
        }
        else
        {
            currentState = PlayerCurrentState.Wait;
        }
    }

    #endregion 
}
