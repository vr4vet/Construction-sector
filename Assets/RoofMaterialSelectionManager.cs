using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofMaterialSelectionManager : MonoBehaviour
{



    public List<MeshRenderer> _rend;
    public Material roof_slate, roof_tiles, roof_metal;

    private void Start()
    {
        foreach (var item in _rend)
        {
            item.enabled = false;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetRoof(MaterialSelectorCube.SelectorMaterialTypeRoof.metal);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetRoof(MaterialSelectorCube.SelectorMaterialTypeRoof.slate);


        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SetRoof(MaterialSelectorCube.SelectorMaterialTypeRoof.tiles);
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
}
