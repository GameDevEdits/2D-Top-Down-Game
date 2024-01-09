using UnityEngine;

public class DestroyOnAnim : MonoBehaviour
{
    // Function to be called by an animation event to destroy the GameObject
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
