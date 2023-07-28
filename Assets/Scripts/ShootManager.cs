using System;
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
    public Ray _ray;
    [SerializeField]
    TrailRenderer bulletTrail;
    // Start is called before the first frame update
    private void Awake()
    {
        if (weaponSettings != null)
        {
            _fireRate = weaponSettings.GetFireRate();
            _bulletVelocity = weaponSettings.GetBulletSpeed();
            _bulletDamage = weaponSettings.GetBulletDamage();
        }

        _ray.origin = _bulletSpawn.position;
        //_ray.direction = transform.forward;
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
        RaycastHit hit;
        _ray.origin = _bulletSpawn.position;
        _ray.direction = transform.forward;

        if (Physics.Raycast(_ray.origin, _ray.direction, out hit, Mathf.Infinity))
        {

#if UNITY_EDITOR

            float distance = Vector3.Distance(_ray.origin, hit.point);

            Debug.DrawRay(_ray.origin, _ray.direction * distance, Color.blue);
            Debug.Log(hit.transform.name);
#endif

            //if (hit.rigidbody)
              //  hit.rigidbody.AddForceAtPosition(transform.forward * 50f, hit.point, ForceMode.Force);
            TrailRenderer trail = Instantiate(bulletTrail, _bulletSpawn.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit/*, false*/));
            //ApplyDamage(hit.transform.gameObject);
        }
        else
        {
#if UNITY_EDITOR
            Debug.DrawRay(_ray.origin, _ray.direction * 1000f, Color.blue);
#endif
            TrailRenderer trail = Instantiate(bulletTrail, _bulletSpawn.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit/*, true*/));
        }

        //GameObject go = Instantiate(_bullet, _bulletSpawn.position, _bulletSpawn.rotation);
        //BulletBehavior bulletBehavior = go.GetComponent<BulletBehavior>();
        //if (bulletBehavior != null)
        //{
        //  bulletBehavior.SetDamage(_bulletDamage);
        //bulletBehavior.SetSpeed(_bulletVelocity);
        //bulletBehavior.ApplyVelocity();
        //}
        StartCoroutine(FireRate());
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit/*, bool noHit*/)
    {

        float time = 0;
        Vector3 startPostion = trail.transform.position;
        /*if (!noHit)
        {
            while (time < 1)
            {
                trail.transform.position = Vector3.Lerp(startPostion, hit.point, time);
                time += Time.deltaTime / trail.time;
                yield return null;
            }
            trail.transform.position = hit.point;
            Destroy(trail.gameObject, trail.time);
        }
        else
        {*/
            Vector3 endPostion = _ray.direction * 100f;
            while (time < 1)
            {
                trail.transform.position = Vector3.Lerp(startPostion, endPostion, time);
                time += Time.deltaTime / trail.time ;
                yield return null;
            }
            trail.transform.position = endPostion;
        if (hit.transform)
        {
            ApplyDamage(hit.transform.gameObject);
            if (hit.rigidbody)
                hit.rigidbody.AddForceAtPosition(transform.forward * 50f, hit.point, ForceMode.Force);
        }
        Destroy(trail.gameObject, trail.time);
        yield return null;

        // }
        //throw new NotImplementedException();
    }

    void ApplyDamage(GameObject obj)
    {

        HealthManager healthManager;
        healthManager = obj.GetComponent<HealthManager>();


        if (healthManager != null)
        {
            healthManager.DealDamage(_bulletDamage);
        }
        else
        {
            return;
        }
    }
    IEnumerator FireRate()
    {
        shoot = false;

        yield return new WaitForSeconds(_fireRate);

        shoot = true;

        yield return null;
    }
}
