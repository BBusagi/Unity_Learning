using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;

    //private float waitingSeconds = Constants.DEFAULT_WAITING_SECONDS;

    private Coroutine typingCorouting;

    private bool isTyping;
    public bool IsTyping => isTyping;




    public void StartTyping(string text)
    {
        if (typingCorouting != null)
        {
            StopCoroutine(typingCorouting);
        }

        typingCorouting = StartCoroutine(TypeLine(text));
    }

    private IEnumerator TypeLine(string text)
    {
        isTyping = true;
        textDisplay.text = text;
        textDisplay.maxVisibleCharacters = 0;

        foreach (char _ in text)
        {
            textDisplay.maxVisibleCharacters++;
            yield return new WaitForSeconds(Constants.TYPEWRITER_WAITING_SECONDS);
        }
        // for (int i = 0; i <= text.Length; i++)
        // {
        //     textDisplay.maxVisibleCharacters = i;
        //     yield return new WaitForSeconds(Constants.DEFAULT_WAITING_SECONDS);
        // }

        isTyping = false;
    }

    public void completeTyping()
    {
        if (typingCorouting != null)
        {
            StopCoroutine(typingCorouting);
        }

        textDisplay.maxVisibleCharacters = textDisplay.text.Length;
        isTyping = false;
    }
}
