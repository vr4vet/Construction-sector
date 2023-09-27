using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StapleTracker : MonoBehaviour
{
    [SerializeField] List<StapleSpot> stapleSpots = new();
    [SerializeField] List<WrappingTear> tears = new();
    [SerializeField] ConstructionManager _manager;



    [SerializeField] List<GameObject> hideOnStart = new();
    void Start()
    {
        foreach (var item in hideOnStart)
        {
            item.SetActive(false);
        }
    }
    public void CheckForCompletion()
    {


        bool allStapled = true;
        foreach (StapleSpot item in stapleSpots)
        {
            if (!item.stapled)
            {
                allStapled = false;
            }
        }

        if (tears.Count < 1 && allStapled)
        {
            Debug.LogWarning("Finished SubTask 2.");
            _manager.HasFinishedSubtask(ConstructionManager.SubTaskEnum.TWO);
        }
    }
}
