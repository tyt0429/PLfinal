using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallemove : MonoBehaviour {
	
	
	public GameObject head;
	public float mouseSensitivity = 1f;
    public float movespeed = 0.2f;
	public float rotateSpeed = 60f;
    public float jumping = 3f;
    bool isGrounded = false;
    float distToGround = 0f;

    public void move(Vector3 v)
    {
        transform.localPosition += movespeed * v;
    }

    public void rotate(float deltaX,float deltaY)
    {
        Vector3 angle = head.transform.localRotation.eulerAngles;
        angle.x += deltaX;
        angle.y += deltaY;
        if (angle.x > 90f && angle.x < 270f)
        {
            if(deltaX > 0)
            {
                angle.x = 90f;
            }else{
                angle.x = 270f;
            }
        }
        head.transform.localRotation = Quaternion.Euler(angle);
    }
    public void face(Vector3 pos)
    {
        Vector3 v = Quaternion.LookRotation(
            pos - transform.position,
            transform.up
            ).eulerAngles;
        transform.localRotation = Quaternion.Euler(v.x, v.y, 0);
    }
    void Start () {
        //distToGround = GetComponent<Collider>().bounds.extents.y + 0.5f;
		distToGround = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distToGround);
		domovement();
		dorotation();
		action();
	}

    public bool grounded()
    {
        return isGrounded;
    }

    public void jump(Vector3 v)
    {
        GetComponent<Rigidbody>().velocity = jumping * (Vector3.up + v);
    }
	void domovement()
    {
        if (!grounded()) return;
        Quaternion rotY = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        Vector3 v = Input.GetAxis("Vertical")*(rotY*Vector3.forward);
		Vector3 angle = transform.localRotation.eulerAngles;
		angle.y += Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
		transform.localRotation = Quaternion.Euler(angle);
        if (Input.GetButton("Jump")) jump(v);
        else move(v);
    }
    void dorotation()
    {
        rotate(
            -mouseSensitivity * Input.GetAxis("Mouse Y"),
             mouseSensitivity * Input.GetAxis("Mouse X")
            );
    }
	void action()
	{
		if (Input.GetKeyDown("c")){
			transform.GetChild(2).transform.localPosition = 0.15f*Vector3.up;
			transform.GetChild(3).transform.localPosition = 0.5f*Vector3.up;
			movespeed = 0.1f;
		}
		if (Input.GetKeyDown("x")){
			transform.GetChild(2).transform.localPosition = 0.46f*Vector3.up;
			transform.GetChild(3).transform.localPosition = 0.95f*Vector3.up;
			movespeed = 0.2f;
		}
		if (Input.GetKeyDown(KeyCode.Mouse0)){
			Vector3 v = transform.GetChild(2).GetChild(2).transform.localRotation.eulerAngles;
			v.y+=1f*Time.deltaTime;
			transform.GetChild(2).GetChild(2).transform.localRotation = Quaternion.Euler(v);
			//transform.GetChild(3).transform.localPosition = 0.5f*Vector3.up;
		}
	}
}
