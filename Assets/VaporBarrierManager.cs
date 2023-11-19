using BNG;
using System.Collections.Generic;
using UnityEngine;

public class VaporBarrierManager : MonoBehaviour
{
    [SerializeField] public ConstructionManager.SubTaskEnum RelatedSubTask;

    [SerializeField] public ConstructionManager _manager;
    [SerializeField] public float _RollRadiusDecrementPerSegment = 0.1f;


    [SerializeReference] public Rigidbody DraggableFoilObject; //we enable this when we put the foil on the frame 

    public GameObject pos_Start, pos_End;
    public float movementIncrement = 0.01f;
    private float currentMovementStatus = 0;

    int segmentsTotal
    {
        get { return segmentObjects.Count; }
    }
    int segmentsStapled
    {
        get
        {
            int b = 0; foreach (var item in segmentObjects)
            {
                if (item.isStapled && item.isRolled)
                {
                    b++;
                }
            }
            return b;
        }
    }
    int segmentsDone
    {
        get
        {
            int b = 0; foreach (var item in segmentObjects)
            {
                if (item.isStapled && item.isTaped && item.isRolled)
                {
                    b++;
                }
            }
            return b;
        }
    }
    int segmentsRolled
    {
        get
        {
            int b = 0; foreach (var item in segmentObjects)
            {
                if (item.isRolled)
                {
                    b++;
                }
            }
            return b;
        }
    }
    int staplesTarget
    {
        get
        {
            int b = 0;
            foreach (var item in segmentObjects)
            {
                b += item.stapleAreaCount;
            }
            return b;
        }
    }


    [SerializeField] List<VaporBarrierSegment> segmentObjects = new();

    [SerializeField] List<VaporBarrierStapleSpot> stapleAreas = new();
    int tapes = 0;
    int tapesTarget;


    bool canDrag
    {
        get
        {
            if (segmentsStapled >= segmentsRolled)
            {
                return true;
            }
            return false;
        }
    }
    bool isDone
    {
        get
        {
            if (segmentsDone >= segmentsTotal) //we need all the staples taped too.
            {
                return true;
            }
            return false;
        }
    }

    bool isFlattening;
    public void StartedFlattening()
    {
        isFlattening = true;
    }

    public void StoppedFlattening()
    {
        isFlattening = false;
    }
    void Update()
    {
        if (isFlattening && DraggableFoilObject.gameObject.activeInHierarchy)
        {


            if (DraggableFoilObject.transform.position.x <= 2.413)
            {
                Debug.LogWarning("PRESSED");
                DraggableFoilObject.transform.position = new Vector3((DraggableFoilObject.transform.position.x + movementIncrement), DraggableFoilObject.transform.position.y, DraggableFoilObject.transform.position.z);
            }

        }
    }

    void Start()
    {
        DraggableFoilObject.gameObject.SetActive(false);
    }
    public void OnDrag(VaporBarrierSegment segment)
    {
        //nothing happens here. 
    }
    public void OnStaple(VaporBarrierStapleSpot segment)
    {
        //nothing happens here. 
    }

    public void OnTape(VaporBarrierStapleSpot segment)
    {
        CheckIfDone();
    }

    public void ActivateFoilDragging()
    {
        DraggableFoilObject.gameObject.SetActive(true);
    }
    public void CheckIfDone()
    {
        if (isDone)
        {
            Debug.LogWarning("We finished the subtask.");
            _manager.HasFinishedSubtask(RelatedSubTask);
        }
    }

}
