using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    GameObject muzzleFlash;

    new Camera camera;

    public int currentAmmo;
    public int bulletPerMag;
    public int bulletLeft;
    public int damage;
    public float range;
    public float rof;
    public float shotCooldown;
	public float FireType;
    public Transform shootPoint;
    public Transform muzzlePoint;
    public Animator anim;

	public GameObject bullet;
    GameObject go_bullet;
    Bullet bulletComp;

    // Use this for initialization
    void Start()
    {
        camera = gameObject.GetComponentInParent<Camera>();
        bulletLeft = bulletPerMag;
    }

    // Update is called once per frame
    void Update()
    {
		
        Fire();
        Reload();
        /*if (bulletComp != null)
            Debug.Log("Weapon::bullet impact = " + bulletComp.impact);
        if(bulletComp != null && bulletComp.impact)
        {
            Destroy(go_bullet);
        }*/
    }

    void Fire()
    {
		if (FireType == 0) {
			if (Input.GetButton ("Fire1") && shotCooldown <= 0 && bulletLeft > 0) {
				shotCooldown = rof;
				bulletLeft--;
            
				RaycastHit hit;

				if (Physics.Raycast (camera.transform.position, camera.transform.forward, out hit)) {
					shootPoint.LookAt (hit.point);
					muzzlePoint.LookAt (hit.point);
					GameObject muzzleFlashIns = Instantiate (muzzleFlash, muzzlePoint);
					Destroy (muzzleFlashIns, 0.1f);
					anim.CrossFadeInFixedTime ("Fire", rof);
					go_bullet = Instantiate (bullet, shootPoint.position, shootPoint.rotation);
					//Destroy(go_bullet, 15);
					bulletComp = go_bullet.GetComponent<Bullet> ();
					bulletComp.damage = damage;
					bulletComp.PlayerSelf = transform.root;
				}
			} else {
				anim.SetBool ("Fire", false);
				shotCooldown -= Time.deltaTime;
			}
		} else if (FireType == 1) {
			if (Input.GetButtonDown ("Fire1") && shotCooldown <= 0 && bulletLeft > 0) {
				shotCooldown = rof;
				bulletLeft--;

				RaycastHit hit;

				if (Physics.Raycast (camera.transform.position, camera.transform.forward, out hit)) {
					shootPoint.LookAt (hit.point);
					muzzlePoint.LookAt (hit.point);
					GameObject muzzleFlashIns = Instantiate (muzzleFlash, muzzlePoint);
					Destroy (muzzleFlashIns, 0.1f);
					anim.CrossFadeInFixedTime ("Fire", rof);
					go_bullet = Instantiate (bullet, shootPoint.position, shootPoint.rotation);
					//Destroy(go_bullet, 15);
					bulletComp = go_bullet.GetComponent<Bullet> ();
					bulletComp.damage = damage;
					bulletComp.PlayerSelf = transform.root;
				}
			} else {
				anim.SetBool ("Fire", false);
				shotCooldown -= Time.deltaTime;
			}
		}
    }

    void Reload()
    {
        if (currentAmmo == 0)
            return;
        if (Input.GetKeyDown(KeyCode.R) && bulletLeft != bulletPerMag && currentAmmo != 0)
        {
            anim.SetTrigger("Reload");
            StartCoroutine(WaitThenDoThings());
        }
    }

    IEnumerator WaitThenDoThings()
    {
        yield return new WaitForSeconds(3f);
       

            if (bulletLeft == 0)
            {
                bulletLeft = bulletPerMag;
                currentAmmo -= bulletPerMag;
            }
            else if ((bulletPerMag - bulletLeft) > currentAmmo)
            {
                bulletLeft += currentAmmo;
                currentAmmo = 0;
            }
            else
            {
                currentAmmo -= (bulletPerMag - bulletLeft);
                bulletLeft = bulletPerMag;
            }
    }

    public void SetMuzzleFlash(GameObject value)
    {
        muzzleFlash = value;
    }

}