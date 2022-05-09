using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMotion : MonoBehaviour
{
    public Animator Animator;

    public Text text;

    [SerializeField]
    private float _time = 1f;
    private bool _fadeIn = true;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(FlickPhrase(_time));
    }

    IEnumerator FlickPhrase(float time)
    {
        yield return new WaitForSeconds(time);
        if(_fadeIn)
        {
            FadeIn();
            StartCoroutine(FlickPhrase(_time));
        }
        else if(false == _fadeIn)
        {
            FadeOut();
            StartCoroutine(FlickPhrase(_time));
        }
    }

    void FadeIn()
    {
        Animator.SetBool("FadeIn", _fadeIn);
        _fadeIn = false;
    }
    
    void FadeOut()
    {
        Animator.SetBool("FadeIn", _fadeIn);
        _fadeIn = true;
    }
}
