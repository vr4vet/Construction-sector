using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerOutofbounds : MonoBehaviour
{

    public GameObject teleportLocation; //where player gets moved if they touch this object
    public CharacterController controller;
    void OnTriggerEnter(Collider colli)
    {
        
            
            controller.enabled = false;

            controller.transform.position = teleportLocation.transform.position;

            controller.enabled = true;
    }
}
