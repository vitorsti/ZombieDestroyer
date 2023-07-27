using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    [SerializeField]
    LayerMask mask;
    [SerializeField]
    LineRenderer lR;
    [SerializeField]

    Vector3 finalPoint;
    [SerializeField]
    Transform initialPoint;
    [SerializeField]
    bool useLaser;
    // Start is called before the first frame update
    void Awake()
    {

        //lR = GetComponent<LineRenderer>();

    }
    private void Start()
    {
        if (lR != null)
            lR.enabled = useLaser;
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (lR != null)
            lR.enabled = useLaser;
#endif
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (useLaser)
        {
            if (lR != null)
                lR.SetPosition(0, initialPoint.position);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5000f, mask))
            {
                if (hit.collider)
                {
                    //finalPoint = hit.point;
                    if (lR != null)
                        lR.SetPosition(1, hit.point);
                }

            }
            else
            {
                //finalPoint = transform.forward * 5000f;
                if (lR != null)
                    lR.SetPosition(1, transform.forward * 2500f);
            }
        }
    }
}
