using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporFoilRollSocket : MonoBehaviour
{




    bool placed;
    [SerializeField] public MeshRenderer _rend;
    [SerializeField] public GameObject foil_segments_parent; //we place this when the block is placed.




  


    //[SerializeField] public Material m_Transparent;
    GameObject heldObject = null;
    bool hoveredOn;

    [SerializeField] public VaporBarrierManager _manager;
    MeshRenderer mRend;



    
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("VaporBarrier") && !placed)
        {
            //Debug.Log("Object with name " + other.gameObject.name + " entered the trigger of object " + gameObject.name);
            Grabbable other_GRABBABLE = other.GetComponent<Grabbable>();
            ConstructionObject other_CONSTRUCTIONOBJECT = other.GetComponent<ConstructionObject>();

            ClearInhandObject(other_CONSTRUCTIONOBJECT, other_GRABBABLE);
            Destroy(other.gameObject);
            foil_segments_parent.SetActive(true);
           
          
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
            _manager.ActivateFoilDragging();
        }
    }



}
