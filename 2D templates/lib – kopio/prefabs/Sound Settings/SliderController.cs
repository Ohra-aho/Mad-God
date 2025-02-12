using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public string name; //Identifier of the slider. This connects slider to settings
    public float value;
    public bool mute;

    //Setting requirements
    public bool muteNeeded = true;
    public string settings = "EventSystem";

    //public Sprite
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        transform.GetChild(1).GetComponent<Slider>().value = value;
        if(!muteNeeded)
        {
            transform.GetChild(3).gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        DisplayValues();
    }

    private void DisplayValues()
    {
        if(value != transform.GetChild(1).GetComponent<Slider>().value)
        {
            value = transform.GetChild(1).GetComponent<Slider>().value;
        }
        if(mute)
        {
            transform.GetChild(3).gameObject.GetComponent<Image>();
        }
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = value.ToString("F2");
        GameObject.Find(settings).GetComponent<SoundSettings>().ChangeValues(name, mute, value);
    }

    public void MuteSound()
    {
        mute = !mute;
    }
}
