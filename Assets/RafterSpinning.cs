using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RafterSpinning : MonoBehaviour
{
    [SerializeReference] GameObject rafter;
    [SerializeReference] BNG.Grabbable _Grabbable;
    public bool left;
    public float speed;

    public RafterSpinning other;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        Teleport();
    //    }
    //}


    public void Disable()
    {
        toggled = false;
    }
    private void Update()
    {
        if (toggled)
        {
            Spin();
        }
    }
    bool toggled;
    public void Rotate()
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

        rafter.transform.Rotate(0.0f, realSpeed * Time.deltaTime, 0.0f, Space.World);
    }
   public void Spin()
    {
        other.Disable();
        toggled = !toggled;
    }
}
