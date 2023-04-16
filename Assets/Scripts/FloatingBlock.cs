using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBlock : MonoBehaviour
{
    public float floatingRange;
    public float floatingPeriod;

    // Start is called before the first frame update
    void Start()
    {
        float randomTimer = Random.Range(0, floatingPeriod);
        Invoke("InitMovement", randomTimer); // prevent floating blocks to move unison uniformly
    }

    private void InitMovement()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - floatingRange, transform.position.z);
        transform.LeanMoveY(transform.position.y + floatingRange, floatingPeriod).setEaseInOutQuad().setLoopPingPong();
    }
}
