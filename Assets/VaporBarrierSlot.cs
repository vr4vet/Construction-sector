using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporBarrierSlot : MonoBehaviour
{
    [SerializeReference]List<VaporBarrierStapleSpot> staples = new();

    [SerializeReference] Material mat_uncovered;
    [SerializeReference] Material mat_covered;
    [SerializeReference] Renderer _rend;

    void Start()
    {
        _rend.material = mat_uncovered;
    }
    bool isCovered;
    public bool _CanAdvance
    {
        get {
            if (staples.Count < 1 && isCovered)
            {
                return true;
            }
            else
            {
                bool FullyStapled = isCovered;
                foreach (var item in staples)
                {
                    if (!item.Stapled)
                    {
                        FullyStapled = false;
                    }
                }
                return FullyStapled;
            }
        } }

    void TryAdvance()
    {
        if (_CanAdvance && !isCovered)
        {
            isCovered = true;
            _rend.material = mat_covered;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plank")) //when we drag the foil on the thing
        {
            TryAdvance();
        }
    }

}
