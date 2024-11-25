using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MoveOnTouch : MonoBehaviour
{
    public Vector3 Endpos;

    private float speed = 2f;
    bool moving = false;




    void Update()
    {
        if (moving && transform.position.y < Endpos.y)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

        }
    }



    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Pusher")
        {
            moving = true;
        }
       // collision.gameObject.SendMessage("Hit");
       
    }

    private void OnTriggerExit(Collider collision)
    {


        if (collision.gameObject.tag == "Pusher")
        {
            moving = false;
        }

    }

}
