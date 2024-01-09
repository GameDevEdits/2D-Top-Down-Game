using UnityEngine;

namespace BarthaSzabolcs.Tutorial_SpriteFlash.Example
{
    public class SimpleFlashExample : MonoBehaviour
    {
        [SerializeField] private SimpleFlash flashEffect;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if the collision involves the specific object or tag you want.
            if (collision.gameObject.CompareTag("Bullet 3"))
            {
                // Perform your desired action here.
                // For example, you can make the GameObject disappear.
                flashEffect.Flash(); // Disable the GameObject.
                                             // You can also destroy it: Destroy(gameObject);
            }
        }
    }
}