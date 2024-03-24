using UnityEngine;

public class IgnorePlayerBulletCollisions : MonoBehaviour
{
    // Specify the layer that represents player bullets
    public LayerMask playerBulletLayer;

    void Start()
    {
        // Ignore collisions with objects on the playerBulletLayer
        Physics.IgnoreLayerCollision(gameObject.layer, playerBulletLayer, true);
    }

    // If you want to dynamically change the behavior, you can use the following method
    public void SetIgnorePlayerBulletCollisions(bool ignore)
    {
        Physics.IgnoreLayerCollision(gameObject.layer, playerBulletLayer, ignore);
    }
}
