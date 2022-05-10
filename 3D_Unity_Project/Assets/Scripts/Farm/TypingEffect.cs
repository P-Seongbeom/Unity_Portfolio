using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    public int CharPerSeconds;
    public GameObject TalkCursor;
    string _targetMassage;
    Text _messageText;
    int index;
    float interval;

    private void Awake()
    {
        _messageText = GetComponent<Text>();
    }

    public void SetMessage(string msg)
    {
        _targetMassage = msg;
        EffectStart();
    }

    void EffectStart()
    {
        _messageText.text = "";
        index = 0;
        TalkCursor.SetActive(false);
        interval = 1.0f / CharPerSeconds;

        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        if(_messageText.text == _targetMassage)
        {
            EffectEnd();
            return;
        }

        _messageText.text += _targetMassage[index];
        ++index;

        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        TalkCursor.SetActive(true);
    }
}
