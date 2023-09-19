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
    [SerializeField] public ConstructionObjectType _requiredType;
    GameObject heldObject = null;
    bool hoveredOn;

    [HideInInspector] public ConstructionBlockManager managerRef;
    MeshRenderer mRend;
    [SerializeReference] List<ConstructionObjectSocket> prerequisites = new();
    [HideInInspector] List<ConstructionObjectSocket> prerequisiteOf = new();
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
}

public enum ConstructionObjectType
{
    PLANK1,
    PLANK2,
    PLANK3,
    SHEET,
    MISC,
}