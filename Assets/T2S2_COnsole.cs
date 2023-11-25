using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class T2S2_COnsole : MonoBehaviour
{

    static T2S2_COnsole _instance;
    public static T2S2_COnsole Instance
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
        if (T2S2_COnsole.Instance != null)
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
