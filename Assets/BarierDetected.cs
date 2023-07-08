using UnityEngine;

public class BarierDetected : MonoBehaviour
{
    private bool isDetected = false;
    public bool IsBarier() 
    {
        return isDetected;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6) 
        {
            isDetected = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            isDetected = false;
        }
    }
}
