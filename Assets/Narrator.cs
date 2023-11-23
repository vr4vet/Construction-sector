using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Narrator : MonoBehaviour
{

    [SerializeReference]TextMeshProUGUI txt_narration;
    [SerializeReference] float timeBetweenLetters;
    [SerializeReference] AudioClip notification;

    public void Narrate(string text)
    {
        StopAllCoroutines();
        StartCoroutine(narrate(text));
    }

    IEnumerator narrate(string text)
    {
        if (notification!= null)
        {
            SoundManager.Instance.PlayEffect(notification);

        }
        txt_narration.text = "";
        foreach (char item in text)
        {
            txt_narration.text = txt_narration.text + item;
            yield return new WaitForSecondsRealtime(timeBetweenLetters);
        }
    }
}
