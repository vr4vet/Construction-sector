using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionBlock : MonoBehaviour
{
    [HideInInspector] public ConstructionBlockManager managerRef;
    MeshRenderer mRend;
    [SerializeReference]List<ConstructionBlock> prerequisite = new();
    [HideInInspector]  List<ConstructionBlock> prerequisiteOf = new();
    [HideInInspector]public bool placed;

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
