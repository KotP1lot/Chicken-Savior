using UnityEngine;

public class music : MonoBehaviour
{
    void Start()
    {
        float volume = PlayerPrefs.GetFloat("volume");
        AudioListener.volume = volume;
    }
}
