using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoe : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Call the method in T1S0Manager to check if it's the correct shoe
            T1S0Manager.Ins.CheckIfCorrectShoe(gameObject);
        }
    }
}
