using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2S1_MANAGER : MonoBehaviour
{

    public GameObject layer1, layer2, layer3, layer4;
    [SerializeReference] GameObject QuizMenu;
    [SerializeReference] GameObject MaterialChoiceMenu;


    public List<MeshRenderer> _rend;
    public Material roof_slate, roof_tiles, roof_metal;

    private void Start()
    {
        foreach (var item in _rend)
        {
            item.enabled = false;
        }
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
        layer3.SetActive(false) ;
        layer4.SetActive(true);

    }

}
