using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporBarrierStapleSpot : MonoBehaviour
{
   [HideInInspector] public bool Stapled;
    [HideInInspector] public bool Taped;

    public GameObject objStaple, objTape, greenViableStaplingAreaObject;
    [SerializeReference] public VaporBarrierManager _manager;


    void Start()
    {
        greenViableStaplingAreaObject.SetActive(false);
    }

    public void ShowStapleArea()
    {
        greenViableStaplingAreaObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Stapler") && !Stapled)
        {
            Stapled = true;
            objStaple.SetActive(true);
            greenViableStaplingAreaObject.SetActive(false);
            _manager.OnStaple(this);
        }


        if (other.CompareTag("Duct Tape") && !Taped && Stapled)
        {
            Taped = true;
            objTape.SetActive(true);
            _manager.OnTape(this);
        }
    }
}
