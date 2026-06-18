using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DistortionManager : MonoBehaviour
{
    [SerializeField] private Volume volume;

    private ChromaticAberration chromatic;

    private void Start()
    {
        volume.profile.TryGet(out chromatic);
    }

    private void Update()
    {
        if (chromatic == null)
            return;

        int day = GameManager.Instance.CurrentDay;

        float intensity = 0f;

        if (day >= 15)
            intensity = 5f;
        else if (day >= 14)
            intensity = 1.4f;
        else if (day >= 13)
            intensity = 1f;
        else if (day >= 10)
            intensity = 0.3f;

        chromatic.intensity.value = intensity;
    }
}