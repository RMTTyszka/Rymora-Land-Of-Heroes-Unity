using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField]
    public float spawnCD = 10f;
    public List<Encounter> Encounters;
    public List<SpawnPoint> Points;
    private Queue<int> pointsQueue;
    private HashSet<int> pointsHash;
    private Encounter currentEncounter;
    public List<Monster> currentMonsters;
    public float spawnTimer = 0f;
    public float timer = 0f;
    public bool hasDeadMonster = false;

    private List<Monster> newMonsterList;


    // Use this for initialization
    void Start () {
        pointsQueue = new Queue<int>();
        pointsHash = new HashSet<int>();
        currentMonsters = new List<Monster>();
        newMonsterList = new List<Monster>();

        foreach (Transform children in transform) {
            SpawnPoint pt = children.GetComponent<SpawnPoint>();
            if (!Points.Contains(pt)) {
                Points.Add(pt);
            }
        }
       SpawnRandom(true);



		
	}

    // Update is called once per frame
    void Update () {
        if (hasDeadMonster) {
            spawnTimer += Time.deltaTime*3;
        }
        if (spawnTimer >= spawnCD) {
            spawnTimer = 0f;
            if (allDead())
            {
                foreach (SpawnPoint point in Points) {
                    point.Reset();
                }
                SpawnRandom(true);
            }
            else {
                SpawnRandom(false);
            }
            hasDeadMonster = false;
        }
        timer += Time.deltaTime*3;
    
        if (timer >= 10f) {
            hasDeadMonster = DeadMonster();
            timer = 0f;
        }

	}

    public void SpawnRandom(bool newEncounter) {
        currentMonsters.RemoveAll(Monster => !Monster.GetComponent<CombatChar>().isAlive);

        if (newEncounter)
        {

            int encounterIndex = Random.Range(0, Encounters.Count);
            currentEncounter = Encounters[encounterIndex];
            pointsHash.Clear();
            pointsQueue.Clear();
            while (pointsQueue.Count < currentEncounter.enconter.Count)
            {
                int x = Random.Range(0, Points.Count);
                if (!pointsHash.Contains(x))
                {
                    pointsHash.Add(x);
                    pointsQueue.Enqueue(x);
                }
            }
            foreach (Monster monster in currentEncounter.enconter)
            {
                Monster newMons = Points[pointsQueue.Dequeue()].SpawnMonster(monster);
                if (newMons != null) {
                    currentMonsters.Add(newMons);
                }

            }
        }
        else {
            foreach (SpawnPoint point in Points) {
                Monster newMons = point.SpawnMonster();

                if (newMons != null)
                {
                    currentMonsters.Add(newMons);
                }
            }
        }



    }

    public bool allDead() {
        return !AliveMonster();
    }
    public bool AliveMonster() {
        bool isAlive = false;
        foreach (Monster monster in currentMonsters) {
            if (monster.GetComponent<CombatChar>().isAlive)
            {
                isAlive =  true;
            }
            else {
            }
        }
        return isAlive;
    }
    public bool allAlive() {
        return !DeadMonster();
    }

    public bool DeadMonster() {
        currentMonsters.RemoveAll(Monster => Monster == null);
        foreach (Monster monster in currentMonsters) {
            if (!monster.GetComponent<CombatChar>().isAlive) {
                return true;
            }
        }
        return false;
    }

}
[System.Serializable]
public class Encounter
{
    public List<Monster> enconter;

}




