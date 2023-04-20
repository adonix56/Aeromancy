using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
    public float sensitivity;
    public bool lockMouse;

    // Start is called before the first frame update
    void Start()
    {
        if (lockMouse)
            Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 turn = GameInput.Instance.GetAxis();
        transform.rotation *= Quaternion.AngleAxis(turn.x * 0.5f, Vector3.up);
        transform.rotation *= Quaternion.AngleAxis(-turn.y * 0.5f, Vector3.right);

        //Clamp the Up/Down rotation
        var angles = transform.localEulerAngles;
        angles.z = 0;
        var angle = transform.localEulerAngles.x;

        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }
        transform.localEulerAngles = angles;
    }
}
