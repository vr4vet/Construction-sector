using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConstructionBlockManager : MonoBehaviour
{

    [SerializeField] List<ConstructionObjectSocket> blocks = new();
    [SerializeField] public Material m_Transparent;
    [SerializeField] public Material m_Normal;

    /// <summary>
    /// checks whether we have completed the first construction subtask
    /// </summary>
    /// 

    void Start()
    {
        InitiateCheck();
    }
    public void InitiateCheck()
    {
        bool complete = true;
        foreach (var item in blocks)
        {


            if (!item.StructuralCompletionCheck())
            {

                complete = false;
            }
        }
        if (complete)
        {
            Debug.LogError("Task 1, Subtask 1 has been completed.");
        }
    }

   
}
