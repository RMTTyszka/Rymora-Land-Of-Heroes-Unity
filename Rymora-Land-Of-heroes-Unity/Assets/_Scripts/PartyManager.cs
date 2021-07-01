using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour {

	public static PartyManager instance = null;

	public List<List<CharMove>> Parties; 

	public CharMove currentMember;
	public int currentParty = 0;
    private bool canChangeCamPos = true;

	

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != null) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		Parties = new List<List<CharMove>>();
		for (int x = 0; x < 3; x++) {
			Parties.Add(new List<CharMove>());
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			SelectPartyMember(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			SelectPartyMember(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			SelectPartyMember(2);
		}
		if (Input.GetKeyDown(KeyCode.Alpha5)) {
			SelectPartyMember();
		}
        if (Input.GetKeyDown(KeyCode.Escape)) {
            foreach (CharMove Char in Parties[currentParty]) {
                Char.GetComponent<Character>().inventory.gameObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Inventory invi = currentMember.GetComponent<Character>().inventory;
            invi.gameObject.SetActive(!invi.gameObject.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            AiMenu aimenu = currentMember.GetComponent<CharMove>().aiMenu;
            aimenu.gameObject.SetActive(!aimenu.gameObject.activeSelf);
        }
        Camera.main.transform.position = GetCamPos();
	}

	public void SelectPartyMember() {
		foreach(CharMove member in Parties[currentParty]) {
            if (member != currentMember) {
                member.isFollowing = currentMember;
            }
			//member.isSelected = true;
			//member.sprite.GetComponent<SpriteOutline>().enabled = true;
			if (member.CombChar.castTarget != null) {
				//member.CombChar.castTarget.sprite.GetComponent<SpriteOutline>().enabled = true;
			}
		}

	}
	public void SelectPartyMember(int index) {
		ResetSelection();
		if (Parties[currentParty].Count >= index+1) {
            if (currentMember == Parties[currentParty][index])
            {
                currentMember = null;
                return;
            }
            else {
			    currentMember = Parties[currentParty][index];
			    currentMember.isSelected = true;
			    currentMember.sprite.GetComponent<SpriteOutline>().enabled = currentMember.isSelected;
			    if (currentMember.CombChar.castTarget != null) {
				    currentMember.CombChar.castTarget.sprite.GetComponent<SpriteOutline>().enabled = true;
			    }
            }

		}
        if (currentMember != null) {
            currentMember.Menu.sortingOrder = 50;
            StartCoroutine(rollCam());
        }
	}

	public int GetPartyIndex(CharMove Char) {
		return Parties[currentParty].IndexOf(Char);
	}

    public IEnumerator rollCam() {
        canChangeCamPos = false;
        Vector3 oldPos = Camera.main.transform.position;
        float timer = 0f;
        while (timer < 1f) {
            timer += Time.deltaTime*6;
            Vector3 newPos = Vector3.Lerp(oldPos, currentMember.transform.position, timer);
            newPos.z = Camera.main.transform.position.z;
            Camera.main.transform.position = newPos;
            yield return 0;
        }
        canChangeCamPos = true;
    }
	public Vector3 GetCamPos() {
        if (canChangeCamPos)
        {
            if (currentMember == null)
            {
                return Camera.main.transform.position;
            }
            Vector3 cameraPos = Vector3.zero;
            foreach (CharMove Char in Parties[currentParty])
            {
                cameraPos += Char.transform.position;
            }
            cameraPos /= Parties[currentParty].Count;
            cameraPos.z = Camera.main.transform.position.z;
            cameraPos = currentMember.transform.position;
            cameraPos.z = Camera.main.transform.position.z;
            return cameraPos;
            //return cameraPos;
        }
        else {
            return Camera.main.transform.position;
        }
	}

	public void ResetSelection() {
		foreach (CharMove Char in Parties[currentParty]) {
            Char.Menu.sortingOrder = 5;
			Char.isSelected = false;
            Char.isFollowing = null;
            Char.wayPoints.Clear();
			Char.sprite.GetComponent<SpriteOutline>().enabled = false;
			if (Char.CombChar.castTarget != null) {
				Char.CombChar.castTarget.spriteOutline.enabled = false;
			}
			if (!Char.isSelected) {
				Char.resetRanges();
			}
		}
       // currentMember = null;
	}
}
