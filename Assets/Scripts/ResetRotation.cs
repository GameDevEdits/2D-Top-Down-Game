using UnityEngine;

public class ResetRotation : MonoBehaviour
{
    void Update()
    {
        // Set the rotation to zero
        transform.rotation = Quaternion.identity;
    }
}