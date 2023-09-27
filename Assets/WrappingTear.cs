using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappingTear : MonoBehaviour
{
    public StapleTracker _tracker;

    private void Start()
    {
        _tracker.tears.Add(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Duct Tape")
        {
            _tracker.tears.Remove(this);
            Destroy(gameObject);
        }
    }
}
