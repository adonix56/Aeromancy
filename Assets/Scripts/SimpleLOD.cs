using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLOD : MonoBehaviour
{
    [SerializeField] private float lodDistance;
    private MeshRenderer meshRenderer;
    private Transform cameraLocation;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        cameraLocation = Camera.main.transform;
        meshRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.enabled = Vector3.Distance(transform.position, cameraLocation.position) < lodDistance;
    }
}
