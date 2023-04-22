using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogBlock : MonoBehaviour
{
    public bool isEdge = true;

    private void OnTriggerEnter(Collider other)
    {
        FogReaction fogReaction = other.gameObject.GetComponent<FogReaction>();
        if (fogReaction != null)
        {

            fogReaction.EnterFogBlock(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FogReaction fogReaction = other.gameObject.GetComponent<FogReaction>();
        if (fogReaction != null)
        {

            fogReaction.ExitFogBlock(this);
        }
    }
}
