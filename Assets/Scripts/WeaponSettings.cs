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
    [SerializeField]
    float _damage;
    public float GetBulletSpeed()
    {
        return _bulletSpeed;
    }

    [Tooltip("ROF - Rate of Fire in Rounds/Min")]
    public float GetFireRate()
    {
        return _rateOfFire;
    }

    public float GetFireRateMarco()
    {
        return 60f / _rateOfFire;
    }

    public float GetBulletDamage()
    {
        return _damage;
    }
}
