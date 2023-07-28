using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LixoGptShootManager : MonoBehaviour
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
    float _damage;
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
            _damage = weaponSettings.GetBulletDamage();
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

    Coroutine currentFireRateCoroutine; // Update the type to Coroutine

    void Fire()
    {
        if (currentFireRateCoroutine != null)
        {
            return; // Don't fire if already in the process of firing.
        }

        if (_bullet == null)
        {
            Debug.LogError("Bullet prefab is null. Make sure it's assigned in the inspector.");
            currentFireRateCoroutine = null; // Reset the coroutine reference.
            return;
        }

        GameObject go = Instantiate(_bullet, _bulletSpawn.position, _bulletSpawn.rotation);

        if (go == null)
        {
            Debug.LogError("Instantiated bullet is null.");
            currentFireRateCoroutine = null; // Reset the coroutine reference.
            return;
        }

        BulletBehavior bulletBehavior = go.GetComponent<BulletBehavior>();

        if (bulletBehavior != null)
        {
            bulletBehavior.SetSpeed(_bulletVelocity);
            bulletBehavior.SetDamage(_damage);
            bulletBehavior.ApplyVelocity();
        }

        currentFireRateCoroutine = StartCoroutine(FireRate());
    }

    IEnumerator FireRate()
    {
        shoot = false;

        yield return new WaitForSeconds(_fireRate);

        shoot = true;
        currentFireRateCoroutine = null; // Reset the coroutine reference.

        yield return null;
    }
}