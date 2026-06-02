using UnityEngine;

public class GhostMonster : Monster
{
    [SerializeField]
    private AudioSource ambientSource;

    [SerializeField]
    private HealthComponent health;

    private void Start()
    {
        ambientSource.Play();
        health.OnDeath += StopPlaying;
    }


    private void StopPlaying()
    {
        ambientSource.Stop();
    }
}
