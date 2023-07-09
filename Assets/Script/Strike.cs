using Unity.VisualScripting;
using UnityEngine;

public class Strike : MonoBehaviour
{
    EventSyst syst;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            Debug.Log("STRIKE");
            syst.OnDead?.Invoke(TypeDie.Car);
        }
    }
    private void OnEnable()
    {
        syst = GameObject.Find("EventSys").GetComponent<EventSyst>();
    }
}
