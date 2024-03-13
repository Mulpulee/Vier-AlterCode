using Entity;
using Entity.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpDisplayer : MonoBehaviour {
    [SerializeField] private Image m_image;

    void Update()
    {
        EntityBehaviour player = PlayerEvent.Instance.Player;
        if (player == null) {
            return;
        }

        m_image.fillAmount = player.Status.Health / player.Status.OriginHealth.MaxValue;
    }
}
