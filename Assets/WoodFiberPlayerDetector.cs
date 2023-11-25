using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFiberPlayerDetector : MonoBehaviour
{
    [SerializeReference]WoodFibreAdjustment adj;
    GameObject firsthandin; //because there are a lot of things called Player, and we don't want to disable the player's ability to interact just because their other hand went out of the radius
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && firsthandin == null)
        {
            firsthandin = other.gameObject;
            adj.PlayerEntered();
            
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && firsthandin == other.gameObject)
        {
            firsthandin = null;
            adj.PlayerLeft();
        }
    }
}