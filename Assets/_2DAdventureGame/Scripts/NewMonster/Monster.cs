using UnityEngine;

[RequireComponent (typeof(Monster))]
public class Monster : MonoBehaviour
{
    [SerializeField] private MonsterData data;
    public MonsterData Data => data;
    public AudioSource audioSource;
    public AudioClip footstepL;
    public AudioClip footstepR;
    public AudioClip hitSound;
    public AudioClip deadSound;
    public AudioClip attackSound;

    private bool isLeft = false;
    private bool executed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void PlayFootStep()
    {
        if (executed) return;
        executed = true;

        if (isLeft)
        {
            audioSource.PlayOneShot(footstepR);
            isLeft = false;
        }
        else
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

    void PlayAttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }

    void PreventDuplicate()
    {
        executed = false;
    }

}
