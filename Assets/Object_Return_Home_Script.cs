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
        if (other.gameObject.GetComponent<Object_Return_Home_Script>() != null)
        {
            other.gameObject.GetComponent<Object_Return_Home_Script>().ResetPosition();
        }
    }

    public void ResetPosition()
    {
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
