using UnityEngine;

public class TransferTriggerToParent : MonoBehaviour
{
    [SerializeField] StairScript parent;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            parent.Initiate();
        }
    }
}
