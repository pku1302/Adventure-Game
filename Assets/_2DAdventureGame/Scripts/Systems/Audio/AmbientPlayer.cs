using System.Collections;
using UnityEngine;

public class AmbientPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] ambientSFXs;

    private int audioIndex = 0;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            audioSource.PlayOneShot(ambientSFXs[audioIndex++]);
            yield return new WaitForSeconds(7f);

            if (audioIndex == ambientSFXs.Length)
            {
                audioIndex = 0;
            }
        }
    }
}
