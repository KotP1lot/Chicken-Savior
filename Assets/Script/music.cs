using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float volume = PlayerPrefs.GetFloat("volume");
        AudioListener.volume = volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
