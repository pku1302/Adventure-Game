using System;
using TMPro;
using UnityEngine;

public class ConfirmUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text messageText;
    
    private Action onYes;
    private Action onNo;

    public void Open(string text, Action yesAction, Action noAction = null)
    {
        gameObject.SetActive(true);
        messageText.text = text;

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
