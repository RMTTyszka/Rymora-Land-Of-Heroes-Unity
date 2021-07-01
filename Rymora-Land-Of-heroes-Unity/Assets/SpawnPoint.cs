using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public Monster myMonster;
    public Monster monterPref;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Monster SpawnMonster() {
        if (myMonster != null) {
            Debug.Log(myMonster.name);
        }
        if (myMonster != null && !myMonster.GetComponent<CombatChar>().isAlive) {
            myMonster = null;
        }

        if (myMonster == null && monterPref != null) {
            myMonster = Instantiate(monterPref, transform.position, Quaternion.identity);
            return myMonster;
        } else {
            return null;
        }
    }    public Monster SpawnMonster(Monster monster) {
        monterPref = monster;
        return SpawnMonster();
    }

    public void Reset() {
        monterPref = null;
    }
}
