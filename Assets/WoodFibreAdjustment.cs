using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFibreAdjustment : MonoBehaviour
{
    [SerializeReference] Grabbable _grab;
    [SerializeReference] WoodFibreSocket _socket;
    [SerializeReference] GameObject _text, _textDone, _plankDone;
    bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (!finished)
        //{
        // if (_grab.BeingHeld)
        if (Input.GetKeyDown(KeyCode.X))
        {
            finished = true;
            Finish();
        }
           
            
       // }
    }


    void Finish()
    {
        _grab.enabled = false;
        _text.SetActive(false);
        _textDone.SetActive(true); 
        _plankDone.SetActive(true);
        _socket.Finished();
        gameObject.SetActive(false);
    }
}