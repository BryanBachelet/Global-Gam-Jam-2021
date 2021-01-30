using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMouvement : MonoBehaviourPun
{
    public float mouvementSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            // Mouvement Part 
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            transform.position += new Vector3(1, 1, 0) * v * mouvementSpeed * Time.deltaTime + new Vector3(1, -1) * h * mouvementSpeed * Time.deltaTime;
        }

    }
}
