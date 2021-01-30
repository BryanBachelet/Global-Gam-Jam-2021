using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using System;

namespace GuerhoubaGame
{

    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        #region Private Fields
        
        bool isFiring;

        #endregion


        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;



        #region IPunObservable implementation

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(isFiring);
                stream.SendNext(Health);
            }
            else
            {
                this.isFiring = (bool)stream.ReceiveNext();
                this.Health = (float)stream.ReceiveNext();
            }
        }

        #endregion 

        #region Public Fields

        [Tooltip("The current Health of our Player")]
        public float Health = 1f;

        #endregion


        #region MonoBehaviour Callbacks

        private void Awake()
        {
            if (photonView.IsMine)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
            }
            DontDestroyOnLoad(this.gameObject);

           
        }

        private void Start()
        {
           
#if UNITY_5_4_OR_NEWER
            // Unity 5.4 has a new scene management. register a method to call CalledOnLevelWasLoaded.
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
#endif

        }

        // Update is called once per frame
        void Update()

        {
            if (photonView.IsMine)
            {

                ProcessInputs();
                if (Health <= 0f)
                {
                    GameManager.gameManager.LeaveRoom();
                }

            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }
            if (!other.name.Contains("Beam"))
            {
                return;
            }
            Health -= 0.1f;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }
            if (!other.name.Contains("Beam"))
            {
                return;
            }
            Health -= 0.1f * Time.deltaTime;
        }

#if !UNITY_5_4_OR_NEWER
    // <summary>See CalledOnLevelWasLoaded. Outdated in Unity 5.4.</summary>
    
        void OnLevelWasLoaded(int level)
    {
    this.CalledOnLevelWasLoaded(level);
    }

#endif


        void CalledOnLevelWasLoaded(int level)
        {
            // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
            if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
            {
                transform.position = new Vector3(0f, 5f, 0f);
            }
        }

        public override void OnDisable()
        {
            // Always call the base to remove callbacks
            base.OnDisable();
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        #endregion

        #region Custom

#if UNITY_5_4_OR_NEWER

        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
        {
            this.CalledOnLevelWasLoaded(scene.buildIndex);
        }

#endif

        private void ProcessInputs()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!isFiring)
                {
                    isFiring = true;
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                if (isFiring)
                {
                    isFiring = false;
                }
            }
        }
        #endregion
    }
}
