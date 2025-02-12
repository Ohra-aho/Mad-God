using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    GameObject true_menu;

    private void Start()
    {
        transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);

    }

    public void DisplayMenu()
    {
        transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeInHierarchy);
        true_menu = transform.GetChild(0).gameObject;
        DisplaySubMenu(3);
    }

    public void DisplaySubMenu(int child_index)
    {
        for(int i = 3; i < 6; i++)
        {
            true_menu.transform.GetChild(i).gameObject.SetActive(false);
            true_menu.transform.GetChild(i - 3).GetComponent<RectTransform>().sizeDelta =
                new Vector2(
                    56,
                    true_menu.transform.GetChild(i - 3).GetComponent<RectTransform>().sizeDelta.y
                );
            true_menu.transform.GetChild(child_index - 3).GetChild(0).GetComponent<TextMeshProUGUI>().margin = new Vector4(16, 0, 16, 0);

        }

        true_menu.transform.GetChild(child_index).gameObject.SetActive(true);
        true_menu.transform.GetChild(child_index - 3).GetComponent<RectTransform>().sizeDelta = 
            new Vector2(
                70,
                true_menu.transform.GetChild(child_index - 3).GetComponent<RectTransform>().sizeDelta.y
            );
        true_menu.transform.GetChild(child_index - 3).GetChild(0).GetComponent<TextMeshProUGUI>().margin = new Vector4(30, 0, 30 ,0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
