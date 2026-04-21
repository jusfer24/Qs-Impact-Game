using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class CameraController : MonoBehaviour {

    public Transform objetivo;
    public float speedCam = 0.020f;
    public Vector3 movement;

    private void LateUpdate()
    {
        float direccionX = Input.GetAxisRaw("Horizontal");
        if (direccionX < 0)
        {
            movement.x = -7.37f;
        }
        else if (direccionX > 0)
        {
            movement.x = 7.37f;
        }
        Vector3 movementW = objetivo.position + movement;
        Vector3 movementSmoothly = Vector3.Lerp(transform.position, movementW, speedCam);
        transform.position = movementSmoothly;
       
    }
}
