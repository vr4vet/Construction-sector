using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RafterSpinning : MonoBehaviour
{
    [SerializeReference] GameObject rafter;
    [SerializeReference] RafterSpinning _otherSide;
    public bool left;
    public float speed;
    bool spinning;


    private void Update()
    {
        if (spinning)
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

    public void Deactivate()
    {
        spinning = false;
    }
    public void toggleSpin()
    {
        _otherSide.Deactivate();
        spinning = !spinning;
    }
}
