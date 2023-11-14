using System.Collections.Generic;
using UnityEngine;

public class MaterialSelectorCube : MonoBehaviour
{
    public Material _mat;
    public List<Renderer> _rend;
    public BNG.Grabbable grab;

    public enum SelectorMaterialTypeRoof
    {
        slate,
        metal,
        tiles
    }

    void Update()
    {
        if (grab.BeingHeld)
        {
            foreach (var item in _rend)
            {
                item.enabled = true;
                item.material = _mat;
            }
            
        }
    }


    // public SelectorMaterialTypeRoof _mat;
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
    //    {

    //       // _manref.SetRoof(_mat);
    //    }
    //}
}
