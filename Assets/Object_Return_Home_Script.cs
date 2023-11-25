using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Return_Home_Script : MonoBehaviour
{
    private Vector3 startPos;
    private Rigidbody rbody;
   public enum ReturningScriptType
    {
        returningPlane,
        objectInstance
    }
    public ReturningScriptType type;

    // Start is called before the first frame update
    void Start()
    {
        if (type == ReturningScriptType.objectInstance)
        {
            startPos = transform.position;
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        Object_Return_Home_Script g = other.gameObject.GetComponent<Object_Return_Home_Script>();
        if (g != null)
        {
            if (g.enabled)
            {
                g.ResetPosition();

            }
        }
    }

    public void ResetPosition()
    {
        Grabbable b = gameObject.GetComponent<Grabbable>();
        if (b != null)
        {
            if (b.BeingHeld)
            {
                return;
            }
        }
        if (rbody != null)
        {
            rbody.velocity = Vector3.zero;
            bool initialState = rbody.isKinematic;
            rbody.isKinematic = true;
            rbody.position = startPos;
            rbody.isKinematic = initialState;

        }
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
