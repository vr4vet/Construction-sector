using UnityEngine;

//Class for creating blinking effect with a choosen color
public class BlinkingEffect : MonoBehaviour
{
    public Color startColor = Color.green;
    public Color endColor = Color.black;
    public Color originalColor;

    [Range(0, 10)]
    public float speed = 1;
    Renderer ren;

    void OnEnable()
    {
        ren = gameObject.GetComponent<Renderer>();
        originalColor = ren.material.color;
        Debug.Log($"Enabling blinking effect on {gameObject.name}");
    }

    void OnDisable()
    {
        ren.material.color = originalColor;
        Debug.Log($"Disabling blinking effect on {gameObject.name}");
    }

    //Creating continious blinking effect
    void Update()
    {
        ren.material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
    }
}
