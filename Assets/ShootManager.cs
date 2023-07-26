using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    public WeaponSettings weaponSettings;
    [SerializeField]
    GameObject _bullet;
    [SerializeField]
    Transform _bulletSpawn;
    //[SerializeField]
    float _fireRate;
    bool shoot;
    //[SerializeField]
    float _bulletVelocity;
    float _bulletDamage;
    [Header("Debug")]
    [SerializeField]
    bool auto;
    // Start is called before the first frame update
    private void Awake()
    {
        if (weaponSettings != null)
        {
            _fireRate = weaponSettings.GetFireRate();
            _bulletVelocity = weaponSettings.GetBulletSpeed();
            _bulletDamage = weaponSettings.GetBulletDamage();
        }
    }
    void Start()
    {
        shoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (auto)
        {
            if (Input.GetMouseButton(0))
            {
                if (shoot)
                {
                    Fire();
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (shoot)
                {
                    Fire();
                }
            }
        }
    }

    void Fire()
    {
        GameObject go = Instantiate(_bullet, _bulletSpawn.position, _bulletSpawn.rotation);
        BulletBehavior bulletBehavior = go.GetComponent<BulletBehavior>();
        if (bulletBehavior != null)
        {
            bulletBehavior.SetDamage(_bulletDamage);
            bulletBehavior.SetSpeed(_bulletVelocity);
            bulletBehavior.ApplyVelocity();
        }
        StartCoroutine(FireRate());
    }
    IEnumerator FireRate()
    {
        shoot = false;

        yield return new WaitForSeconds(_fireRate);

        shoot = true;

        yield return null;
    }
}
