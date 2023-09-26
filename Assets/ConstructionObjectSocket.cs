using BNG;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionObjectSocket : MonoBehaviour
{

    //state of the current block
    public enum blockState
    {
        unplaceable,
        placeable,
        hovered,
        placed,
    }

    [HideInInspector] public blockState _state = blockState.placeable;
    [HideInInspector] public MeshRenderer _rend;
    [SerializeField] public ConstructionObjectType _requiredType;
    //[SerializeField] public Material m_Transparent;
    GameObject heldObject = null;
    bool hoveredOn;

    [HideInInspector] public ConstructionBlockManager managerRef;
    MeshRenderer mRend;
    [SerializeReference] List<ConstructionObjectSocket> prerequisites = new();
    [HideInInspector] List<ConstructionObjectSocket> prerequisiteOf = new();



    void Start()
    {
        _rend = GetComponent<MeshRenderer>();
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Plank")
        {
            Debug.Log("Object with name " + other.gameObject.name + " entered the trigger of object " + gameObject.name);
            Grabbable otherGrab = other.GetComponent<Grabbable>();
            ConstructionObject otherObjectData = other.GetComponent<ConstructionObject>();
            if (otherGrab == null)
            {
                Debug.Log("otherGrab was null on item frame enter, this should not happen, wrong object is tagged with Plank");

                return;
            }
            if (otherObjectData == null)
            {
                Debug.Log("otherObjectData was null on item frame enter, this should not happen, wrong object is tagged with Plank");
                return;
            }

            if (heldObject == null)
            {
                Debug.LogWarning("heldObject was null. we can hold the item.");
            }
            else
            {
                Debug.LogWarning("heldObject occuppied. we cannt hold the item. held item name is " + heldObject.name);

            }
           
            
            if ( otherObjectData._ObjectType == _requiredType && heldObject == null)
            {
                if (otherObjectData._heldby != null)
                {
                    otherObjectData._heldby.heldObject = null; //clears previous holder
                    otherObjectData._heldby = null;
                    return;
                }
                if (TryPlace())
                {
                    if (otherGrab.BeingHeld)
                    {
                        otherGrab.DropItem(otherGrab.GetPrimaryGrabber());
                    }
                    otherObjectData._heldby = this;
                    SnapTargetToPlace(other.gameObject);
                    heldObject = other.gameObject;
                    otherGrab.enabled = false;
                }

            }
        }
    }

    bool TryPlace()
    {
        InitiateStructuralCompletionCheck();

        foreach (var item in prerequisites)
        {
            if (item._state != blockState.placed)
            {
                return false;
            }
        }
        _state = blockState.placed;
        return true;
    }

    void RefreshVisibility()
    {
        bool visible = true; //wether we can see this socket
        foreach (var item in prerequisites)
        {
            if (item._state != blockState.placed)
            {
                visible = false;
            }
        }
        if (visible)
        {
            _rend.enabled = true;
        }
        else
        {
            _rend.enabled = false;
        }

        if (_state == blockState.placed)
        {
            _rend.enabled = false;
        }
    }

    void SnapTargetToPlace(GameObject b)
    {
        Rigidbody target = b.GetComponent<Rigidbody>();
        if (target == null)
        {
            Debug.Log("SnapTargetToPlace - target has no rigidbody. Should not happen.");
            return;
        }

        target.isKinematic = true;
        target.useGravity = false;

        b.transform.position = this.transform.position;
        b.transform.rotation = this.transform.rotation;
    }



    public void InitiateStructuralCompletionCheck()
    {
        managerRef.InitiateCheck();
    }

    public bool StructuralCompletionCheck()
    {
        RefreshVisibility();
        if (_state == blockState.placed)
        {
            return true;
        }
        else return false;
    }
}

public enum ConstructionObjectType
{
    stillbeam,
    stud,
    brace,
    wallbeam,
    sheet,
}