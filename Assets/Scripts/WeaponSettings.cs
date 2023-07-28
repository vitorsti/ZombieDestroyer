using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewWeaponSettings", menuName = "ScriptableObjects/WeaponSettings", order = 1)]
public class WeaponSettings : ScriptableObject
{
    [SerializeField]
    float _bulletSpeed;
    [SerializeField]
    float _rateOfFire;
    public float GetBulletSpeed()
    {
        return _bulletSpeed;
    }

    [Tooltip("ROF - Rate of Fire in Rounds/Min")]
    public float GetFireRate()
    {
        return 60f/ _rateOfFire;
    }
}
