using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetAxis("Submit") == 1)
        {
            NetworkManager.instance.Play();
        }
    }
}
