using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuController : MonoBehaviour
{
    public Slider volumeSlider;
    public GameObject[] buttonsToHide;
    public GameObject buttonBack;
    public int sceneInd;
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = 0.5f;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneInd);
    }
    public void ToggleVolumeSlider()
    {
      
            buttonBack.gameObject.SetActive(true);
        
        if (volumeSlider != null)
        {
            volumeSlider.gameObject.SetActive(!volumeSlider.gameObject.activeSelf);
        }

        foreach (GameObject button in buttonsToHide)
        {
            button.SetActive(false);
        }
    }

    public void ExitOptions()
    {
        if (volumeSlider != null)
        {
            volumeSlider.gameObject.SetActive(!volumeSlider.gameObject.activeSelf);
        }
        if (buttonBack != null)
        {
            buttonBack.gameObject.SetActive(false);
        }
        foreach (GameObject button in buttonsToHide)
        {
            button.SetActive(true);
        }
    }
    public void AdjustVolume()
    {
        if (volumeSlider != null)
        {
            AudioListener.volume = volumeSlider.value;
            PlayerPrefs.SetFloat("volume", AudioListener.volume);
            PlayerPrefs.Save();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
