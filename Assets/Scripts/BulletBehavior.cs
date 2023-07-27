using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField]
    float _speed;
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

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= _life)
        {
            Destroy(this.gameObject)
;        }
        //rB.AddForce(transform.up * speed * Time.deltaTime, ForceMode.Force);
        //transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
    }
}