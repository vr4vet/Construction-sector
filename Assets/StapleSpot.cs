using UnityEngine;

public class StapleSpot : MonoBehaviour
{
    [HideInInspector] public bool stapled;
    [SerializeReference] public bool invalid;

    [SerializeReference] StapleTracker _tracker;



    private void OnTriggerEnter(Collider other)
    {
        var collisionPoint = other.ClosestPoint(transform.position);
        Debug.Log("Warning - Staple Spot Collided with object with name " + other.gameObject.name);
        if (other.tag == "Stapler")
        {
            if (invalid)
            {
                WrappingTear newTear = Instantiate(ConstructionManager.Instance.S2_Prefab_WrapRipDecal, collisionPoint, Quaternion.identity).GetComponent<WrappingTear>();
                newTear._tracker = _tracker;
                ConstructionManager.Instance.currentSubtaskObjects.Add(newTear.gameObject);

                return;
            }
            stapled = true;
            _tracker.CheckForCompletion();
            GameObject staple = Instantiate(ConstructionManager.Instance.S2_Prefab_Staple, collisionPoint, gameObject.transform.rotation);
            ConstructionManager.Instance.currentSubtaskObjects.Add(staple);

            //detect skill step here
        }
    }
}
