using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFix : MonoBehaviour
{
    public void ReRotation()
    {
        transform.rotation = Quaternion.identity;
    }
}
