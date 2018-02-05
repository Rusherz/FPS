using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {

    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

    public GameObject muzzleFlash;

    public GameObject[] weapons;
    GameObject currWeapon;
    Weapon currWeaponInfo;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                if ((i + 1) > weapons.Length)
                    return;

                if (currWeapon == null)
                {
                    SetWeapon(i);
                    return;
                }
                    
                if (currWeapon.name == weapons[i].name + "(Clone)")
                    return;

                Destroy(currWeapon);
                SetWeapon(i);

            }

        }
    }

    void SetWeapon(int i)
    {
        currWeapon = Instantiate(weapons[i], transform);
        currWeaponInfo = currWeapon.GetComponent<Weapon>();
        currWeaponInfo.SetMuzzleFlash(muzzleFlash);
    }

}
