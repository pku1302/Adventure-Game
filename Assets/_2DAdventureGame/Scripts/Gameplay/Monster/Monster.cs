using NUnit.Framework;
using UnityEngine;


public class Monster : MonoBehaviour
{
    [SerializeField] private MonsterData data;
    public MonsterData Data => data;
    public AudioSource audioSource;
    public AudioClip footstepL;
    public AudioClip footstepR;
    public AudioClip hitSound;
    public AudioClip deadSound;
    public AudioClip deadSound2;
    public AudioClip attackSound;
    public AudioClip attackSound2;
    public AudioClip guardSound;

    public LootComponent Loot { get; private set; }

    private bool isLeft = false;
    private bool executed = false;
    private HealthComponent healthComponent;

    private void Start()
    {
        Loot = GetComponent<LootComponent>();
        healthComponent = GetComponent<HealthComponent>();
    }


    void PlayFootStep()
    {
        if (isLeft && footstepR != null)
        {
            audioSource.PlayOneShot(footstepR);
            isLeft = false;
        }
        else if (footstepL != null)
        {
            audioSource.PlayOneShot(footstepL);
            isLeft = true;
        }
    }

    void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }

    void PlayDeadSound()
    {
        audioSource.PlayOneShot(deadSound);
    }

    void PlayDeadSound2()
    {
        audioSource.PlayOneShot(deadSound2);
    }

    void PlayAttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }

    void PlayPrepareAttackSound()
    {
        audioSource.PlayOneShot(attackSound2);
    }

    void PlayGuardSound()
    {
        audioSource.PlayOneShot(guardSound);
    }

    void PreventDuplicate()
    {
        executed = false;
    }

}
