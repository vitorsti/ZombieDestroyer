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

    float lastShootTime;
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
                //i//f (shoot)
                //{
                Fire();
                //}
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                //if (shoot)
                //{
                Fire();
                //}
            }
        }
    }

    void Fire()
    {
        /*Debug.DrawRay(_bulletSpawn.position
            , _bulletSpawn.up * 100f, Color.red);*/
        if (lastShootTime + _fireRate < Time.time)
        {
            _ray.direction = _bulletSpawn.forward;

            #region NonRaycast

            GameObject go = Instantiate(_bullet, _bulletSpawn.position, _bulletSpawn.rotation);
            BulletBehavior bulletBehavior = go.GetComponent<BulletBehavior>();
            if (bulletBehavior != null)
            {

                M2HB_Animation_Controller.instance.ShootAnim();
                bulletBehavior.SetDamage(_bulletDamage);
                bulletBehavior.SetSpeed(_bulletVelocity);
                bulletBehavior.ApplyVelocity();
            }

            #endregion

            #region Raycast

           /* M2HB_Animation_Controller.instance.ShootAnim();
            RaycastHit hit;
            Debug.DrawRay(_ray.origin, _ray.direction * 100f, Color.red);
            if (Physics.Raycast(_bulletSpawn.position, _ray.direction, out hit, Mathf.Infinity))
            {
                TrailRenderer newTrail = Instantiate(bulletTrail, _bulletSpawn.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(newTrail, hit,true));
            }
            else
            {
                TrailRenderer newTrail = Instantiate(bulletTrail, _bulletSpawn.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(newTrail, hit,false));
            }
           */

            #endregion

            lastShootTime = Time.time;
        }
        //StartCoroutine(FireRate());
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit, bool haveHit)
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

        Vector3 endPostion;
        if (haveHit)
            endPostion = hit.point;
        else
            endPostion = _ray.direction * 500f;

        //endPostion = endPostion.normalized;
        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPostion, endPostion, time);
            time += Time.deltaTime / trail.time /* _bulletVelocity*/;
            yield return null;
        }
        trail.transform.position = endPostion;
        if (hit.transform)
        {
            ApplyDamage(hit.transform.gameObject);
            if (hit.rigidbody)
                hit.rigidbody.AddForceAtPosition(transform.forward * 50f, hit.point, ForceMode.Force);
            DestroyImmediate(trail.gameObject);
        }
        else
        {
            //Destroy(trail.gameObject, 1f);
            DestroyImmediate(trail.gameObject);
        }
        //
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
