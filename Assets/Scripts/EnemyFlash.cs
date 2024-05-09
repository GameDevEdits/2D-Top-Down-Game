using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyFlash : MonoBehaviour
{
    public float flashDuration = 0.1f;
    public Color flashColor = Color.red;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
	
	public GameObject textHolder;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
		
        if (other.CompareTag("PlayerBullet"))
        {
			//Create the damage number popup
			Instantiate(textHolder, this.transform);
			//I'll need some information on the damage system to create a distinction between the regular and critial popups
			
            // Handle the enemy being hit by a player bullet.
            StartCoroutine(FlashEnemy());
        }
    }

    IEnumerator FlashEnemy()
    {
        // Flash the enemy with the specified color for the specified duration.
        spriteRenderer.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        // Reset the enemy's color to its original color.
        spriteRenderer.color = originalColor;
    }
}
