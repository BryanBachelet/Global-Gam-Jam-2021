﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerInteraction : MonoBehaviourPun
{
    #region Public Variable
    public float radiusOfDectection = 2f;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Collider2D[] collider2Ds;

    #endregion

    #region Private Variable
    // Utilisation pour l'optimisation
    private Vector3 dectecPosition;

    private Collider2D objectTarget;
    #endregion



    void Update()
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {

            collider2Ds = Physics2D.OverlapCircleAll(this.transform.position, radiusOfDectection, layerMask);
            // -- Misc Test ---
            if (collider2Ds.Length > 0)
            {

                float dotElement = 10f;

                foreach (Collider2D element in collider2Ds)
                {
                   
                    float h = Input.GetAxis("Horizontal");
                    float v = Input.GetAxis("Vertical");
                    Vector3 dir = new Vector3(h, v, 0);
                    if (dir == Vector3.zero) dir = Vector3.up;
                    Vector3 objetDir = element.transform.position - transform.position;
                    float newDot = Vector3.Dot(dir, objetDir);
                    if (newDot < dotElement)
                    {
                        dotElement = newDot;
                        objectTarget = element;
                    }
                }

                objectTarget.GetComponent<Object_Interact>().ActiveInteract();

            }
            else
            {
                objectTarget = null;
            }

        }
    }
}
