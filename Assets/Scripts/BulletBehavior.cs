using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField]
    float _speed;
    float _damage;
    float _life = 1.5f;
    float timer = 0.0f;
    Rigidbody rB;
    bool begin;
    // Start is called before the first frame update
    void Awake()
    {
        rB = GetComponent<Rigidbody>();
        //rB.useGravity = false;
    }
    public void ApplyVelocity()
    {
        //begin = true;
        if (!rB.useGravity)
       // {
            rB.useGravity = true;

        if (_speed > 0)
        rB.AddForce(transform.forward * _speed, ForceMode.Impulse);
        /*if (_speed > 0)
            transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);
        else
        {
#if UNITY_EDITOR
            Debug.Log("Speed is set to zero");
#endif
        }*/
    }

    //}
    public void SetSpeed(float value)
    {
        _speed = value;
    }
    public void SetDamage(float value)
    {
        _damage = value;
    }

    public float GetDamage()
    {
        return _damage;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= _life)
        {
            Destroy(this.gameObject)
;
        }
       // if(begin)
            //transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);
        //ApplyVelocity();
        //rB.AddForce(transform.up * speed * Time.deltaTime, ForceMode.Force);
        //transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
    }

    HealthManager healthManager;
    Rigidbody otherRb;
    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
    }*/
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        healthManager = other.GetComponent<HealthManager>();
        otherRb = other.GetComponent<Rigidbody>();
        if (healthManager)
        {
            healthManager.DealDamage(_damage);

        }
        if (otherRb)
        {
            otherRb.AddForceAtPosition(transform.forward * 1f, transform.position, ForceMode.Impulse);

        }
    }
}
