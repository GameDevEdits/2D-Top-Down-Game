using UnityEngine;

public class IgnoreBulletCollisions : MonoBehaviour
{
    // Specify the layer masks for player bullets and enemy bullets
    public LayerMask playerBulletLayer;
    public LayerMask enemyBulletLayer;

    void Start()
    {
        // Ignore collisions between objects on the playerBulletLayer and enemyBulletLayer
        Physics.IgnoreLayerCollision(playerBulletLayer, enemyBulletLayer, true);
    }

    // If you want to dynamically change the behavior, you can use the following method
    public void SetIgnoreBulletCollisions(bool ignore)
    {
        Physics.IgnoreLayerCollision(playerBulletLayer, enemyBulletLayer, ignore);
    }
}
