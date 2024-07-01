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
    [SerializeField] public bool assumeBlockShape; //if yes, we just snap the used block to the location. otherwise, we enable a FinishedBlock illusory uninteractible graphical object
    [SerializeField] public GameObject FinishedBlock; //we place this when the block is placed.
    [SerializeField] public ConstructionObjectType _requiredType;
    [SerializeField] public bool needsExtraStep;
    [HideInInspector] public bool isActuallyFinished;

    Material originalMat;
    public bool _complete
    {
        get
        {
            if (_state == blockState.placed)
            {
                if (needsExtraStep && !isActuallyFinished)
                {
                    return false;
                }
                return true;
            }
            else return false;
        }
    }


    //[SerializeField] public Material m_Transparent;
    [HideInInspector] public GameObject heldObject = null;
    bool hoveredOn;

    [SerializeField] public ConstructionBlockManager managerRef;
    MeshRenderer mRend;
    [SerializeReference] List<ConstructionObjectSocket> prerequisites = new();
    [HideInInspector] List<ConstructionObjectSocket> prerequisiteOf = new();


    [SerializeField] string RequiredTag;

    void Start()
    {
        if (FinishedBlock != null)
        {
            FinishedBlock.SetActive(false);
           
        }

        InitiateStructuralCompletionCheck();
        RefreshVisibility();
        if (_rend != null)
        {
            originalMat = _rend.material;

        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == RequiredTag && _state != blockState.placed)
        {
            //Debug.Log("Object with name " + other.gameObject.name + " entered the trigger of object " + gameObject.name);
            Grabbable other_GRABBABLE = other.GetComponent<Grabbable>();
            Object_Return_Home_Script returner = other.GetComponent<Object_Return_Home_Script>();
            if (returner != null)
            {
                returner.enabled = false;
            }
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


            if (other_CONSTRUCTIONOBJECT._ObjectType == _requiredType && heldObject == null)
            {

                if (HasNoUnmetPrerequisites())
                {
                    ClearInhandObject(other_CONSTRUCTIONOBJECT, other_GRABBABLE);
                    _rend.enabled = false;
                    if (assumeBlockShape) //wether we can just lock the object to the slot. alternatively, we destroy the placed object and just activate a preset static overlay object
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
                    
                    if (!needsExtraStep) //in case we need to do something else after placing the block before it can be considered completed (like in the case of Wood Fiber insulation.)
                    {
                        _state = blockState.placed;
                        InitiateStructuralCompletionCheck();
                        isActuallyFinished = true;
                    }

                }

            }

          void ClearInhandObject(ConstructionObject cobj, Grabbable cobjGrabbable)
            {
                if (cobj._heldby != null)  //removes the item from player hand
                {
                    cobj._heldby.heldObject = null; //clears previous holder
                    cobj._heldby = null;
                    return;
                }
                if (cobjGrabbable != null)
                {
                    if (cobjGrabbable.BeingHeld)
                    {
                        cobjGrabbable.DropItem(cobjGrabbable.GetPrimaryGrabber());
                        cobjGrabbable.enabled = false;
                    }

                }
            }
        }
    }

    public void Finished()
    {
        isActuallyFinished = true;
        _state = blockState.placed;
        Debug.Log("Finished a wood fibre socketing + adjustment."); //only one that uses this for now.
        managerRef.InitiateCheck();
    }
    /// <summary>
    /// Returns true if the player can place this block (there are no unmet prerequisites)
    /// </summary>
    /// <returns></returns>
    bool HasNoUnmetPrerequisites()
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
        
        return true;
    }

   public void RefreshVisibility()
    {
        if (_rend == null)
        {
            //Debug.LogError("No renderer detected for ConstructionobjectSocket " + gameObject.name);
            return;
        }
        if (!assumeBlockShape && _state == blockState.placed)
        {//if we don't use another block that snaps to this socket, we simply turn the completed block "illusion" active

            FinishedBlock.SetActive(true);
            return;
        }
        switch (_state)
        {
            
            case blockState.placeable:
                _rend.enabled = true;
                bool visible = true; //wether we can see this socket
                foreach (var item in prerequisites)
                {
                    if (item._state != blockState.placed)
                    {//check all prerequisites and if even one isnt full we cant place anything here.
                        visible = false;
                    }
                }

                if (visible)
                {
                    _rend.material = ConstructionManager.Instance.placeableMat;
                }
                else
                {
                    _rend.material = ConstructionManager.Instance.unplaceableMat;
                }
                break;
            case blockState.placed:
                _rend.enabled = false;
                if (assumeBlockShape) //assume shape of used block
                {
                    _rend.enabled = false;
                    //the block gets fixed in place on TryPlace, so no need to do anything else.
                }
                else //or not. then we use the premade FinishedBlock
                {
                    
                    if (FinishedBlock == null)
                    {

                        Debug.LogError(gameObject.name + " - Nullref - FInishedBLock was null but AssumeBlockShape was false, which is a contradiction as you need a block shape to use if you're not going to use the plank you're slotting in.");
                    }
                    FinishedBlock.SetActive(true);
                    _rend.enabled = false;  
                }
                
                break;
            default:
                break;
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

   
}

public enum ConstructionObjectType
{
    stillbeam,
    stud,
    brace,
    wallbeam,
    sheet,
    woodFibre,
    vaporFoil,
    t2s2_RoofTileClamp,
    cornertile,
    normaltile,
    stormclip
}