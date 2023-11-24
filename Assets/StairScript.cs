using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairScript : MonoBehaviour
{
    [SerializeReference]GameObject bottomCoord, topCoord;
    [SerializeReference] CharacterController _charControl;
    [SerializeReference] GameObject _Player;
    bool wentUp;



   
    bool canInteract = true;
    IEnumerator delayNextInteraction()
    {

        yield return new WaitForEndOfFrame();
        Teleport();


        canInteract = false;
        yield return new WaitForSecondsRealtime(1f);

        canInteract = true;
    }
   



    void Teleport()
    {
        _charControl.enabled = false;
        if (wentUp)
        {
            _Player.transform.position = bottomCoord.transform.position;
        }
        else
        {
            _Player.transform.position = topCoord.transform.position;

        }
        wentUp = !wentUp;
        _charControl.enabled = true;
    }
    public void OnTrigger(Collider other)
    {
        
           
            StartCoroutine(delayNextInteraction());
    }
}
