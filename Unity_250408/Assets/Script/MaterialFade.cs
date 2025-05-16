using UnityEngine;
using System.Collections.Generic;

public class MaterialFade : MonoBehaviour
{
    public GameObject[] targetObjects;
    public float startAlpha = 1.0f;
    public float endAlpha = 0.2f;
    public float lerpDuration = 1.0f;
    public float stagger = 0.1f; // Delay between each object's fade start

    private List<Material> materials = new List<Material>();
    private List<Color> baseColors = new List<Color>();
    private float[] delayTimers;
    private bool[] fading;

    private float globalTimer = 0f;
    private bool isFadingOut = false;
    private bool isFadingIn = false;

    void Start()
    {
        List<float> delays = new List<float>();

        foreach (GameObject obj in targetObjects)
        {
            if (obj.TryGetComponent(out Renderer renderer))
            {
                Material mat = renderer.material; // Instantiate
                Color color = mat.color;
                color.a = startAlpha;
                mat.color = color;

                materials.Add(mat);
                baseColors.Add(color);
            }
        }

        int count = materials.Count;
        delayTimers = new float[count];
        fading = new bool[count];

        for (int i = 0; i < count; i++)
            delayTimers[i] = stagger * i;
    }

    void OnTriggerEnter(Collider other)
    {
        globalTimer = 0f;
        isFadingOut = true;
        isFadingIn = false;
        ResetFading();
    }

    void OnTriggerExit(Collider other)
    {
        globalTimer = 0f;
        isFadingOut = false;
        isFadingIn = true;
        ResetFading();
    }

    void Update()
    {
        if (!isFadingOut && !isFadingIn) return;

        globalTimer += Time.deltaTime;

        for (int i = 0; i < materials.Count; i++)
        {
            if (!fading[i] && globalTimer >= delayTimers[i])
            {
                fading[i] = true;
                delayTimers[i] = globalTimer; // reuse to track start time
            }

            if (fading[i])
            {
                float t = (globalTimer - delayTimers[i]) / lerpDuration;
                t = Mathf.Clamp01(t);

                float newAlpha = isFadingOut
                    ? Mathf.Lerp(startAlpha, endAlpha, t)
                    : Mathf.Lerp(endAlpha, startAlpha, t);

                Color newColor = baseColors[i];
                newColor.a = newAlpha;
                materials[i].color = newColor;
            }
        }
    }

    void ResetFading()
    {
        for (int i = 0; i < fading.Length; i++)
            fading[i] = false;
    }
}