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

    public bool EndEffect;
    private bool canSkip = false;
    private float _effectTime;

    private void Awake()
    {
        _messageText = GetComponent<Text>();
    }

    private void Update()
    {
        if(canSkip)
        {
            _effectTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0) && _effectTime > 0.3)
        {
            _messageText.text = _targetMassage;
            EffectEnd();
            return;
        }
    }

    public void SetMessage(string msg)
    {
        _targetMassage = msg;
        EffectStart();
    }

    void EffectStart()
    {
        EndEffect = false;
        canSkip = true;
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
        canSkip = false;
        _effectTime = 0;
        EndEffect = true;
        TalkCursor.SetActive(true);
    }
}
