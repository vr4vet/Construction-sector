using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S2T1_Quiz_Console : MonoBehaviour
{

    static S2T1_Quiz_Console _instance;
    public static S2T1_Quiz_Console Instance
    {
        get
        {
            return _instance;
        }
    }


    GameObject DebugPrefab;
    //references
    [Header("Menu references")]
    [SerializeReference] T2S1_MANAGER _manager;

    [SerializeReference] GameObject MainMenu;
    [SerializeReference] GameObject PleaseWait;
    [SerializeReference] GameObject QuizMenu;
    [SerializeReference] GameObject MaterialChoiceMenu;

    private List<GameObject> allMenus = new();

    #region UnityMethods
    void Awake()
    {
        if (S2T1_Quiz_Console.Instance != null)
        {
            throw new System.Exception(name + " - FloatingUIManager.Awake() - Tried to initialize duplicate singleton.");
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        allMenus.AddRange(new List<GameObject>() { MainMenu, QuizMenu, MaterialChoiceMenu });

        foreach (var item in allMenus)
        {
            item.SetActive(false);
        }
        MainMenu.SetActive(true);
    }

    #endregion

    #region Navigation Methods
   public void SwitchToMenu()
    {
        foreach (var item in allMenus)
        {
            item.SetActive(false);
        }
        MainMenu.SetActive(true);
    }
    public void SwitchToQuiz()
    {
        foreach (var item in allMenus)
        {
            item.SetActive(false);
        }
        QuizMenu.SetActive(true);
    }

    void SwitchToRoofDemonstrationAndMatChoice()
    {
        foreach (var item in allMenus)
        {
            item.SetActive(false);
        }
        MaterialChoiceMenu.SetActive(true);
    }

    void PermitQuiz()
    {

    }



    #endregion


    #region Misc

    void MakePlayerWait()
    {
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {

        PleaseWait.SetActive(true);

        yield return new WaitForSecondsRealtime(5f);

        PleaseWait.SetActive(false);
    }

    #endregion
}
