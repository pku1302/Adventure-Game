using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashLightToggle : MonoBehaviour
{
    public Light2D flashlight;
    public Collider2D hitbox;

    public float maxBattery = 100f;
    public float battery;
    public float drainSpeed = 1f;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip flashlightSFX;
    [SerializeField]
    private AudioClip errorSFX;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        battery = maxBattery;
        flashlight.enabled = false;
        hitbox.enabled = false;
    }

    private void Update()
    {
        if (gameManager.CurrentState == GameState.GamePlay && InputManager.Instance.WasMouseRightClicked)
        {
            ToggleFlashlight();
        }

        if (flashlight.enabled)
        {
            battery -= Time.deltaTime;

            if (battery <= 0)
            {
                battery = 0;
                flashlight.enabled = false;
            }
        }
    }

    public void ToggleFlashlight()
    {
        if (battery > 0)
        {
            audioSource.PlayOneShot(flashlightSFX);
            flashlight.enabled = !flashlight.enabled;
            hitbox.enabled = !hitbox.enabled;
        }
        else
        {
            audioSource.PlayOneShot(errorSFX);
        }
    }


    
}
