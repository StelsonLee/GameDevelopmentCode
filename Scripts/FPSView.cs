/*
 * Name: Lee Kong Hue
 * ID: 1303181
 * Course: GV
 *
 * Description
 * To allow player to look around inside the game world with first person perspective view.
 */
using UnityEngine;
using System.Collections;

public class FPSView : MonoBehaviour
{
    public bool xAxes = false;
    public bool yAxes = false;
    public float sensitivityHor = 9.0f;
    public float sensitivityVer = 9.0f;
    public float minimumVer = -45.0f;
    public float maximumVer = 45.0f;

    private float _rotationX;

    void Start()
    {
        _rotationX = 0;
    }
	
    void FixedUpdate()
    {
        if (xAxes)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if (yAxes)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVer;
            _rotationX = Mathf.Clamp(_rotationX, minimumVer, maximumVer);
            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}
