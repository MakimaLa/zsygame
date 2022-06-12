using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour
{
	public float healthBonus;               
	public AudioClip collect;               


	private PickupSpanwer pickupSpawner;    
	private Animator anim;                  
	private bool landed;                    


	void Awake()
	{
		// Setting up the references.
		pickupSpawner = GameObject.Find("pickupmannger").GetComponent<PickupSpanwer>();
		anim = transform.root.GetComponent<Animator>();
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		// If the player enters the trigger zone...
		if (other.tag == "Player")
		{
			// Get a reference to the player health script.
			PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

			// Increasse the player's health by the health bonus but clamp it at 100.
			playerHealth.health += healthBonus;
			playerHealth.health = Mathf.Clamp(playerHealth.health, 0f, 100f);

			// Update the health bar.
			playerHealth.UpdateHealthBar();

			// Trigger a new delivery.
			//pickupSpawner.StartCoroutine(pickupSpawner.Deliverpickup());

			// Play the collection sound.
			AudioSource.PlayClipAtPoint(collect, transform.position);

			// Destroy the crate.
			Destroy(transform.root.gameObject);
		}
		// Otherwise if the crate hits the ground...
		else if (other.tag == "ground" && !landed)
		{
			// ... set the Land animator trigger parameter.
			anim.SetTrigger("Land");

			transform.parent = null;
			gameObject.AddComponent<Rigidbody2D>();
			landed = true;
		}
	}
}
