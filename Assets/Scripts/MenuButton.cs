using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public GameObject character;
    public GameObject[] buttonPrefabs;
    public GameObject canvas;
    public Vector3 distanseToNearButton = new Vector3();
    public float buttonsScale = 0.4f;

    private GameObject[] buttons;
    private bool opened = false;

    public void OpenMenu()
    {
        if (!opened)
        {
            buttons = new GameObject[3];
            for (int i = 0; i < buttonPrefabs.Length; i++)
            {
                buttons[i] = CreateButton(i);
            }
            opened = true;
        } else
        {
            for (int i = 0; i < buttonPrefabs.Length; i++)
            {
                GrepButton(i);
            }
            opened = false;
        }
    }
    private GameObject CreateButton(int indexOfPrefabButton)
    {
        GameObject button = Instantiate(buttonPrefabs[indexOfPrefabButton]);
        button.transform.SetParent(canvas.transform);
        button.transform.SetAsFirstSibling();
        Vector3 localPosition = new Vector3(GetComponent<RectTransform>().localPosition.x,
                                            GetComponent<RectTransform>().localPosition.y,
                                            GetComponent<RectTransform>().localPosition.z);
        button.GetComponent<RectTransform>().localPosition = localPosition;
        button.GetComponent<RectTransform>().localScale = new Vector3(buttonsScale,
                                                                      buttonsScale, 
                                                                      buttonsScale);
        Vector3 targetPoint = distanseToNearButton * (1 + indexOfPrefabButton) + button.GetComponent<RectTransform>().localPosition;
        button.GetComponent<MenusButtonMovingAnimation>().Play(targetPoint);

        if (button.TryGetComponent<RestartButton>(out RestartButton restartButton))
        {
            restartButton.character = character;
        }

        return button;
    }
    private void GrepButton(int indexOfButton)
    {
        Vector3 targetPoint = new Vector3(GetComponent<RectTransform>().localPosition.x,
                                          GetComponent<RectTransform>().localPosition.y,
                                          GetComponent<RectTransform>().localPosition.z);
        buttons[indexOfButton].GetComponent<MenusButtonMovingAnimation>().Play(targetPoint);
        Destroy(buttons[indexOfButton], 2f);
    }
}
