using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    public PlayerStamina stamina;
    public Image staminaBar;
    public GameObject root;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        staminaBar.fillAmount = stamina.currentStamina / stamina.maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        float ratio = stamina.currentStamina / stamina.maxStamina;
        staminaBar.fillAmount = ratio;
        root.SetActive(ratio < 1f);
    }
}
