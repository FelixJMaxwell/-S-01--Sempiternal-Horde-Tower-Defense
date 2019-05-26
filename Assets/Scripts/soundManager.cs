using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class soundManager : MonoBehaviour {

    public static float sfxVolumen = 0;
    public static float MusicVolumen = 0;
    public Slider SoundSlider;
    public Slider MusicSlider;
    public AudioMixer masterMixer;
    public GameObject mainMenu;

    private void Start() {
        if (sfxVolumen != 0) {
            SoundSlider.value = sfxVolumen;
            setSfxLvl(sfxVolumen);
        }
        if (MusicVolumen != 0) {
            MusicSlider.value = MusicVolumen;
            setMusicLvl(MusicVolumen);
        }
    }

    public void okOptionMenuBtn() {
        sfxVolumen = SoundSlider.value;
        MusicVolumen = MusicSlider.value;

        if (mainMenu != null) {
            mainMenu.GetComponent<mainMenu>().optionsPanel.SetActive(false);
            mainMenu.GetComponent<mainMenu>().PanelPrincipal.SetActive(true);
        }
    }

    public void cancelOptionMenuBtn() {
        Debug.Log("No changes made");

        if (mainMenu != null) {
            mainMenu.GetComponent<mainMenu>().optionsPanel.SetActive(false);
            mainMenu.GetComponent<mainMenu>().PanelPrincipal.SetActive(true);
        }
    }

    public void setSfxLvl(float sfxLvl) {
        masterMixer.SetFloat("SFX", sfxLvl);
    }

    public void setMusicLvl(float musicLvl) {
        masterMixer.SetFloat("MusicLvl", musicLvl);
    }
}