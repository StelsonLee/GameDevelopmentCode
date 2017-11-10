/*
 * Name   : Lee Kong Hue 
 * ID     : 1303181
 * Course : GV
 * 
 * discription 
 * this class is written to allow player game object to move according to player input
 */
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 9.0f;

    public float gravity = -9.8f;
    public float runMultiple = 1.2f;
    public float moveSpeed = 0;
    public float noiseRadiusOri = 5;
    public float noiseMultiple = 2;

    private CharacterController _charController;
    private SphereCollider noiseCollider;

	// Use this for initialization
	void Start ()
    {
        _charController = GetComponent<CharacterController>();
        noiseCollider = this.transform.Find("NoiseMake").transform.GetComponent<SphereCollider>();
        moveSpeed = speed;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = speed * runMultiple;
        }
        else
        {
            moveSpeed = speed;
        }

        //to change the noise radius accordingly to player character stete such as running, walking, or idle.
        if (Input.GetAxis("Horizontal") != 0 && Input.GetKey(KeyCode.LeftShift) || Input.GetAxis("Vertical") != 0 && Input.GetKey(KeyCode.LeftShift))
        {
            noiseCollider.radius = noiseRadiusOri * noiseMultiple;
        }
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            noiseCollider.radius = noiseRadiusOri * (noiseMultiple/ 5);
        }
        else
        {
            noiseCollider.radius = noiseRadiusOri;
        }

        //to move player character according to the input
        float deltaX = Input.GetAxis("Horizontal") * moveSpeed;
        float deltaZ = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, moveSpeed);
        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
    }
}
