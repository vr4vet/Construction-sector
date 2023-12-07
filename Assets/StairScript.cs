using BNG;
using System.Collections;
using UnityEngine;

public class StairScript : MonoBehaviour
{
    [SerializeReference] GameObject bottomCoord, topCoord;
    [SerializeReference] CharacterController _charControl;
    [SerializeReference] PlayerTeleport _Player;
    bool wentUp;




    bool canInteract = true;
    IEnumerator delayNextInteraction()
    {

        yield return new WaitForEndOfFrame();
        Teleport();
        yield return new WaitForSecondsRealtime(2f);

        canInteract = true;
    }




    void Teleport()
    {
        _charControl.enabled = false;
        if (wentUp)
        {
            _Player.TeleportPlayerToTransform(bottomCoord.transform);

        }
        else
        {
            _Player.TeleportPlayerToTransform(topCoord.transform);


        }
        wentUp = !wentUp;
        _charControl.enabled = true;
        canInteract = false;

    }
    public void OnTrigger(Collider other)
    {

        if (canInteract)
        {
            StartCoroutine(delayNextInteraction());
        }
    }
}
