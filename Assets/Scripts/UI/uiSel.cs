using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class uiSel : MonoBehaviour
{
    public GameObject[] uiObjects;
    public float darkenAmount = 0.2f; 
    public float defaultBrightness = 1.0f; 

    private int selectedIndex = 0;
    private Color[] originalColors; 

    void Start()
    {
        originalColors = new Color[uiObjects.Length];
        for (int i = 0; i < uiObjects.Length; i++)
        {
            originalColors[i] = uiObjects[i].GetComponent<Image>().color;
        }

        SetSelectedUIObject(selectedIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = Mathf.Max(0, selectedIndex - 1);
            SetSelectedUIObject(selectedIndex);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = Mathf.Min(uiObjects.Length - 1, selectedIndex + 1);
            SetSelectedUIObject(selectedIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            UnityEngine.Debug.Log("Selected UI Object: " + uiObjects[selectedIndex].name);
            SceneManager.LoadScene("inGame_ui");
        }
    }

    void SetSelectedUIObject(int index)
    {
        for (int i = 0; i < uiObjects.Length; i++)
        {
            if (i != index)
            {
                uiObjects[i].GetComponent<Image>().color = originalColors[i];
            }
            else
            {
                Color originalColor = originalColors[i];
                Color darkenedColor = originalColor * (1.0f - darkenAmount);
                uiObjects[i].GetComponent<Image>().color = darkenedColor;
            }
        }
    }
}
