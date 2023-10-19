using UnityEngine;

public class MaterialSelectorCube : MonoBehaviour
{
    public RoofMaterialSelectionManager _manref;

    public enum SelectorMaterialTypeRoof
    {
        slate,
        metal,
        tiles
    }

    public SelectorMaterialTypeRoof _mat;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

           // _manref.SetRoof(_mat);
        }
    }
}
