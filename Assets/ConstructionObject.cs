using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionObject : MonoBehaviour
{
    [SerializeField] public ConstructionObjectType _ObjectType;
    [SerializeReference] Grabbable _grabRef; //check if held by the bool _BeingHeld
    [SerializeReference] public Rigidbody _rbody;
    [HideInInspector] public ConstructionObjectSocket _heldby;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

   
}
