using System;
using UnityEngine;

public enum TypeDie 
{
    Car,
    Water
} 
public class EventSyst: MonoBehaviour
{
    public Action<TypeDie> OnDead;
    public Action OnStepForward;
}
