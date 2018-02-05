using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 1;
    float lifeTime = 5f;
    Vector3 gravity = Physics.gravity;
    Vector3 moveDirection;
    float closest;
    bool foundHit = false;
    Vector3 spawnPoint;
    Quaternion spawnRotation;
    public GameObject hitEffectPrefab;
    public Transform PlayerSelf;
    public int damage;

    void Awake()
    {
        moveDirection = transform.forward * speed;
        closest = Mathf.Infinity;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(moveDirection);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, speed * Time.deltaTime);
        foreach (RaycastHit hit in hits)
        {
            if(hit.transform != PlayerSelf)
            {
                if(Vector3.Distance(transform.position, hit.point) < closest)
                {
                    closest = Vector3.Distance(transform.position, hit.point);
                    foundHit = true;
                    spawnPoint = hit.point;
                    spawnRotation = Quaternion.LookRotation(hit.normal);
                }
            }
			if (hit.transform.GetComponent<PlayerHealth> () == true) {
				hit.transform.GetComponent<PlayerHealth> ().TakeDamage (damage);
			}
        }

        if (foundHit)
        {
            GameObject go_hitEffect = Instantiate(hitEffectPrefab, spawnPoint, spawnRotation);
            Destroy(go_hitEffect, 1f);
            Destroy(gameObject);
        }

        transform.position += moveDirection * Time.deltaTime;
        moveDirection.y -= gravity.y * Time.deltaTime;

        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)
        {
            Destroy(gameObject);
        }

    }

    /*Vector3 prevPos;
    public bool impact = false;
    public int damage;

	public GameObject hitEffectPrefab;

	void Start(){
		Debug.Log ("Bullet::Started");
		prevPos = transform.position;
	}

	void Update(){
		RaycastHit hit;
		Ray ray = new Ray (prevPos, transform.forward);
		if (Physics.SphereCast(ray, 5, out hit, 5)) {
            if (hit.transform.name != "Player" && hit.transform != transform)
            {
                Debug.Log("Bullet::We hit: " + hit.transform.name);
                GameObject hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 1f);
                impact = true;
                PlayerHealth h = hit.transform.GetComponent<PlayerHealth>();
                if (h != null)
                {
                    h.TakeDamage(damage);
                }
            }
        }
		prevPos = transform.position;
	}*/

}
