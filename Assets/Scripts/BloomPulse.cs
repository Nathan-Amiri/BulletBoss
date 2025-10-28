using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BloomPulse : MonoBehaviour
{
    [SerializeField] private Volume globalVolume;
    private Bloom bloom;

    public float defaultIntensity;
    public float pulseIntensity;
    public float pulseDuration;

    private void Awake()
    {
        globalVolume.profile.TryGet(out bloom);
        bloom.intensity.value = defaultIntensity;
    }
    public void Pulse()
    {
        bloom.intensity.value = pulseIntensity;

        Invoke(nameof(EndPulse), pulseDuration);
    }
    private void EndPulse()
    {
        globalVolume.profile.TryGet(out Bloom bloom);
        bloom.intensity.value = defaultIntensity;
    }
}