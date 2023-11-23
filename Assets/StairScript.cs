using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairScript : MonoBehaviour
{
    [SerializeReference]GameObject bottomCoord, topCoord;
    [SerializeReference] CharacterController _charControl;
    [SerializeReference] GameObject _Player;
    [SerializeReference] BNG.Grabbable _Grabber;
    bool wentUp;


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        Teleport();
    //    }
    //}

    private void Update()
    {
        if (_Grabber.BeingHeld && canInteract)
        {
            _Grabber.DropItem(_Grabber.GetClosestGrabber());
            StartCoroutine(delayNextInteraction());
            Teleport(); 
        }
    }
    bool canInteract = true;
    IEnumerator delayNextInteraction()
    {
        canInteract = false;
        yield return new WaitForSecondsRealtime(3f);

        canInteract = true;
    }
    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.tag == "Player")
    //    {
    //        Teleport();
    //    }
    //}



    void Teleport()
    {
        _charControl.enabled = false;
        if (wentUp)
        {
            _Player.transform.position = bottomCoord.transform.position;
        }
        else
        {
            _Player.transform.position = topCoord.transform.position;

        }
        wentUp = !wentUp;
        _charControl.enabled = true;
    }
}
