using UnityEngine;

public class Interacter : MonoBehaviour
{
    float raycastDistance = 3; //Adjust to suit your use case

    //obsolete nonVR implementation
    //void Update()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // This creates a 'ray' at the Main Camera's Centre Point essentially the centre of the users Screen

    //    RaycastHit hit; //This creates a Hit which is used to callback the object that was hit by the Raycast

    //    if (Physics.Raycast(ray, out hit, raycastDistance)) //Actively creates a ray using the above set perameters at the predeterminded distance
    //    {



    //        if (Input.GetKeyDown(KeyCode.E))//Check if the player has pressed the Interaction button
    //        {
    //            Debug.Log("[E] was pressed while looking at a block.");

    //            //Add your Note Method/UI here - for example (if the note has a script attached to it)

    //            ConstructionObjectSocket script = hit.collider.GetComponent<ConstructionObjectSocket>();
    //            if (script != null)
    //            {
    //                script.OnInteraction();
    //            }
    //        }

    //    }
    //}
}
