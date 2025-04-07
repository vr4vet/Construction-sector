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

    int LAST_COMPLETED_LEVEL = 0;
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
        LAST_COMPLETED_LEVEL = PlayerPrefs.HasKey("LAST_SAVED_LEVEL") ? PlayerPrefs.GetInt("LAST_SAVED_LEVEL") : 0;

        Debug.Log("PlayerPrefs - last saved level is " + LAST_COMPLETED_LEVEL);
        Start();


    }

    public void ClearSave()
    {
        LAST_COMPLETED_LEVEL = PlayerPrefs.GetInt("LAST_SAVED_LEVEL", 0);
    }

    public void Save()
    {

        PlayerPrefs.SetInt("LAST_SAVED_LEVEL", LAST_COMPLETED_LEVEL);
    }

    private void Load()
    {
        if (LAST_COMPLETED_LEVEL == null) return;

        switch (LAST_COMPLETED_LEVEL)
        {
            case 0:

                Start00();//t1s0
                break;
            case 1: //t1s1
                Start01();
                break;
            case 2: //t1s2
                Start02();
                break;
            case 3: //t1s3
                Start03();
                break;
            case 4: //t1s4
                Start04();
                break;
            case 5: //t2s1
                Start05();
                break;
            case 6: //t2s2
                Start06();
                break;
            default:
                Start01();
                break;
        }


        //handled saving
        //handled loading
        //should be good now

        void Start00()
        { //shoe task
            LAST_SUBTASK = SubTaskEnum.START;
            SwitchElementVisibility(subtaskObjects[0]);
            _narrator.Narrate("Task 1, Subtask 0 - Safety equipment!<br>Pick up the correct protective equipment.");
            CompleteElement(SubTaskEnum.START);
        }


        void Start01()
        {
            LAST_SUBTASK = SubTaskEnum.ZERO;
            SwitchElementVisibility(subtaskObjects[1]);
            _narrator.Narrate("Task 1, Subtask 1 - Create a wooden frame wall \n Pick up a beam from the table and attach it to the corresponding spot at one of the green outlines.");
            CompleteElement(SubTaskEnum.ZERO);
        }
        void Start02()
        {
            LAST_SUBTASK = SubTaskEnum.ONE;
            SwitchElementVisibility(subtaskObjects[2]);
            _narrator.Narrate("Task 1, Subtask 2 - Attach the housewrap for the outer layer, then staple it. Tape the holes, if any.");
            CompleteElement(SubTaskEnum.ONE);//always one less because we dont want the task to be completed...
        }

        void Start03()
        {
            LAST_SUBTASK = SubTaskEnum.TWO;
            SwitchElementVisibility(subtaskObjects[3]);
            _narrator.Narrate("Task 1, Subtask 3 - Insert wood fiber insulation into the frame, then arrange it neatly into place.");
            CompleteElement(SubTaskEnum.TWO);
        }

        void Start04()
        {
            LAST_SUBTASK = SubTaskEnum.THREE;
            SwitchElementVisibility(subtaskObjects[4]);
            _narrator.Narrate("Task 1, Subtask 4 - Apply the vapor foil, stapling and taping it.");
            CompleteElement(SubTaskEnum.THREE);
        }

        void Start05()
        {
            LAST_SUBTASK = SubTaskEnum.FOUR;
            SwitchElementVisibility(subtaskObjects[5]);
            _narrator.Narrate("Task 2, Subtask 1 - Learn the layers of a roof, then fill out a short quiz.");
            CompleteElement(SubTaskEnum.FOUR);
        }

        void Start06()
        {
            LAST_SUBTASK = SubTaskEnum.FIVE;
            SwitchElementVisibility(subtaskObjects[6]);
            _narrator.Narrate("Task 2, Subtask 2 - Assemble the tiling of a roof.");
            CompleteElement(SubTaskEnum.FIVE);
        }

       


      

    }

    void Update()
    {
        #region debug_controls
        //if (Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    _subtask = SubTaskEnum.ZERO;
        //    SwitchElementVisibility(subtaskObjects[0]);
        //    _narrator.Narrate("Task 1, Subtask 0 - Safety equipment!<br>Pick up the correct protective equipment.");
        //    CompleteElement(SubTaskEnum.START);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    _subtask = SubTaskEnum.ONE;
        //    SwitchElementVisibility(subtaskObjects[1]);
        //    _narrator.Narrate("Task 1, Subtask 1 - Create a wooden frame wall \n Pick up a beam from the table and attach it to the corresponding spot at one of the green outlines.");
        //    CompleteElement(SubTaskEnum.ZERO);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    _subtask = SubTaskEnum.TWO;
        //    SwitchElementVisibility(subtaskObjects[2]);
        //    _narrator.Narrate("Task 1, Subtask 2 - Attach the housewrap for the outer layer, then staple it. Tape the holes, if any.");
        //    CompleteElement(SubTaskEnum.ONE);//always one less because we dont want the task to be completed...
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    _subtask = SubTaskEnum.THREE;
        //    SwitchElementVisibility(subtaskObjects[3]);
        //    _narrator.Narrate("Task 1, Subtask 3 - Insert wood fiber insulation into the frame, then arrange it neatly into place.");
        //    CompleteElement(SubTaskEnum.TWO);//always one less because we dont want the task to be completed...
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    _subtask = SubTaskEnum.FOUR;
        //    SwitchElementVisibility(subtaskObjects[4]);
        //    _narrator.Narrate("Task 1, Subtask 4 - Apply the vapor foil, stapling and taping it.");
        //    CompleteElement(SubTaskEnum.THREE);//always one less because we dont want the task to be completed...

        //}
        //if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    _subtask = SubTaskEnum.FIVE;
        //    SwitchElementVisibility(subtaskObjects[5]);
        //    _narrator.Narrate("Task 2, Subtask 1 - Learn the layers of a roof, then fill out a short quiz.");
        //    CompleteElement(SubTaskEnum.FOUR);//always one less because we dont want the task to be completed...
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha6))
        //{
        //    _subtask = SubTaskEnum.SIX;

        //    SwitchElementVisibility(subtaskObjects[6]);
        //    _narrator.Narrate("Task 2, Subtask 2 - Assemble the tiling of a roof.");
        //    CompleteElement(SubTaskEnum.FIVE);//always one less because we dont want the task to be completed...
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha7))
        //{
        //    _subtask = SubTaskEnum.SEVEN;

        //    SwitchElementVisibility(subtaskObjects[6]);

        //    CompleteElement(SubTaskEnum.SIX);//always one less because we dont want the task to be completed...

        //}
        #endregion

    }

    public enum SubTaskEnum
    {
        START,
        ZERO, //t1s0
        ONE, //t1s1
        TWO, //t1s2
        THREE, //t1s3
        FOUR, //t1s4
        FIVE, //technically t2s1
        SIX,//technically t2s2
            SEVEN
    }


    public SubTaskEnum LAST_SUBTASK = SubTaskEnum.ZERO;
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
            case SubTaskEnum.ZERO:
                T1_S0_protection_equipment.SetCompleated(false);
                T1_S1.SetCompleated(false);
                T1_S2.SetCompleated(false);
                T1_S3.SetCompleated(false);
                T1_S4.SetCompleated(false);
                T2_S1.SetCompleated(false);
                T2_S2.SetCompleated(false);
                break;
            case SubTaskEnum.ONE:
                T1_S0_protection_equipment.SetCompleated(true);

                T1_S1.SetCompleated(false);
                T1_S2.SetCompleated(false);
                T1_S3.SetCompleated(false);
                T1_S4.SetCompleated(false);
                T2_S1.SetCompleated(false);
                T2_S2.SetCompleated(false);
                break;
            case SubTaskEnum.TWO:
               
                T1_S0_protection_equipment.SetCompleated(true);

                T1_S1.SetCompleated(true);
                T1_S2.SetCompleated(false);
                T1_S3.SetCompleated(false);
                T1_S4.SetCompleated(false);
                T2_S1.SetCompleated(false);
                T2_S2.SetCompleated(false);
                break;
            case SubTaskEnum.THREE:
                T1_S0_protection_equipment.SetCompleated(true);

                T1_S1.SetCompleated(true);
                T1_S2.SetCompleated(true);
                T1_S3.SetCompleated(false);
                T1_S4.SetCompleated(false);
                T2_S1.SetCompleated(false);
                T2_S2.SetCompleated(false);
                break;
            case SubTaskEnum.FOUR:
                T1_S0_protection_equipment.SetCompleated(true);

                T1_S1.SetCompleated(true);
                T1_S2.SetCompleated(true);
                T1_S3.SetCompleated(true);
                T1_S4.SetCompleated(false);
                T2_S1.SetCompleated(false);
                T2_S2.SetCompleated(false);
                T1.Compleated(true);
                break;
            case SubTaskEnum.FIVE:
                T1_S0_protection_equipment.SetCompleated(true);

                T1_S1.SetCompleated(true);
                T1_S2.SetCompleated(true);
                T1_S3.SetCompleated(true);
                T1_S4.SetCompleated(true);
                T2_S1.SetCompleated(false);
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
                T2_S2.SetCompleated(false);
                T1.Compleated(true);
                break;
            case SubTaskEnum.SEVEN:
                LAST_COMPLETED_LEVEL = 0; // not needed, as the user has finished the entire game at this point. so he can start from the beginning.
                //Debug.LogError("Player started SubTaskEnum.SEVEN");

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
            default:
                //Debug.LogError("Player started an unexpected subtask.");

                break;
        }

    }


    //public void ChangeLevelDebug(int i)
    //{

    //    switch (i)
    //    {

    //        case 0:
    //            SwitchElementVisibility(subtaskObjects[0]);
    //            CompleteElement(SubTaskEnum.START);
    //            break;
    //        case 1:
    //            SwitchElementVisibility(subtaskObjects[1]);
    //            CompleteElement(SubTaskEnum.ZERO);
    //            break;
    //        case 2:
    //            SwitchElementVisibility(subtaskObjects[2]);
    //            CompleteElement(SubTaskEnum.ONE);
    //            break;
    //        case 3:
    //            SwitchElementVisibility(subtaskObjects[3]);
    //            CompleteElement(SubTaskEnum.TWO);
    //            break;
    //        case 4:
    //            SwitchElementVisibility(subtaskObjects[4]);
    //            CompleteElement(SubTaskEnum.THREE);
    //            break;
    //        case 5:
    //            SwitchElementVisibility(subtaskObjects[5]);
    //            CompleteElement(SubTaskEnum.FOUR);
    //            break;
    //        case 6:
    //            SwitchElementVisibility(subtaskObjects[6]);
    //            CompleteElement(SubTaskEnum.FIVE);
    //            break;

    //        default:
    //            break;
    //    }

        


    //}
    public void HasFinishedSubtask(SubTaskEnum stask)
    {
        foreach (var item in _temporarySubtaskObjects)
        {
            Destroy(item);
        }
        _temporarySubtaskObjects.Clear();

        //Debug.LogWarning("Player has finished subtask" + stask);
        LAST_SUBTASK = stask + 1;
        switch (LAST_SUBTASK)
        {
            case SubTaskEnum.ZERO:
                CompleteElement(SubTaskEnum.START);//prepares the grounds
                SwitchElementVisibility(subtaskObjects[0]);
               // _narrator.Narrate("Task 1, Subtask 0 - Safety equipment!<br>Pick up the correct protective equipment.");
                break;
            case SubTaskEnum.ONE:
                CompleteElement(SubTaskEnum.ZERO);//completes the previous task about safety equipment
                //Debug.LogError("Saved progress. [T1S0]");
                LAST_COMPLETED_LEVEL = 1;

                SwitchElementVisibility(subtaskObjects[1]);
                break;
            case SubTaskEnum.TWO:
                CompleteElement(SubTaskEnum.ONE);
                LAST_COMPLETED_LEVEL = 2;
                SwitchElementVisibility(subtaskObjects[2]);
               // _narrator.Narrate("Task 1, Subtask 2 - Attach the housewrap for the outer layer, then staple it. Tape the holes, if any.");
                break;
            case SubTaskEnum.THREE:
                CompleteElement(SubTaskEnum.TWO);
                LAST_COMPLETED_LEVEL = 3;
                SwitchElementVisibility(subtaskObjects[3]);
                break;

            case SubTaskEnum.FOUR:
                CompleteElement(SubTaskEnum.THREE);
                LAST_COMPLETED_LEVEL = 4;
                SwitchElementVisibility(subtaskObjects[5]);
                StartCoroutine(narrateT2S1());

                SwitchElementVisibility(subtaskObjects[4]);
                break;

            case SubTaskEnum.FIVE:
                CompleteElement(SubTaskEnum.FOUR);
                LAST_COMPLETED_LEVEL = 5;
                SwitchElementVisibility(subtaskObjects[5]);
                StartCoroutine(narrateT2S1());
                break;

            case SubTaskEnum.SIX:
                CompleteElement(SubTaskEnum.FIVE);
                LAST_COMPLETED_LEVEL = 6;
                SwitchElementVisibility(subtaskObjects[6]);
                break;

            case SubTaskEnum.SEVEN:
                CompleteElement(SubTaskEnum.SIX);
                LAST_COMPLETED_LEVEL = 0; // if you finish it, you go back to the beginning next time you load up the game.
                break;
            default:
                break;
        }
        Save();

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
        Load();

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
