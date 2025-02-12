using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    GameObject info_box;
    public Item item;
    public GameObject button;

    private void Awake()
    {
        info_box = transform.parent.parent.parent.GetChild(1).gameObject;
    }

    public void DisplayInfo()
    {
        info_box.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.name;
        info_box.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.description;

        Transform choises = transform.parent.parent.parent.GetChild(2);
        Utils.ClearChildren(choises);

        for (int i = 0; i < item.uses.Count; i++)
        {
            int index = i;
            GameObject new_button = Instantiate(button, choises);
            new_button.GetComponent<Button>().onClick.AddListener(
                () => item.uses[index].Invoke()
            );
            new_button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.use_names[i];
        }
    }
}
