using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaderAnimation : MonoBehaviour
{
    [SerializeField] private SpriteMask m_mask;
    [SerializeField] private SpriteRenderer m_playerSprite;
    [SerializeField] private Animation m_anim;
    [SerializeField] private AnimationClip[] m_clips;

    private void Awake()
    {
        m_anim.AddClip(m_clips[0], "FadeIn");
        m_anim.AddClip(m_clips[1], "FadeOut");
    }

    private void Update()
    {
        m_mask.sprite = m_playerSprite.sprite;
        if (m_playerSprite.flipX) m_mask.transform.rotation = new Quaternion(0, 180, 0, 0);
        else m_mask.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void FadeIn()
    {
        m_anim.Play("FadeIn");
    }

    public void FadeOut()
    {
        m_anim.Play("FadeOut");
    }
}
