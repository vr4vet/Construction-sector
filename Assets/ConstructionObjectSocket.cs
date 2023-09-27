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
    [SerializeField] public MeshRenderer _rend;
    [SerializeField] public bool AssumeBlockShape; //if yes, we just snap the used block to the location. otherwise, we enable a FinishedBlock illusory uninteractible graphical object
    [SerializeField] public GameObject FinishedBlock;
    [SerializeField] public ConstructionObjectType _requiredType;
    //[SerializeField] public Material m_Transparent;
    GameObject heldObject = null;
    bool hoveredOn;

    [SerializeField] public ConstructionBlockManager managerRef;
    MeshRenderer mRend;
    [SerializeReference] List<ConstructionObjectSocket> prerequisites = new();
    [HideInInspector] List<ConstructionObjectSocket> prerequisiteOf = new();


    [SerializeField] string RequiredTag;

    void Start()
    {
        if (FinishedBlock!= null)
        {
            FinishedBlock.SetActive(false);

        }
    }


    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == RequiredTag && _state != blockState.placed)
        {
            Debug.Log("Object with name " + other.gameObject.name + " entered the trigger of object " + gameObject.name);
            Grabbable other_GRABBABLE = other.GetComponent<Grabbable>();
            ConstructionObject other_CONSTRUCTIONOBJECT = other.GetComponent<ConstructionObject>();
            if (other_GRABBABLE == null)
            {
                Debug.Log("otherGrab was null on item frame enter, this should not happen, wrong object is tagged with" + RequiredTag);

                return;
            }
            if (other_CONSTRUCTIONOBJECT == null)
            {
                Debug.Log("otherObjectData was null on item frame enter, this should not happen, wrong object is tagged with" + RequiredTag);
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
           
            
            if ( other_CONSTRUCTIONOBJECT._ObjectType == _requiredType && heldObject == null)
            {
                if (other_CONSTRUCTIONOBJECT._heldby != null)
                {
                    other_CONSTRUCTIONOBJECT._heldby.heldObject = null; //clears previous holder
                    other_CONSTRUCTIONOBJECT._heldby = null;
                    return;
                }
                if (TryPlace())
                {
                    if (  other_GRABBABLE != null)
                    {
                        if (other_GRABBABLE.BeingHeld)
                        {
                            other_GRABBABLE.DropItem(other_GRABBABLE.GetPrimaryGrabber());
                            other_GRABBABLE.enabled = false;
                        }

                    }


                    if (AssumeBlockShape)
                    {
                        other_CONSTRUCTIONOBJECT._heldby = this;
                        heldObject = other.gameObject;
                        SnapTargetToPlace(other.gameObject);
                    }
                    else
                    {
                        Destroy(other.gameObject);
                        FinishedBlock.SetActive(true);
                    }


                }

            }
        }
    }

    bool TryPlace()
    {
        if (prerequisites == null)
        {
            Debug.Log("prerequisites were null ");
            return true;
        }
        
        if (prerequisites.Count > 0)
        {
            foreach (var item in prerequisites)
            {
                if (item._state != blockState.placed)
                {
                    return false;
                }
            }
        }
       
        _state = blockState.placed;
        InitiateStructuralCompletionCheck();
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
        if (!AssumeBlockShape && _state==blockState.placed)
        {

            FinishedBlock.SetActive(visible);

            return;
        }
        if (_rend == null)
        {
            return;
        }
        if (visible )
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