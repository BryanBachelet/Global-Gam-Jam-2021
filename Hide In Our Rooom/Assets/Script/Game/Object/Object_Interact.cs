using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Object_Interact : MonoBehaviourPunCallbacks, IPunObservable
{
    public Color classicColor = Color.white;
    public Color colorInteract = Color.blue;
    public bool isInteract = false;
    public int resetFrame = 3;

    private int countFrame = 1;
    private SpriteRenderer spriteRenderer;
    private PhotonView photonView;

    public void ActiveInteract(PhotonView player)
    {
        ChangeOwner(player);
        isInteract = true;
        countFrame = 0;
    }

    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isInteract);
          
        }
        else
        {
            this.isInteract = (bool)stream.ReceiveNext();
          
        }
    }

    #endregion


    public virtual void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        photonView = this.GetComponent<PhotonView>();
        spriteRenderer.color = classicColor;
    }

    public virtual void Update()
    {
        Debug.Log("Interact = " + isInteract);
        if (isInteract)
        {
            spriteRenderer.color = colorInteract;
        }
        else
        {
            spriteRenderer.color = classicColor;
        }

    }

    public virtual void LateUpdate()
    {
        if (photonView.Owner == null)
        {

            countFrame++;
            if (countFrame > resetFrame)
            {
                isInteract = false;
            }
        }
    }

    public void ChangeOwner(PhotonView player)
    {
        if (photonView.Owner != player.Owner)
        {
            photonView.TransferOwnership(player.Owner);
        }

    }

    public void ResetOwner()
    {
        photonView.TransferOwnership(0);
    }
}
