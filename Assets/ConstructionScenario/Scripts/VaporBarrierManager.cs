using BNG;
using System.Collections.Generic;
using UnityEngine;

public class VaporBarrierManager : MonoBehaviour
{
    [SerializeField] public ConstructionManager.SubTaskEnum RelatedSubTask;

    [SerializeField] public ConstructionManager _manager;
    [SerializeField] List<VaporBarrierSegment> segmentObjects = new();

    int tapes = 0;
    int tapesTarget;

    int segmentsTotal
    {
        get { return segmentObjects.Count; }
    }


   public int segmentsDone
    {
        get
        {
            int b = 0; foreach (var item in segmentObjects)
            {
                if (item.isRolled)
                {
                    b++;
                }
                else
                {
                    return b;
                }
            }
            return b;
        }
    }

    bool isDone
    {
        get
        {
            Debug.Log($"Segments Done: {segmentsDone}/{segmentsTotal}");
            return segmentsDone >= segmentsTotal;
        }
    }

    

    void Update()
    {
    }

    void Start()
    {
    }


    public int? GetIndexOfSegment(VaporBarrierSegment segment)
    {
        try
        {
            int index = segmentObjects.IndexOf(segment);
            if (index >= 0)
            {
                return index;
            }
            else
            {
                // Segment not found
                return null;
            }
        }
        catch (System.Exception)
        {
            return null;
        }
    }

   
    public void CheckIfDone()
    {
        if (isDone)
        {
            _manager.HasFinishedSubtask(RelatedSubTask);
        }
    }

}
