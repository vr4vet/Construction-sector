using System.Collections;
using UnityEngine;

public class StairScript : MonoBehaviour
{
    [SerializeReference] GameObject bottomCoord, topCoord;
    [SerializeReference] CharacterController _charControl;
    [SerializeReference] GameObject _Player;
    bool wentUp;





   

    public void Initiate()
    {
        StartCoroutine(TeleportWithCooldown());
    }
  
    bool cooldown;
    IEnumerator TeleportWithCooldown()
    {

        yield return new WaitForEndOfFrame();
        Teleport();


        cooldown = false;
        yield return new WaitForSecondsRealtime(2f);

        cooldown = true;

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

}
