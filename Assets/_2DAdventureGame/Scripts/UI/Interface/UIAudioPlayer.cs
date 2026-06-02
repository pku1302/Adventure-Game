using UnityEngine;

public class UIAudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clickClip;

    [SerializeField]
    private AudioClip hoverClip;

    [SerializeField]
    private AudioClip itemClip;

    [SerializeField]
    private AudioClip sellClip;

    [SerializeField]
    private AudioClip buyClip;

    [SerializeField]
    private AudioClip enhanceClip;

    [SerializeField]
    private AudioClip equipClip;

    [SerializeField]
    private AudioClip cancelClip;

    [SerializeField]
    private AudioClip mapClip;


    public void PlayClick()
    {
        audioSource.PlayOneShot(clickClip);
    }

    public void PlayEquip()
    {
        audioSource.PlayOneShot(equipClip);
    }

    public void PlayBuy()
    {
        audioSource.PlayOneShot(buyClip);
    }

    public void PlaySell()
    {
        audioSource.PlayOneShot(sellClip);
    }

    public void PlayItem()
    {
        audioSource.PlayOneShot(itemClip);
    }

    public void PlayHover()
    {
        audioSource.PlayOneShot(hoverClip);
    }

    public void PlayEnhance()
    {
        audioSource.PlayOneShot(enhanceClip);
    }

    public void PlayCancel()
    {
        audioSource.PlayOneShot(cancelClip);
    }

    public void PlayMap()
    {
        audioSource.PlayOneShot(mapClip);
    }
}

