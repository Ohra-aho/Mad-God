using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static void ClearChildren(Transform target)
    {
        for (int i = target.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(target.GetChild(i).gameObject);
        }
    }
}
