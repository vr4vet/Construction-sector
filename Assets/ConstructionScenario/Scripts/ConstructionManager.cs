using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{

    public static ConstructionManager Instance { get; private set; }
    public Material placeableMat, unplaceableMat;
    public List<GameObject> currentSubtaskObjects = new();//to wipe when we switch subtask
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
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _subtask = SubTaskEnum.T1S0;
            T1S0_ToggleElementVisibility(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _subtask = SubTaskEnum.T1S1;
            T1S3_ToggleElementVisibility(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _subtask = SubTaskEnum.T1S2;
            T1S2_ToggleElementVisibility(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _subtask = SubTaskEnum.T1S3;
            T1S4_ToggleElementVisibility(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _subtask = SubTaskEnum.T1S4;
            T1S3_ToggleElementVisibility(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _subtask = SubTaskEnum.T2S1;
            T2S1_ToggleElementVisibility(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _subtask = SubTaskEnum.T2S2;
            T2S2_ToggleElementVisibility(true);
        }

    }

    public enum SubTaskEnum
    {
        T1S0,
        T1S1,
        T1S2,
        T1S3,
        T1S4,
        T2S1, //technically t2s1
        T2S2//technically t2s2
    }


    public SubTaskEnum _subtask = SubTaskEnum.T1S0;
    public GameObject S2_Prefab_WrapRipDecal;
    public GameObject S2_Prefab_Staple;
    public int max_rips;



    public GameObject T1S0_VisibleElements;
    public GameObject T1S1_VisibleElements;
    public GameObject T1S2_VisibleElements;
    public GameObject T1S3_VisibleElements;
    public GameObject T1S4_VisibleElements;
    public GameObject T2S1_VisibleElements;
    public GameObject T2S2_VisibleElements;

    public void T1S2_HasApproachedFrameWithRoll()
    {//trigger

        //tell player to wrap roll on frame
    }
    void T1S0_ToggleElementVisibility(bool visible)
    {
        T1S0_VisibleElements.SetActive(visible);
        T1S1_VisibleElements.SetActive(!visible);
        T1S2_VisibleElements.SetActive(!visible);
        T1S3_VisibleElements.SetActive(!visible);
        T1S4_VisibleElements.SetActive(!visible);
        T2S1_VisibleElements.SetActive(!visible);
    }
    void T1S1_ToggleElementVisibility(bool visible)
    {
        T1S0_VisibleElements.SetActive(!visible);
        T1S1_VisibleElements.SetActive(visible);
        T1S2_VisibleElements.SetActive(!visible);
        T1S3_VisibleElements.SetActive(!visible);
        T1S4_VisibleElements.SetActive(!visible);
        T2S1_VisibleElements.SetActive(!visible);
    }

    void T1S2_ToggleElementVisibility(bool visible)
    {
        T1S0_VisibleElements.SetActive(!visible);
        T1S1_VisibleElements.SetActive(!visible);
        T1S2_VisibleElements.SetActive(visible);
        T1S3_VisibleElements.SetActive(!visible);
        T1S4_VisibleElements.SetActive(!visible);
        T2S1_VisibleElements.SetActive(!visible);
    }
    void T1S3_ToggleElementVisibility(bool visible)
    {
        T1S0_VisibleElements.SetActive(!visible);
        T1S1_VisibleElements.SetActive(!visible);
        T1S2_VisibleElements.SetActive(!visible);
        T1S3_VisibleElements.SetActive(visible);
        T1S4_VisibleElements.SetActive(!visible);
        T2S1_VisibleElements.SetActive(!visible);
    }
    void T1S4_ToggleElementVisibility(bool visible)
    {
        T1S0_VisibleElements.SetActive(!visible);
        T1S1_VisibleElements.SetActive(!visible);
        T1S2_VisibleElements.SetActive(!visible);
        T1S3_VisibleElements.SetActive(!visible);
        T1S4_VisibleElements.SetActive(visible);
        T2S1_VisibleElements.SetActive(!visible);
    }
    void T2S1_ToggleElementVisibility(bool visible)
    {
        T1S0_VisibleElements.SetActive(!visible);
        T1S1_VisibleElements.SetActive(!visible);
        T1S2_VisibleElements.SetActive(!visible);
        T1S3_VisibleElements.SetActive(!visible);
        T2S1_VisibleElements.SetActive(visible);
        T2S2_VisibleElements.SetActive(!visible);
    }
    void T2S2_ToggleElementVisibility(bool visible)
    {
        T1S0_VisibleElements.SetActive(!visible);
        T1S1_VisibleElements.SetActive(!visible);
        T1S2_VisibleElements.SetActive(!visible);
        T1S3_VisibleElements.SetActive(!visible);
        T2S1_VisibleElements.SetActive(!visible);
        T2S2_VisibleElements.SetActive(visible);
    }


    public void HasFinishedSubtask(SubTaskEnum stask)
    {
        foreach (var item in currentSubtaskObjects)
        {
            Destroy(item);
        }
        currentSubtaskObjects.Clear();

        Debug.LogWarning("Player has finished subtask" + stask);
        _subtask = stask + 1;
        switch (_subtask)
        {
            case SubTaskEnum.T1S0:
                T1S0_ToggleElementVisibility(true);
                break;
            case SubTaskEnum.T1S1:
                T1S1_ToggleElementVisibility(true);
                break;
            case SubTaskEnum.T1S2:
                T1S2_ToggleElementVisibility(true);
                break;
            case SubTaskEnum.T1S3:
                T1S3_ToggleElementVisibility(true);
                break;
            case SubTaskEnum.T1S4:
                T1S4_ToggleElementVisibility(true);
                break;
            case SubTaskEnum.T2S1:
                T2S1_ToggleElementVisibility(true);
                break;
            case SubTaskEnum.T2S2:
                T2S2_ToggleElementVisibility(true);
                break;
            default:
                break;

        }
    }



    void Start()
    {
        _subtask = SubTaskEnum.T1S0;

        T1S0_ToggleElementVisibility(true);
    }
}
