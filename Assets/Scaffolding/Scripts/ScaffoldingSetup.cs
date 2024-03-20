using UnityEngine;
using System.Collections.Generic;
using BNG;

public class ScaffoldingSetup : MonoBehaviour
{
    public List<ScaffoldingPart> scaffoldingParts;

    public List<ScaffoldingPickUpPart> scaffoldingPickUpParts;

    private int currentPartIndex = 0;
    
    public GameObject hand;
    public Grabber grabberComponent;

    public Material material;

    void Start()
    {
        grabberComponent = hand.GetComponent<Grabber>();

        scaffoldingParts.Add(new ScaffoldingPart("FootPiece", GameObject.FindGameObjectWithTag("FootPiece")));
        scaffoldingParts.Add(new ScaffoldingPart("LongBeamBottom", GameObject.FindGameObjectWithTag("LongBeamBottom")));
        scaffoldingParts.Add(new ScaffoldingPart("CrossBeamBottom", GameObject.FindGameObjectWithTag("CrossBeamBottom")));
        scaffoldingParts.Add(new ScaffoldingPart("StandardBottom", GameObject.FindGameObjectWithTag("StandardBottom")));
        scaffoldingParts.Add(new ScaffoldingPart("Bracing", GameObject.FindGameObjectWithTag("Bracing")));
        scaffoldingParts.Add(new ScaffoldingPart("LongBeamTop", GameObject.FindGameObjectWithTag("LongBeamTop")));
        scaffoldingParts.Add(new ScaffoldingPart("CrossBeamTop", GameObject.FindGameObjectWithTag("CrossBeamTop")));
        scaffoldingParts.Add(new ScaffoldingPart("Kickboard", GameObject.FindGameObjectWithTag("Kickboard")));
        scaffoldingParts.Add(new ScaffoldingPart("SteelDeck", GameObject.FindGameObjectWithTag("SteelDeck")));
        scaffoldingParts.Add(new ScaffoldingPart("Railing", GameObject.FindGameObjectWithTag("Railing")));
        scaffoldingParts.Add(new ScaffoldingPart("LadderBeam", GameObject.FindGameObjectWithTag("LadderBeam")));
        scaffoldingParts.Add(new ScaffoldingPart("LadderStandard", GameObject.FindGameObjectWithTag("LadderStandard")));
        scaffoldingParts.Add(new ScaffoldingPart("Ladder", GameObject.FindGameObjectWithTag("Ladder")));
        scaffoldingParts.Add(new ScaffoldingPart("StandardTop", GameObject.FindGameObjectWithTag("StandardTop")));

        scaffoldingPickUpParts.Add(new ScaffoldingPickUpPart("FootPiecePickUp", GameObject.FindGameObjectWithTag("FootPiecePickUp")));
        // scaffoldingPickUpParts.Add(new ScaffoldingPickUpPart("LongBeamBottomPickUp", GameObject.FindGameObjectWithTag("LongBeamBottomPickUp")));
        // scaffoldingPickUpParts.Add(new ScaffoldingPickUpPart("CrossBeamBottomPickUp", GameObject.FindGameObjectWithTag("CrossBeamBottomPickUp")));
        // scaffoldingPickUpParts.Add(new ScaffoldingPickUpPart("StandardBottomPickUp", GameObject.FindGameObjectWithTag("StandardBottomPickUp")));
        // scaffoldingPickUpParts.Add(new ScaffoldingPickUpPart("BracingPickUp", GameObject.FindGameObjectWithTag("BracingPickUp")));
        // scaffoldingPickUpParts.Add(new ScaffoldingPickUpPart("LongBeamTopPickUp", GameObject.FindGameObjectWithTag("LongBeamTopPickUp")));
        // scaffoldingPickUpParts.Add(new ScaffoldingPickUpPart("CrossBeamTopPickUp", GameObject.FindGameObjectWithTag("CrossBeamTopPickUp")));
        // scaffoldingPickUpParts.Add(new ScaffoldingPickUpPart("KickboardPickUp", GameObject.FindGameObjectWithTag("KickboardPickUp")));
        // scaffoldingPickUpParts.Add(new ScaffoldingPickUpPart("SteelDeckPickUp", GameObject.FindGameObjectWithTag("SteelDeckPickUp")));
        // scaffoldingPickUpParts.Add(new ScaffoldingPickUpPart("RailingPickUp", GameObject.FindGameObjectWithTag("RailingPickUp")));
        // scaffoldingPickUpParts.Add(new ScaffoldingPickUpPart("LadderBeamPickUp", GameObject.FindGameObjectWithTag("LadderBeamPickUp")));
        // scaffoldingPickUpParts.Add(new ScaffoldingPickUpPart("LadderStandardPickUp", GameObject.FindGameObjectWithTag("LadderStandardPickUp")));
        // scaffoldingPickUpParts.Add(new ScaffoldingPickUpPart("LadderPickUp", GameObject.FindGameObjectWithTag("LadderPickUp")));
        // scaffoldingPickUpParts.Add(new ScaffoldingPickUpPart("StandardTopPickUp", GameObject.FindGameObjectWithTag("StandardTopPickUp")));

        InitializeScaffolding(scaffoldingParts, false);
    }

