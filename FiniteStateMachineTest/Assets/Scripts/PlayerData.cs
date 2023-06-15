using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public float hunger;
    public float maxHunger;

    private void OnEnable()
    {
        hunger = 0;
    }
    public void SetHunger(float value)
    {
        hunger = value;
    }
}
