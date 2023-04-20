using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ADONiXSwap : MonoBehaviour
{
    [SerializeField] private GameObject newPrefab;

    [ContextMenu("Swap")]
    public void SwapObjects() {
#if UNITY_EDITOR
        var newInstance = UnityEditor.PrefabUtility.InstantiatePrefab(newPrefab, transform) as GameObject;
        //newInstance.name = gameObject.name;
        newInstance.transform.SetParent(transform.parent, true);
        //newInstance.transform.localScale *= 3;
        DestroyImmediate(gameObject);
#endif
    }
}
