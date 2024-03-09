using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMoving : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;  //딜레이
    [SerializeField] private bool infiniteHorizontal;           //X축 반복
    [SerializeField] private bool infiniteVertical;             //Y축 반복

    private Vector3 lastPosition;
    private float textureUnitSizeX;
    private float textureUnitSizeY;

    void Start()
    {
        lastPosition = Camera.main.transform.position; //pTransform.position
        Texture2D texture = GetComponent<SpriteRenderer>().sprite.texture;
        textureUnitSizeX = texture.width / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit * transform.localScale.x;
        textureUnitSizeY = texture.height / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit * transform.localScale.y;
    }

    void LateUpdate()
    {
        SetFollower(Camera.main.transform);
    }

    void SetFollower(Transform pTransform)
    {
        Vector3 deltaMovement = pTransform.position - lastPosition;
        transform.position += deltaMovement;
        lastPosition = pTransform.position;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.x);

        if (infiniteHorizontal)
        {
            if (Mathf.Abs(pTransform.position.x - transform.position.x) >= textureUnitSizeX)
            {
                float offsetPositionX = (pTransform.position.x - transform.position.x) % textureUnitSizeX;
                transform.position = new Vector3(pTransform.position.x + offsetPositionX, transform.position.y);
            }
        }

        if (infiniteVertical)
        {
            if (Mathf.Abs(pTransform.position.y - transform.position.y) >= textureUnitSizeY)
            {
                float offsetPositionY = (pTransform.position.y - transform.position.y) % textureUnitSizeY;
                transform.position = new Vector3(transform.position.x, pTransform.position.y + offsetPositionY);
            }
        }
    }
}
