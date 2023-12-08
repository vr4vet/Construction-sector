using UnityEngine;

public class OnStairEnter : MonoBehaviour
{
    public StairScript stairs;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Stair")
        {
            stairs.OnTrigger(other);
        }
        if (other.tag == "WoodFiber")
        {
            other.GetComponent<WoodFiberPlayerDetector>().TriggerManually();
        }
    }
}
