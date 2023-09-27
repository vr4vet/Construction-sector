using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{

    public static ConstructionManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            S2_ToggleElementVisibility(true);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            S1_ToggleElementVisibility(true);
        }
    }

    public enum SubTaskEnum
    {
        ONE,
        TWO,
        THREE,
        FOUR
    }

   
    public SubTaskEnum _subtask = SubTaskEnum.ONE;
    public GameObject S2_Prefab_WrapRipDecal;
    public GameObject S2_Prefab_Staple;
    public int max_rips;

    


    public GameObject S1_VisibleElements;
    public GameObject S2_VisibleElements;
    public GameObject S3_VisibleElements;
    public GameObject S4_VisibleElements;

    public void S2_HasApproachedFrameWithRoll()
    {

        //tell player to wrap roll on frame
    }
     void S1_ToggleElementVisibility(bool visible)
    {
        S1_VisibleElements.SetActive(visible);
        S2_VisibleElements.SetActive(!visible);
        S3_VisibleElements.SetActive(!visible);
        S4_VisibleElements.SetActive(!visible);
    }

     void S2_ToggleElementVisibility(bool visible)
    {
        S1_VisibleElements.SetActive(!visible);
        S2_VisibleElements.SetActive(visible);
        S3_VisibleElements.SetActive(!visible);
        S4_VisibleElements.SetActive(!visible);
    }
     void S3_ToggleElementVisibility(bool visible)
    {
        S1_VisibleElements.SetActive(!visible);
        S2_VisibleElements.SetActive(!visible);
        S3_VisibleElements.SetActive(visible);
        S4_VisibleElements.SetActive(!visible);
    }
     void S4_ToggleElementVisibility(bool visible)
    {
        S1_VisibleElements.SetActive(visible);
        S2_VisibleElements.SetActive(!visible);
        S3_VisibleElements.SetActive(!visible);
        S4_VisibleElements.SetActive(visible);
    }

    public void HasFinishedSubtask( SubTaskEnum stask)
    {
        if (stask != SubTaskEnum.FOUR)
        {
            Debug.LogWarning("Player has finished subtask" + stask);
            _subtask = stask + 1;
            switch (_subtask)
            {
                case SubTaskEnum.ONE:
                    S1_ToggleElementVisibility(true);
                    break;
                case SubTaskEnum.TWO:
                    S2_ToggleElementVisibility(true);
                    break;
                case SubTaskEnum.THREE:
                    S3_ToggleElementVisibility(true);
                    break;
                case SubTaskEnum.FOUR:
                    S4_ToggleElementVisibility(true);
                    break;
                default:
                    break;
            }
        }
        else
        {

        //end scene or show end construction
        }
    }



    void Start()
    {
        S2_ToggleElementVisibility(true);
    }
}
