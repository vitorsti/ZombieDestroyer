using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewWeaponSettings", menuName = "ScriptableObjects/WeaponSettings", order = 1)]
public class WeaponSettings : ScriptableObject
{
    [SerializeField]
    float _bulletSpeed;
    [SerializeField]
    float _fireRate;
    [SerializeField]
    float _damage;
    public float GetBulletSpeed()
    {
        return _bulletSpeed;
    }

    public float GetFireRate()
    {
        return _fireRate;
    }

    public float GetBulletDamage()
    {
        return _damage;
    }
}
