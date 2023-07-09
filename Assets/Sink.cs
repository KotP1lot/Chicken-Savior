using UnityEngine;

public class Sink : MonoBehaviour
{
    [SerializeField]ParticleSystem _particleSystem;
    EventSyst syst;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            _particleSystem.transform.position = new Vector3(other.transform.position.x, _particleSystem.transform.position.y, other.transform.position.z);
            _particleSystem.Play();
            syst.OnDead?.Invoke(TypeDie.Water);
        }

    }
    private void OnEnable()
    {
        syst = GameObject.Find("EventSys").GetComponent<EventSyst>();
    }
}
