using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    float health = 100;
	public GameObject cube;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    void Die()
    {
		Instantiate(cube, new Vector3(Random.Range(-32, 32), 0.5f, Random.Range(-32, 32)), Quaternion.identity);
        Destroy(gameObject);
    }

}
