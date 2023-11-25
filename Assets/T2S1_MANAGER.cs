using System.Collections.Generic;
using UnityEngine;

public class T2S1_MANAGER : MonoBehaviour
{

    public GameObject layer1, layer2, layer3, layer4;
    [SerializeReference] GameObject DemonstrationMenu;
    [SerializeReference] GameObject QuizMenu;
    [SerializeReference] GameObject MaterialChoiceMenu;


    public List<MeshRenderer> _rend;
    public Material roof_slate, roof_tiles, roof_metal;


    #region QuizElements

    public GameObject btn_rafters, btn_underlayment, btn_battens, btn_tiles, quizCorrect, quizWrong;
    public bool Clickedrafters, Clickedunderlayment, Clickedbattens, Clickedtiles;
    public bool quizCanProceed;
    public List<GameObject> Picked_btns;

    private float buttonVerticalIncrement = -35;

    Vector3 startPos_btn_rafters, startPos_btn_underlayment, startPos_btn_battens, startPos_btn_tiles;
    Vector3 endPos_start
    {
        get
        {
            return endPos_obj_rafters.transform.position;
        }
    }

    public GameObject endPos_obj_rafters, endPos_obj_underlayment, endPos_obj_battens, endPos_obj_tiles;

    public void ClickRaftersobj()
    {
        if (Clickedrafters)
        {//if this has already been clicked, we remove it and move it back.
            btn_rafters.transform.position = startPos_btn_rafters;
            Picked_btns.Remove(btn_rafters);
            Clickedrafters = false;
        }
        else //we select it
        {
            Clickedrafters = true;
            btn_rafters.transform.position = new Vector3(endPos_start.x, endPos_start.y + (buttonVerticalIncrement * Picked_btns.Count), endPos_start.z);


            Picked_btns.Add(btn_rafters);
            CheckIfQuizIsCorrect();
        }

    }
    public void ClickBattenssobj()
    {
        if (Clickedbattens)
        {//if this has already been clicked, we remove it and move it back.
            btn_battens.transform.position = startPos_btn_battens;
            Picked_btns.Remove(btn_battens);
            Clickedbattens = false;

        }
        else //we select it
        {
            Clickedbattens = true;
           btn_battens.transform.position = new Vector3(endPos_start.x, endPos_start.y + (buttonVerticalIncrement * Picked_btns.Count), endPos_start.z);
            Picked_btns.Add(btn_battens);
            CheckIfQuizIsCorrect();
        }

    }
    public void ClickUnderlaymentsobj()
    {
        if (Clickedunderlayment)
        {//if this has already been clicked, we remove it and move it back.
            btn_underlayment.transform.position = startPos_btn_underlayment;
            Picked_btns.Remove(btn_underlayment);
            Clickedunderlayment = false;
        }
        else //we select it
        {
            Clickedunderlayment = true;
            btn_underlayment.transform.position = new Vector3(endPos_start.x, endPos_start.y + (buttonVerticalIncrement * Picked_btns.Count), endPos_start.z);
            Picked_btns.Add(btn_underlayment);
            CheckIfQuizIsCorrect();
        }

    }
    public void ClickTilessobj()
    {
        if (Clickedtiles)
        {//if this has already been clicked, we remove it and move it back.
            btn_tiles.transform.position = startPos_btn_tiles;
            Picked_btns.Remove(btn_tiles);
            Clickedtiles = false;
        }
        else //we select it
        {
            Clickedtiles = true;
            btn_tiles.transform.position = new Vector3(endPos_start.x, endPos_start.y + (buttonVerticalIncrement * Picked_btns.Count), endPos_start.z);
            Picked_btns.Add(btn_tiles);
            CheckIfQuizIsCorrect();
        }

    }

    void CheckIfQuizIsCorrect()
    {

        if (Picked_btns.Count < 4)
        {
            quizCorrect.SetActive(false);
            quizWrong.SetActive(false);
            return;
        }
        bool b = false;
        if (Picked_btns[0] == btn_rafters && Picked_btns[1] == btn_underlayment && Picked_btns[2] == btn_battens && Picked_btns[3] == btn_tiles)
        {
            b = true;
        }



        if (b)
        {
            quizCanProceed = true;
            quizCorrect.SetActive(true);
            quizWrong.SetActive(false);
        }
        else
        {
            quizCanProceed = false;
            quizCorrect.SetActive(false);
            quizWrong.SetActive(true);

        }
    }

    public void GoToQuiz()
    {
        DemonstrationMenu.SetActive(false);
        QuizMenu.SetActive(true);
        MaterialChoiceMenu.SetActive(false);
    }
    public void FinishQuiz()
    {
        if (quizCanProceed)
        {
            DemonstrationMenu.SetActive(false);
            QuizMenu.SetActive(false);
            MaterialChoiceMenu.SetActive(true);
        }
    }
    public void GoToNextSubtask()
    {

        if (hasSelectedMaterial)
        {
            ConstructionManager.Instance.HasFinishedSubtask(ConstructionManager.SubTaskEnum.FIVE);

        }
    }

    bool hasSelectedMaterial = false;
    void InitializeBtnStartingPos()
    {
        startPos_btn_rafters = btn_rafters.transform.position;
        startPos_btn_underlayment = btn_underlayment.transform.position;
        startPos_btn_battens = btn_battens.transform.position;
        startPos_btn_tiles = btn_tiles.transform.position;
    }



    #endregion

    private void Start()
    {
        foreach (var item in _rend)
        {
            item.enabled = false;
        }
        InitializeBtnStartingPos();
        DemonstrationMenu.SetActive(true);
        QuizMenu.SetActive(false);
        MaterialChoiceMenu.SetActive(false);
    }

    public void SetRoof(MaterialSelectorCube.SelectorMaterialTypeRoof incoming)
    {
        foreach (var item in _rend)
        {
            item.enabled = true;
        }
        switch (incoming)
        {
            case MaterialSelectorCube.SelectorMaterialTypeRoof.slate:
                foreach (var item in _rend)
                {
                    item.material = roof_slate;

                }

                break;
            case MaterialSelectorCube.SelectorMaterialTypeRoof.metal:
                foreach (var item in _rend)
                {
                    item.material = roof_metal;

                }
                break;
            case MaterialSelectorCube.SelectorMaterialTypeRoof.tiles:
                foreach (var item in _rend)
                {
                    item.material = roof_tiles;

                }
                break;
            default:
                break;
        }
    }


    public void MatChoice(int i)
    {
        hasSelectedMaterial = true;
        switch (i)
        {
            case 1:
                SetRoof(MaterialSelectorCube.SelectorMaterialTypeRoof.metal);
                break;
            case 2:
                SetRoof(MaterialSelectorCube.SelectorMaterialTypeRoof.slate);
                break;

            case 3:
                SetRoof(MaterialSelectorCube.SelectorMaterialTypeRoof.tiles);
                break;
            default:
                break;
        }
    }

    public void Activate1()
    {
        layer1.SetActive(true);
        layer2.SetActive(true);
        layer3.SetActive(true);
        layer4.SetActive(true);
    }
    public void Activate2()
    {
        layer1.SetActive(false);
        layer2.SetActive(true);
        layer3.SetActive(true);
        layer4.SetActive(true);

    }
    public void Activate3()
    {
        layer1.SetActive(false);
        layer2.SetActive(false);
        layer3.SetActive(true);
        layer4.SetActive(true);

    }
    public void Activate4()
    {
        layer1.SetActive(false);
        layer2.SetActive(false);
        layer3.SetActive(false);
        layer4.SetActive(true);

    }

}
