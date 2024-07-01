using System.Collections;
using System.Collections.Generic;
using Task;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{

    public static ConstructionManager Instance { get; private set; }
    public Narrator _narrator
    {
        get
        {
            if (Narr!= null)
            {
                return Narr;
            }
            else
            {
                Narr = gameObject.GetComponent<Narrator>();
                if (Narr == null)
                {
                    Debug.LogError("Error - Missing Narrator on the ConstructionManager object.");
                    return null;
                }
                else return _narrator;
            }
        }
    }
    
    private Narrator Narr;
    public Material placeableMat, unplaceableMat;
    public List<GameObject> _temporarySubtaskObjects = new();//to wipe when we switch subtask
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
   //     if (Input.GetKeyDown(KeyCode.Alpha0))
   //     {
   //         _subtask = SubTaskEnum.ZERO;
   //         SwitchElementVisibility(subtaskObjects[0]);
			// _narrator.Narrate("Task 1, Subtask 0 - Safety equipment!<br>Pick up the correct protective equipment.");
   //         CompleteElement(SubTaskEnum.START);
   //     }
   //     if (Input.GetKeyDown(KeyCode.Alpha1))
   //     {
   //         _subtask = SubTaskEnum.ONE;
   //         SwitchElementVisibility(subtaskObjects[1]);
			//_narrator.Narrate("Task 1, Subtask 1 - Create a wooden frame wall \n Pick up a beam from the table and attach it to the corresponding spot at one of the green outlines.");
   //         CompleteElement(SubTaskEnum.ZERO);
   //     }
   //     if (Input.GetKeyDown(KeyCode.Alpha2))
   //     {
   //         _subtask = SubTaskEnum.TWO;
   //         SwitchElementVisibility(subtaskObjects[2]);
			// _narrator.Narrate("Task 1, Subtask 2 - Attach the housewrap for the outer layer, then staple it. Tape the holes, if any.");
   //         CompleteElement(SubTaskEnum.ONE);//always one less because we dont want the task to be completed...
   //     }
   //     if (Input.GetKeyDown(KeyCode.Alpha3))
   //     {
   //         _subtask = SubTaskEnum.THREE;
   //         SwitchElementVisibility(subtaskObjects[3]);
			// _narrator.Narrate("Task 1, Subtask 3 - Insert wood fiber insulation into the frame, then arrange it neatly into place.");
   //         CompleteElement(SubTaskEnum.TWO);//always one less because we dont want the task to be completed...
   //     }
   //     if (Input.GetKeyDown(KeyCode.Alpha4))
   //     {
   //         _subtask = SubTaskEnum.FOUR;
   //         SwitchElementVisibility(subtaskObjects[4]);
			//_narrator.Narrate("Task 1, Subtask 4 - Apply the vapor foil, stapling and taping it.");
   //         CompleteElement(SubTaskEnum.THREE);//always one less because we dont want the task to be completed...

   //     }
   //     if (Input.GetKeyDown(KeyCode.Alpha5))
   //     {
   //         _subtask = SubTaskEnum.FIVE;
   //         SwitchElementVisibility(subtaskObjects[5]);
			//_narrator.Narrate("Task 2, Subtask 1 - Learn the layers of a roof, then fill out a short quiz.");
   //         CompleteElement(SubTaskEnum.FOUR);//always one less because we dont want the task to be completed...
   //     }

   //     if (Input.GetKeyDown(KeyCode.Alpha6))
   //     {
   //         _subtask = SubTaskEnum.SIX;

   //         SwitchElementVisibility(subtaskObjects[6]);
			//_narrator.Narrate("Task 2, Subtask 2 - Assemble the tiling of a roof.");
   //         CompleteElement(SubTaskEnum.FIVE);//always one less because we dont want the task to be completed...
   //     }
   //     if (Input.GetKeyDown(KeyCode.Alpha7))
   //     {
   //         _subtask = SubTaskEnum.SEVEN;

   //         SwitchElementVisibility(subtaskObjects[6]);
 
   //         CompleteElement(SubTaskEnum.SIX);//always one less because we dont want the task to be completed...

   //     }

    }

    public enum SubTaskEnum
    {
        START,
        ZERO,
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE, //technically t2s1
        SIX,//technically t2s2
            SEVEN
    }


    public SubTaskEnum _subtask = SubTaskEnum.ZERO;
    public GameObject S2_Prefab_WrapRipDecal;
    public GameObject S2_Prefab_Staple;
    public int max_rips;


    public List<GameObject> subtaskObjects = new();

    public GameObject T1S0_VisibleElements;
    public GameObject T1S1_VisibleElements;
    public GameObject T1S2_VisibleElements;
    public GameObject T1S3_VisibleElements;
    public GameObject T1S4_VisibleElements;
    public GameObject T2S1_VisibleElements;
    public GameObject T2S2_VisibleElements;

    IEnumerator narrateT2S1()
    {

        yield return new WaitForSecondsRealtime(4f);
        _narrator.Narrate("Turn around, go to the ladder and touch it, to go upstairs.");
    }


    [Header("Tasks")]
    public Task.Task T1, T2;

    [Header("Subtasks")]
    public Task.Subtask T1_S0_protection_equipment, T1_S1, T1_S2, T1_S3, T1_S4, T2_S1, T2_S2;

    List<Task.Subtask> subtasks;
    public void CompleteElement(SubTaskEnum which)
    {
        subtasks = new List<Task.Subtask>() { T1_S0_protection_equipment, T1_S1, T1_S2, T1_S3, T1_S4, T2_S1, T2_S2 };
        switch (which)
        {
            case SubTaskEnum.START:

                T1_S0_protection_equipment.SetCompleated(false);
                T1_S1.SetCompleated(false);
                T1_S2.SetCompleated(false);
                T1_S3.SetCompleated(false);
                T1_S4.SetCompleated(false);
                T2_S1.SetCompleated(false);
                T2_S2.SetCompleated(false);
                break;
            case SubTaskEnum.ZERO:
                T1_S0_protection_equipment.SetCompleated(true);
                T1_S1.SetCompleated(false);
                T1_S2.SetCompleated(false);
                T1_S3.SetCompleated(false);
                T1_S4.SetCompleated(false);
                T2_S1.SetCompleated(false);
                T2_S2.SetCompleated(false);
                break;
            case SubTaskEnum.ONE:
                T1_S0_protection_equipment.SetCompleated(true);
                T1_S1.SetCompleated(true);
                T1_S2.SetCompleated(false);
                T1_S3.SetCompleated(false);
                T1_S4.SetCompleated(false);
                T2_S1.SetCompleated(false);
                T2_S2.SetCompleated(false);
                break;
            case SubTaskEnum.TWO:
                T1_S0_protection_equipment.SetCompleated(true);
                T1_S1.SetCompleated(true);
                T1_S2.SetCompleated(true);
                T1_S3.SetCompleated(false);
                T1_S4.SetCompleated(false);
                T2_S1.SetCompleated(false);
                T2_S2.SetCompleated(false);
                break;
            case SubTaskEnum.THREE:
                T1_S0_protection_equipment.SetCompleated(true);
                T1_S1.SetCompleated(true);
                T1_S2.SetCompleated(true);
                T1_S3.SetCompleated(true);
                T1_S4.SetCompleated(false);
                T2_S1.SetCompleated(false);
                T2_S2.SetCompleated(false);
                break;
            case SubTaskEnum.FOUR:
                T1_S0_protection_equipment.SetCompleated(true);
                T1_S1.SetCompleated(true);
                T1_S2.SetCompleated(true);
                T1_S3.SetCompleated(true);
                T1_S4.SetCompleated(true);
                T2_S1.SetCompleated(false);
                T2_S2.SetCompleated(false);
                T1.Compleated(true);
                T1.Compleated(true);
                break;
            case SubTaskEnum.FIVE:
                T1_S0_protection_equipment.SetCompleated(true);
                T1_S1.SetCompleated(true);
                T1_S2.SetCompleated(true);
                T1_S3.SetCompleated(true);
                T1_S4.SetCompleated(true);
                T2_S1.SetCompleated(true);
                T2_S2.SetCompleated(false);
                T1.Compleated(true);
                break;
            case SubTaskEnum.SIX:
                T1_S0_protection_equipment.SetCompleated(true);
                T1_S1.SetCompleated(true);
                T1_S2.SetCompleated(true);
                T1_S3.SetCompleated(true);
                T1_S4.SetCompleated(true);
                T2_S1.SetCompleated(true);
                T2_S2.SetCompleated(true);
                T1.Compleated(true);
                T2.Compleated(true);
                break;
            case SubTaskEnum.SEVEN:
                break;
            default:
                break;
        }
    }


    public void ChangeLevelDebug(int i)
    {

        switch (i)
        {

            case 0:
                SwitchElementVisibility(subtaskObjects[0]);
                CompleteElement(SubTaskEnum.START);
                break;
            case 1:
                SwitchElementVisibility(subtaskObjects[1]);
                CompleteElement(SubTaskEnum.ZERO);
                break;
            case 2:
                SwitchElementVisibility(subtaskObjects[2]);
                CompleteElement(SubTaskEnum.ONE);
                break;
            case 3:
                SwitchElementVisibility(subtaskObjects[3]);
                CompleteElement(SubTaskEnum.TWO);
                break;
            case 4:
                SwitchElementVisibility(subtaskObjects[4]);
                CompleteElement(SubTaskEnum.THREE);
                break;
            case 5:
                SwitchElementVisibility(subtaskObjects[5]);
                CompleteElement(SubTaskEnum.FOUR);
                break;
            case 6:
                SwitchElementVisibility(subtaskObjects[6]);
                CompleteElement(SubTaskEnum.FIVE);
                break;

            default:
                break;
        }

        


    }
    public void HasFinishedSubtask(SubTaskEnum stask)
    {
        foreach (var item in _temporarySubtaskObjects)
        {
            Destroy(item);
        }
        _temporarySubtaskObjects.Clear();

        Debug.LogWarning("Player has finished subtask" + stask);
        _subtask = stask + 1;
        switch (_subtask)
        {
            case SubTaskEnum.ZERO:
                CompleteElement(SubTaskEnum.START);//prepares the grounds
                SwitchElementVisibility(subtaskObjects[0]);
                _narrator.Narrate("Task 1, Subtask 0 - Safety equipment!<br>Pick up the correct protective equipment.");
                break;
            case SubTaskEnum.ONE:
                CompleteElement(SubTaskEnum.ZERO);//completes the last task

                SwitchElementVisibility(subtaskObjects[1]);
                _narrator.Narrate("Task 1, Subtask 1 - Create a wooden frame wall \n Pick up a beam from the table and attach it to the corresponding spot at one of the green outlines.");
                break;
            case SubTaskEnum.TWO:
                CompleteElement(SubTaskEnum.ONE);

                SwitchElementVisibility(subtaskObjects[2]);
                _narrator.Narrate("Task 1, Subtask 2 - Attach the housewrap for the outer layer, then staple it. Tape the holes, if any.");
                break;
            case SubTaskEnum.THREE:
                CompleteElement(SubTaskEnum.TWO);

                SwitchElementVisibility(subtaskObjects[3]);
                _narrator.Narrate("Task 1, Subtask 3 - Insert wood fiber insulation into the frame, then arrange it neatly into place.");
                break;
            case SubTaskEnum.FOUR:
                CompleteElement(SubTaskEnum.THREE);

                SwitchElementVisibility(subtaskObjects[4]);
                _narrator.Narrate("Task 1, Subtask 4 - Apply the vapor foil, stapling and taping it.");
                break;

            case SubTaskEnum.FIVE:
                CompleteElement(SubTaskEnum.FOUR);

                SwitchElementVisibility(subtaskObjects[5]);
                _narrator.Narrate("Task 2, Subtask 1 - Learn the layers of a roof, then fill out a short quiz.");
                StartCoroutine(narrateT2S1());
                break;

            case SubTaskEnum.SIX:
                CompleteElement(SubTaskEnum.FIVE);

                SwitchElementVisibility(subtaskObjects[6]);
                _narrator.Narrate("Task 2, Subtask 2 - Assemble the tiling of a roof.");
                break;

            case SubTaskEnum.SEVEN:
                CompleteElement(SubTaskEnum.SIX);

                break;
            default:
                break;
        }

    }

    
    void DefineSubTaskObjects()
    {
        subtaskObjects = new List<GameObject> { T1S0_VisibleElements ,
    T1S1_VisibleElements ,
    T1S2_VisibleElements ,
    T1S3_VisibleElements ,
    T1S4_VisibleElements ,
    T2S1_VisibleElements ,
    T2S2_VisibleElements };
    }
    void Start()
    {
        DefineSubTaskObjects();

        SwitchElementVisibility(subtaskObjects[0]);
        _narrator.Narrate("Task 1, Subtask 0 - Equip the mandatory protective shoes in order to start the Construction activity.");
    }

    void SwitchElementVisibility(GameObject subtask)
    {
        foreach (var item in subtaskObjects)
        {
            item.SetActive(false);
        }
        foreach (var item in _temporarySubtaskObjects)
        {
            Destroy(item);
        }
        subtask.SetActive(true);
    }
}
