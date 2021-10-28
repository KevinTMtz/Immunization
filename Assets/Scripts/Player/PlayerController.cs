using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;


public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{

    public Transform cameraTransform;

    public float playerSpeed = 5.0f;
    public float speed = 0.0f;
    float verticalSpeed = 0.0f;

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

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
            speed = 0;
            Vector3 movement = Vector3.zero;

            movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (movement.sqrMagnitude > 1.0f)
                movement.Normalize();

            movement = movement * playerSpeed * Time.deltaTime;

            movement = transform.TransformDirection(movement);
            characterController.Move(movement);

            verticalSpeed = verticalSpeed - 10.0f * Time.deltaTime;
            if (verticalSpeed < -10.0f) verticalSpeed = -10.0f;
            
            var verticalMove = new Vector3(0, verticalSpeed * Time.deltaTime, 0);
            var flag = characterController.Move(verticalMove);
            
            if ((flag & CollisionFlags.Below) != 0) verticalSpeed = 0;
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
