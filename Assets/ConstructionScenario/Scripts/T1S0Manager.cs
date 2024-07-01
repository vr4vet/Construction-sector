using System.Collections;
using UnityEngine;

public class T1S0Manager : MonoBehaviour
{
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;

    public GameObject S1Prefab;
    public GameObject S2Prefab;
    public GameObject S3Prefab;

    private GameObject[] parents;
    private GameObject[] shoes;

    public static T1S0Manager Ins;

    private void Awake()
    {
        if(Ins == null)
        {
            Ins = this;
        }
        else
        {
            Destroy(this);
        }
    }

    IEnumerator finishedSubtask()
    {

       yield return new WaitForSecondsRealtime(3f);
        ConstructionManager.Instance.HasFinishedSubtask(ConstructionManager.SubTaskEnum.ZERO);


    }
    IEnumerator wrongShoes()
    {

        ConstructionManager.Instance._narrator.Narrate("Incorrect shoes! These are not working shoes! Try again in a few seconds!");
        yield return new WaitForSecondsRealtime(3f);
        // Spawn new shoes randomly
        SpawnShoesRandomly();


    }
    internal void CheckIfCorrectShoe(GameObject gameObject)
    {
        if (gameObject.CompareTag("Shoes"))
        {
            Debug.Log("Correct shoe collided!");
            // Handle correct shoe collided, proceed to the next task
            StartCoroutine(finishedSubtask());
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Wrong shoe collided!");
            // Handle incorrect shoe collided, remove and unassign all shoes, then spawn again
            RemoveAndRespawnShoes(gameObject);
        }
    }

    void Start()
    {
        parents = new GameObject[] { P1, P2, P3 };
        shoes = new GameObject[] { S1Prefab, S2Prefab, S3Prefab };

        SpawnShoesRandomly();
    }

    void Update()
    {
        
    }

    void SpawnShoesRandomly()
    {
        Shuffle(parents);

        for (int i = 0; i < Mathf.Min(parents.Length, shoes.Length); i++)
        {
            GameObject shoeInstance = Instantiate(shoes[i], parents[i].transform.position, Quaternion.identity, parents[i].transform);
            shoeInstance.SetActive(true);
        }
    }

    void RemoveAndRespawnShoes(GameObject g)
    {
        // Unassign and destroy existing shoes
        foreach (var parent in parents)
        {
            // Check if the parent has any children before trying to access them
            if (parent.transform.childCount > 0)
            {
                // Destroy all children of the parent
                foreach (Transform child in parent.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
        Destroy(g);

        // Spawn new shoes randomly
        StartCoroutine(wrongShoes());
    }


    void Shuffle(GameObject[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randIndex = Random.Range(0, i + 1);
            GameObject temp = array[i];
            array[i] = array[randIndex];
            array[randIndex] = temp;
        }
    }
}
