using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public float sensitivity = 90f;
    [SerializeField] private Transform body;
    [SerializeField] private PlayerMovementRigidbody player;
    private float xRotation = 0f;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        //cursor locking
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Confined;
        }

        float lookX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float lookVertical = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= lookVertical;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        body.Rotate(Vector3.up * lookX);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
