using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConstructionBlockManager : MonoBehaviour
{
    [SerializeField] public ConstructionManager.SubTaskEnum RelatedSubTask;
    [SerializeField] List<ConstructionObjectSocket> blocks = new();
    [SerializeField] public ConstructionManager _manager;
    [SerializeField] public bool FinishesSubtaskWhenDone = true;

   

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
        Debug.Log("Initiating completion check for " + gameObject.name);
        string s = "checking the following blocks:\n";

        foreach (var item in blocks)
        {
            
           
            if (item._complete)
            {
                s += item.gameObject.name + " was checked. it is complete \n";
                
            }
            else
            {
                s += item.gameObject.name + " was checked. it is incomplete \n";
                complete = false;
            }
            item.RefreshVisibility();
        }
        Debug.Log(s);
        if (complete && FinishesSubtaskWhenDone)
        {
            Debug.LogWarning("We finished the subtask.");
            _manager.HasFinishedSubtask(RelatedSubTask);
        }
    }

   
}
