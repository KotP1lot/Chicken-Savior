using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class menuController : MonoBehaviour
{
    public Slider volumeSlider;
    public TextMeshProUGUI text;
    public TextMeshProUGUI text_v;
    public Slider cursorSlider;
    public GameObject[] buttonsToHide;
    public GameObject buttonBack;
    public int sceneInd;
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 0.5f);
        cursorSlider.value = -PlayerPrefs.GetFloat("cursorSpeed", 0.10f); ;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneInd);
    }
    public void ToggleVolumeSlider()
    {
            buttonBack.gameObject.SetActive(true);
            cursorSlider.gameObject.SetActive(true);
            text.gameObject.SetActive(true); 
            text_v.gameObject.SetActive(true);

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
            cursorSlider.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
            text_v.gameObject.SetActive(false);
        }
        foreach (GameObject button in buttonsToHide)
        {
            button.SetActive(true);
        }
    }
    public void setCursor()
    {
            PlayerPrefs.SetFloat("cursorSpeed", Mathf.Abs(cursorSlider.value));
            PlayerPrefs.Save();
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
        UnityEngine.Application.Quit();
    }
}
