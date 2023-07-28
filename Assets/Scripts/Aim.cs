using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    ///[SerializeField]
    //GameObject _bullet;
    [SerializeField]
    Vector3 _screenPosi;
    [SerializeField]
    Vector3 _worldPosi;
    [SerializeField]
    Transform _target;
    //[SerializeField]
    //Transform _bulletSpawn;
    [SerializeField]
    float zOffset;
    float _mouseX = 0;
    float _mouseY = 0;
    float sensitivity = 5;

    //[SerializeField]
    //float _fireRate;
    //bool shoot;

    [SerializeField]
    bool useLookAt;
    // Start is called before the first frame update
    void Start()
    {
        //zOffset = transform.position.z;
        //shoot = true;
        //Cursor.lockState = CursorLockMode.Locked;
        ///Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        #region Rotation With LookAt
        if (useLookAt)
        {
            //LookAt
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");
#if UNITY_EDITOR
            //Debug.Log("mouse x: " + h);
            //Debug.Log("mouse y: " + v);
#endif
            _screenPosi = Input.mousePosition;
            _screenPosi.z = Camera.main.nearClipPlane + zOffset;
            _worldPosi = Camera.main.ScreenToWorldPoint(_screenPosi);
            _target.position = _worldPosi;
            Quaternion OriginalRot = transform.rotation;
            transform.LookAt(_target, Vector3.up);

            Quaternion NewRot = transform.rotation;
            transform.rotation = OriginalRot;
            transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, sensitivity * Time.deltaTime);

            float ry = transform.eulerAngles.y;
            float rx = transform.eulerAngles.x;

            if (ry >= 180) ry -= 360;
            if (rx >= 180) rx -= 360;

            transform.eulerAngles = new Vector3(
                Mathf.Clamp(rx, -10, 10),
                Mathf.Clamp(ry, -60, 60),
                0
            );
        }
        #endregion

        #region Rotation Without LookAt
        if (!useLookAt)
        {
            //rotation without lookat
            _mouseX += Input.GetAxis("Mouse X") * sensitivity;
            _mouseX = Mathf.Clamp(_mouseX, -60, 60);

            _mouseY -= Input.GetAxis("Mouse Y") * sensitivity;
            _mouseY = Mathf.Clamp(_mouseY, -10, 10);

            transform.localRotation = Quaternion.Euler(_mouseY, _mouseX, 0);

            float ry = transform.eulerAngles.y;
            float rx = transform.eulerAngles.x;

            if (ry >= 180) ry -= 360;
            if (rx >= 180) rx -= 360;

            transform.eulerAngles = new Vector3(
                Mathf.Clamp(rx, -10, 10),
                Mathf.Clamp(ry, -60, 60),
                0
            );
        }
        #endregion
  
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_target.position, 1f);
        Gizmos.color = Color.red;
    }
}
