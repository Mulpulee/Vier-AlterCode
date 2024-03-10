using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ChangeTextOnClick : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI textComponent;
    private bool isListeningForKey = false;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();

        if (textComponent == null)
        {
            UnityEngine.Debug.LogError("TextMeshPro component is missing!");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isListeningForKey = true;
    }

    void Update()
    {
        if (isListeningForKey)
        {
            if (Input.anyKeyDown)
            {
                string inputKey = Input.inputString;

                textComponent.text = inputKey;

                UnityEngine.Debug.Log(inputKey);

                isListeningForKey = false;
            }
        }
    }
}
