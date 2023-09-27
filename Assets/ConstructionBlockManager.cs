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
        foreach (var item in blocks)
        {


            if (!item.StructuralCompletionCheck())
            {

                complete = false;
            }
        }
        if (complete && FinishesSubtaskWhenDone)
        {
            _manager.HasFinishedSubtask(RelatedSubTask);
        }
    }

   
}
