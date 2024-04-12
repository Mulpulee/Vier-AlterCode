using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader instance;

    [SerializeField] private AnimationClip[] m_clips;
    private Animation m_anim;
    private Image m_image;

    private void Awake()
    {
        if (instance == null) instance = this;
        m_anim = GetComponent<Animation>();
        m_image = GetComponent<Image>();
    }

    public void Init(bool isBlack = false)
    {
        m_image.color = isBlack ? Color.black : Color.clear;
    }

    public Coroutine FadeIn(Action pNext)
    {
        m_anim.clip = m_clips[0];
        return StartCoroutine(FadeRoutine(pNext));
    }
    public Coroutine FadeOut(Action pNext)
    {
        m_anim.clip = m_clips[1];
        return StartCoroutine(FadeRoutine(pNext));
    }

    private IEnumerator FadeRoutine(Action pNext)
    {
        m_anim.Play();

        yield return new WaitForSeconds(0.5f);
        if (m_anim.clip == m_clips[1]) yield return new WaitUntil(() => !m_anim.isPlaying);

        pNext.Invoke();
    }
}
