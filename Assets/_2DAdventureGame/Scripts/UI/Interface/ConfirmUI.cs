using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text messageText;
    [SerializeField]
    private Button cancelButton;

    private Action onYes;
    private Action onNo;

    public void Open(string text, bool isCancelActive, Action yesAction, Action noAction = null)
    {
        gameObject.SetActive(true);
        messageText.text = text;

        if (isCancelActive)
        {
            cancelButton.gameObject.SetActive(true);
        }
        else
        {
            cancelButton.gameObject.SetActive(false);
        }

        onYes = yesAction;
        onNo = noAction;
    }

    public void OnClickYes()
    {
        onYes?.Invoke();

        Close();
    }

    public void OnClickNo()
    {
        onNo?.Invoke();

        Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

}
