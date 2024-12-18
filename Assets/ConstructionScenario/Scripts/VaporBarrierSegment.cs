using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporBarrierSegment : MonoBehaviour
{
    [SerializeReference] public VaporBarrierManager _manager;
    [SerializeReference] Renderer _rend;

    int? ourIndex;
    void SetIndex()
    {


    }

    public bool isRolled
    {
        get
        {
            Debug.Log(gameObject.name + " is rolled: " + _rend.enabled.ToString());
            return _rend.enabled;
        }
    }
    public bool isPrerequisiteRolled
    {
        get
        {
            if (ourIndex == null)
            {
                ourIndex = _manager.GetIndexOfSegment(this);
                Debug.Log("Our index is " + ourIndex.ToString());
                if (ourIndex == null)
                {
                    Debug.LogError("Unregistered vapor foil segment  = " + gameObject.name);
                }
            }
            if (ourIndex <= _manager.segmentsDone)
            {
                return true;
            }
            return true;

        }
    }
   



    void Start()
    {

        if (_rend == null)
        {
            _rend = gameObject.GetComponent<MeshRenderer>();
        }
        _rend.enabled = false;
    }

    void TryAdvance()
    {
        if (isPrerequisiteRolled && !_rend.enabled)
        {
            _rend.enabled = true;
            _manager.CheckIfDone();
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if ((other.tag == "VaporObject") && !isRolled)
        
            TryAdvance();
        }




}
