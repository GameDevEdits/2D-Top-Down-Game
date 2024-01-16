using UnityEngine;

public class EnemyWallCollider : MonoBehaviour
{
    // Specify the layer that represents enemies in the Unity Inspector
    public LayerMask enemyLayer;

    private void Start()
    {
        // Ignore collisions between this object's collider and all colliders on the enemy layer
        Physics.IgnoreLayerCollision(gameObject.layer, enemyLayer, true);
    }
}
