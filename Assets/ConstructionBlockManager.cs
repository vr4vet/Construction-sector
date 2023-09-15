using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionBlockManager : MonoBehaviour
{

    [SerializeField] List<ConstructionBlock> blocks = new();
    [SerializeField] public Material m_Transparent;
    [SerializeField] public Material m_Normal;



    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in blocks)
        {
            item.managerRef = this;
            item.ChangeMat(m_Transparent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
