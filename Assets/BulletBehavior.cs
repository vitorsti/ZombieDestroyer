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
    // Start is called before the first frame update
    void Awake()
    {
        rB = GetComponent<Rigidbody>();
        //rB.useGravity = false;
    }
    public void ApplyVelocity()
    {
        //if (!rB.useGravity)
        //{
        //    rB.useGravity = true;

        if (_speed > 0)
            rB.AddForce(transform.up * _speed, ForceMode.Impulse);
            //transform.Translate(transform.up * _speed * Time.deltaTime, Space.World);
        else
        {
#if UNITY_EDITOR
            Debug.Log("Speed is set to zero");
#endif
        }
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

        if(timer >= _life)
        {
            Destroy(this.gameObject)
;        }
        //ApplyVelocity();
        //rB.AddForce(transform.up * speed * Time.deltaTime, ForceMode.Force);
        //transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
    }


    HealthManager healthManager;
    Rigidbody otherRb;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider)
        {
            healthManager = collision.gameObject.GetComponent<HealthManager>();
            if (healthManager)
            {
                healthManager.DealDamage(_damage);
                //this.GetComponent<Collider>().isTrigger = false;
                //Destroy(this.gameObject);
            }
        }
    }

  /* private void OnTriggerEnter(Collider other)
    {
        //if (other)
        //{
        Debug.Log(other.name);
            healthManager = other.GetComponent<HealthManager>();
            otherRb = other.GetComponent<Rigidbody>();
            if (healthManager)
            {
                healthManager.DealDamage(_damage);
                //this.GetComponent<Collider>().isTrigger = false;
                //Destroy(this.gameObject);
            }
        if (otherRb)
        {
            otherRb.AddForceAtPosition(transform.up * 1f, transform.position, ForceMode.Impulse);

        }
        //}
    }
*/
}
