using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogBlock : MonoBehaviour
{
    public bool isEdge = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EnvironmentManager envManager = GameObject.Find("GameManager").GetComponent<EnvironmentManager>();
            if(envManager)
            {
                if(isEdge)
                {
                    envManager.ChangeToDefaultEnvironment();
                }
                else
                {
                    envManager.ChangeToFogEnvironment();
                }
            }
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        if (changesFogEnvironment)
    //        {
    //            EnvironmentManager envManager = GameObject.Find("GameManager").GetComponent<EnvironmentManager>();
    //            if (envManager)
    //            {
    //                envManager.ChangeToDefaultEnvironment();
    //            }
    //        }
    //    }
    //}
}
