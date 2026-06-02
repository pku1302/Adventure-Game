using UnityEngine;

public class JiangshiMonster : Monster
{
    [SerializeField]
    private MovementComponent movement;

    public AudioClip angrySound;

    void PlayJiangshiAttackSound()
    {
        if (movement.speed >= 3.0f)
        {
            audioSource.PlayOneShot(angrySound);
        }
        else
        {
            audioSource.PlayOneShot(attackSound);
        }
    }
}
