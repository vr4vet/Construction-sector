using System.Collections.Generic;
using UnityEngine;

public class VaporBarrierSegment : MonoBehaviour
{
    [SerializeReference] List<VaporBarrierStapleSpot> staples = new();
    [SerializeReference] public VaporBarrierManager _manager;
    [SerializeReference] Renderer _rend;
    public int stapleAreaCount
    {
        get
        {
            return staples.Count;
        }
    }
    public int stapleAreaStapledCount
    {
        get
        {
            int b = 0;
            foreach (var item in staples)
            {
                if (item.Stapled)
                {
                    b++;
                }
            }
            return b;
        }
    }

    public int stapleAreaCompleteCount
    {
        get
        {
            int b = 0;
            foreach (var item in staples)
            {
                if (item.Stapled && item.Taped)
                {
                    b++;
                }
            }
            return b;
        }
    }


    

    public bool isRolled
    {
        get
        {
            return _rend.enabled;
        }
    }

    public bool isStapled
    {
        get
        {
            bool stapled = true;
            foreach (var item in staples)
            {
                if (!item.Stapled)
                {
                    stapled = false;
                }
            }
            return stapled;
        }
    }

    public bool isTaped
    {
        get
        {
            bool taped = true;
            foreach (var item in staples)
            {
                if (!item.Taped)
                {
                    taped = false;
                }
            }
            return taped;
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

    public bool _CanAdvance
    {
        get
        {
            if (stapleAreaStapledCount >= stapleAreaCount)
            {
                return true;
            }
            else return false;
        }
    }

    void TryAdvance()
    {
        if (_CanAdvance && !_rend.enabled)
        {
            _rend.enabled = true;
            foreach (var item in staples)
            {
                item.ShowStapleArea();
            }
            _manager.OnDrag(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("VaporBarrier")) //when we drag the foil on the thing
        {
            TryAdvance();
        }
    }

}
