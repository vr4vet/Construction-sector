using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionBlock : MonoBehaviour
{


    public enum blockState
    {
        unplaceable,
        placeable,
        hovered,
        placed,
    }

   [HideInInspector]public blockState _state = blockState.unplaceable;
    bool hoveredOn;

    [HideInInspector] public ConstructionBlockManager managerRef;
    MeshRenderer mRend;
    [SerializeReference]List<ConstructionBlock> prerequisite = new();
    [HideInInspector]  List<ConstructionBlock> prerequisiteOf = new();


    void Awake()
    {
        mRend = GetComponent<MeshRenderer>();
        if (prerequisite.Count > 0)
        {
            foreach (var item in prerequisite)
            {
                item.prerequisiteOf.Add(this);
            }
        }
        
       
    }

    void RefreshState()
    {
        if (_state == blockState.placed)
        {
            return;
        }



        //hover vs no hover
        if (_state == blockState.placeable && hoveredOn)
        {
            _state = blockState.hovered;
            return;
        }
        else if (_state == blockState.hovered && !hoveredOn)
        {
            _state = blockState.placeable;
            return;
        }

        
        bool allplaced = true;
        foreach (var item in prerequisite)
        {
            if (item._state == blockState.placed)
            {

                allplaced = false;
            }
        }

    }
    public void OnInteraction()
    {
        if (placed)
        {
            return;
        }
        bool allplaced = true;
        foreach (var item in prerequisite)
        {
            if (!item.placed)
            {
               
                allplaced = false;
            }
        }

        if (!placed && allplaced)
        {
            placed = true;
            ChangeMaterialByState();
        }
       
    }

    void ChangeMaterialByState()
    {
        if (placed)
        {
            mRend.material = managerRef.m_Normal;
        }
        else mRend.material = managerRef.m_Transparent;
    }

    public void ChangeMat(Material m)
    {
        mRend.material = m;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
