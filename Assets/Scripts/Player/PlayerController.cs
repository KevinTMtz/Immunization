using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;


public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{

    public Transform cameraTransform;

    void Start()
    {
        if (!photonView.IsMine)
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // TODO: Add player and camera movement
        }
    }

    // TODO: Sync variables
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // TODO: Add all variables we want to send
            //stream.SendNext(variable);
        }
        else
        {
            // TODO: Set all variables received and cast them
            //variable = stream.ReceiveNext();
        }
    }
}
