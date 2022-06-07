using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpanwer : MonoBehaviour
{

    public GameObject[] pickups;
    public float pickupDeliveryTime = 5f;
    public float dropRangeleft;
    public float dropRangeright;
    public float highHealthThreshold = 75f;
    public float lowHealthThreshold = 25f;

    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

    }
    void Start()
    {
        StartCoroutine(DeliverPickup());
    }

	public IEnumerator DeliverPickup()
	{
		// 等待投放延迟
		yield return new WaitForSeconds(pickupDeliveryTime);

		float dropPosX = Random.Range(dropRangeleft, dropRangeright);

		// Create a position with the random x coordinate.
		Vector3 dropPos = new Vector3(dropPosX, 15f, 1f);

		if (playerHealth.health >= highHealthThreshold)
			Instantiate(pickups[0], dropPos, Quaternion.identity);

		else if (playerHealth.health <= lowHealthThreshold)
			Instantiate(pickups[1], dropPos, Quaternion.identity);
		
		else
		{
			int pickupIndex = Random.Range(0, pickups.Length);
			Instantiate(pickups[pickupIndex], dropPos, Quaternion.identity);
		}
	}
	// Update is called once per frame
	void Update()
    {
        
    }
}
