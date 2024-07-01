using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T1S4_foilFlatter : MonoBehaviour
{

    public VaporBarrierManager _manager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VaporBarrier"))
        {
            _manager.StartedFlattening();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("VaporBarrier"))
        {
            _manager.StoppedFlattening();

        }
    }
}
