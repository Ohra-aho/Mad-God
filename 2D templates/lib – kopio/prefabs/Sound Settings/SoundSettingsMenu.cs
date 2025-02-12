using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettingsMenu : MonoBehaviour
{
    SoundSettings SS;
    public GameObject soundSlider;
    public string settings = "EventSystem";
    // Start is called before the first frame update
    void Start()
    {
        SS = GameObject.Find(settings).GetComponent<SoundSettings>();

        //Get sound targets from SoundSettings and makes settings bar for each sound target
        for(int i = 0; i < SS.soundTargets.Count; i++)
        {
            MakeSettingBar(SS.soundTargets[i]);
        }
        transform.GetChild(0).GetComponent<ScrollView>().AdjustScrollSize();
    }

    private void MakeSettingBar(SoundTarget soundTarget)
    {
        GameObject sliderInstance = Instantiate(soundSlider, transform.GetChild(0).GetChild(0));

        // Modify the properties of the copied slider
        SliderController sliderController = sliderInstance.GetComponent<SliderController>();
        sliderController.value = soundTarget.volume;
        sliderController.mute = soundTarget.mute;
        sliderController.name = soundTarget.name;
    }

    
}



