using BNG;
using System.Collections;
using UnityEngine;

public class StairScript : MonoBehaviour
{
    [SerializeReference] GameObject bottomCoord, topCoord;
    [SerializeReference] PlayerTeleport _Player;
    bool wentUp;




    bool canInteract = true;
    IEnumerator delayNextInteraction()
    {

        yield return new WaitForEndOfFrame();
        Teleport();
        canInteract = false;

        yield return new WaitForSecondsRealtime(4f);

        canInteract = true;
    }




    void Teleport()
    {
        if (wentUp)
        {
            _Player.TeleportPlayerToTransform(bottomCoord.transform);

        }
        else
        {
            _Player.TeleportPlayerToTransform(topCoord.transform);


        }
        wentUp = !wentUp;

    }
    public void OnTrigger(Collider other)
    {

        if (canInteract)
        {
            StartCoroutine(delayNextInteraction());
        }
    }
}
