using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudSpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] m_sprites;
    private Image m_image;

    private void Start()
    {
        m_image = GetComponent<Image>();
    }

    public void ChangeForm(int type)
    {
        m_image.sprite = m_sprites[type];
    }
}
