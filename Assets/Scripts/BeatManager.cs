using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{
    // This script only calls the trigger when the music is playing

    private readonly float bpm = 120;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Intervals[] intervals;

    private void Update()
    {
        foreach (Intervals interval in intervals)
        {
            float sampledTime = musicSource.timeSamples / (musicSource.clip.frequency * interval.GetIntervalLength(bpm));
            interval.CheckForNewInterval(sampledTime);
        }
    }
}

[System.Serializable]
public class Intervals
{
    [SerializeField] private float beatMultiplier; // 0.5 = half time, 2 = double time
    [SerializeField] private UnityEvent trigger;

    private int lastInterval;

    public float GetIntervalLength(float bpm)
    {
        return 60 / (bpm * beatMultiplier);
    }

    public void CheckForNewInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != lastInterval)
        {
            lastInterval = Mathf.FloorToInt(interval);
            trigger.Invoke();
        }
    }
}