    void Update()
    {
        if (currentPartIndex < scaffoldingParts.Count)
        {
            ScaffoldingPart scaffoldingPart = scaffoldingParts[currentPartIndex];
            ScaffoldingPickUpPart pickUpPart = scaffoldingPickUpParts[currentPartIndex];
            SetAllObjectsVisibility(scaffoldingPart,true);
            if (scaffoldingPart.IsVisible)
            {

                //PickUpPart(pickUpPart);
                // Debug.Log($"Place the {pickUpPart.Name} on the scaffolding.");
                PlacePartOnScaffolding(pickUpPart, scaffoldingPart);

                // Move to the next part
                if (scaffoldingParts[currentPartIndex].DoneTask)
                    currentPartIndex++;
            }
        }
        else
        {
            Debug.Log("Congratulations! Scaffolding is complete.");
        }
    }

    void InitializeScaffolding(List<ScaffoldingPart> parts, bool isVisible)
    {
        // Make all parts invisible initially
        foreach (ScaffoldingPart parent in parts)
        {
            parent.SetVisibility(isVisible);

            foreach (Transform child in parent.Part.transform)
            {
                if (child.GetComponent<BlinkingEffect>() == null)
                    child.gameObject.AddComponent<BlinkingEffect>();
            }
        }
    }

    void SetAllObjectsVisibility(ScaffoldingPart parent, bool isVisible)
    {
        if (parent.IsVisible != isVisible)
            parent.SetVisibility(isVisible);
    }

    void PickUpPart(ScaffoldingPickUpPart part)
    {
        // Check if the part exists
        if (part == null)
            return;
        
            // Check for collisions with the hand collider
            bool isCollidingWithHand = Physics.CheckBox(part.Part.transform.position, part.Part.transform.localScale / 2f, part.Part.transform.rotation, 12);
            // If the part is colliding with the hand, scale it to half size
            if (isCollidingWithHand && (grabberComponent.HeldGrabbable != null) && part.Part.transform.localScale==part.Scale)
            {

                part.Part.transform.localScale = part.Scale * 0.5f;

            }
            else if (!isCollidingWithHand || grabberComponent.HeldGrabbable == null)
            {
                // If the part is not colliding with the hand anymore, scale it to its usual size
                part.Part.transform.localScale = part.Scale;
            }
        
    }

    void PlacePartOnScaffolding(ScaffoldingPickUpPart pickUpPart, ScaffoldingPart scaffoldingPart)
    {
        // Implement VR interactions to place the part on the scaffolding
        GameObject scaffoldingPartGameObject = scaffoldingPart.Part;
        GameObject pickUpPartGameObject = pickUpPart.Part;
        BlinkingEffect highlightObject = scaffoldingPartGameObject.GetComponent<BlinkingEffect>();

        bool isCollidingWithHand = Physics.CheckBox(pickUpPartGameObject.transform.position, pickUpPartGameObject.transform.localScale / 10f, pickUpPartGameObject.transform.rotation, LayerMask.GetMask(scaffoldingPart.Name));
        if (isCollidingWithHand && !scaffoldingPart.DoneTask)
        {
            foreach (Transform child in scaffoldingPartGameObject.transform)
            {
                if (child.GetComponent<BlinkingEffect>() != null)
                {
                    Destroy(child.gameObject.GetComponent<BlinkingEffect>());
                    child.gameObject.GetComponent<Renderer>().material = material;
                }       
            }
            Destroy(pickUpPartGameObject);
            scaffoldingPart.DoneTask = true;
        }
    }
}

[System.Serializable]
public class ScaffoldingPart
{
    public string Name;
    public bool IsVisible;
    public bool DoneTask;
    public GameObject Part;

    public ScaffoldingPart(string name, GameObject part)
    {
        Name = name;
        DoneTask = false;
        Part = part;
    }

    public void SetVisibility(bool isVisible)
    {
        if (Part != null)
        {
            Part.SetActive(isVisible);
            IsVisible = isVisible;
        }
    }
}

[System.Serializable]
public class ScaffoldingPickUpPart
{
    public string Name;
    public GameObject Part;
    public Vector3 Scale;

    public ScaffoldingPickUpPart(string name, GameObject part)
    {
        Name = name;
        Part = part;
        Scale = part.transform.localScale;
    }
}

