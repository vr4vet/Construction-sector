using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RafterSpinning : MonoBehaviour
{
    [SerializeReference] GameObject rafter;
    [SerializeReference] BNG.Grabbable _Grabbable;
    public bool left;
    public float speed;


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        Teleport();
    //    }
    //}

    private void Update()
    {
        if (_Grabbable.BeingHeld)
        {
            Spin();
        }
    }
   void Spin()
    {
        float realSpeed = speed;
        if (left)
        {
            realSpeed *= 1f;
        }
        else
        {
            realSpeed *= -1f;
        }
        
        rafter.transform.Rotate(0.0f,  realSpeed *Time.deltaTime, 0.0f, Space.World);

    }
}
