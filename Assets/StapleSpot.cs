using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StapleSpot : MonoBehaviour
{
    [HideInInspector] public bool stapled;
    [SerializeReference] public bool invalid;

    [SerializeReference] StapleTracker _tracker;
    
  
    private void OnTriggerEnter(Collider other)
    {
        var collisionPoint = other.ClosestPoint(transform.position);
        if (other.tag == "Stapler")
        {
            if (invalid)
            {
                Instantiate(ConstructionManager.Instance.S2_Prefab_WrapRipDecal, collisionPoint, Quaternion.identity);


                return;
            }
            stapled = true;
            _tracker.CheckForCompletion();
            Instantiate(ConstructionManager.Instance.S2_Prefab_Staple, collisionPoint, Quaternion.identity);
            //detect skill step here
        }
    }
}
