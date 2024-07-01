using BNG;
using Photon.Voice.PUN.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFiberPlayerDetector : MonoBehaviour
{
    [SerializeReference]WoodFibreAdjustment adj;
    GameObject firsthandin; //because there are a lot of things called Player, and we don't want to disable the player's ability to interact just because their other hand went out of the radius
    private Grabbable grab;


    private void Start()
    {
        grab = gameObject.GetComponent<Grabbable>();      
    }


    private void Update()
    {

        if (grab == null) return;
        if (!grab.IsGrabbable())
        {
            adj.Finish();
        }
    }


}
