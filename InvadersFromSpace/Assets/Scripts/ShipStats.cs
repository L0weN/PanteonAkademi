using System;
using UnityEngine;

[System.Serializable]
public class ShipStats
{
    [Range(1, 5)] public int maxHealth;
    public int currentHealth;
    public int maxLifes = 3;
    public int currentLifes = 3;
    public float shipSpeed;
    public float fireRate;
}
