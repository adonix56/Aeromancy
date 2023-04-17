using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Blowable))]
public class Pushable : MonoBehaviour
{
    private Blowable blowable;
    private Rigidbody rb;

    private Vector3 currentVelocity;
    private float currentCollisions;
    private float separationOnCollision = 0.1f;

    // Start is called before the first frame update
    private void Awake()
    {
        blowable = GetComponent<Blowable>();
        rb = GetComponent<Rigidbody>();
        blowable.OnBlowEnter += OnBlowEnter;
        blowable.OnBlowExit += OnBlowExit;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        if (currentCollisions > 0)
            return;
        transform.position += currentVelocity * Time.deltaTime;
    }

    public void OnBlowEnter(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            currentVelocity = GetMainAxisDirection(direction) / rb.mass;
        }
    }

    public void OnBlowExit()
    {
        currentVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            return;
        currentCollisions++;
        for(int i = 0; i < collision.contactCount; i++)
        {
            transform.position += -currentVelocity * separationOnCollision;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        currentCollisions--;
    }

    private Vector3 GetMainAxisDirection(Vector3 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y) && Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
        {
            return new Vector3(direction.x, 0, 0);
        }
        else if (Mathf.Abs(direction.y) > Mathf.Abs(direction.z))
        {
            return new Vector3(0, direction.y, 0);
        }
        return new Vector3(0, 0, direction.z);
    }
}
