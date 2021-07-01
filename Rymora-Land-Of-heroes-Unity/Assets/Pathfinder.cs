using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinder : MonoBehaviour {

    public Tilemap ground;
    public Tilemap walls;
    public Tilemap enviroment;
    public Transform pref;
    public List<Vector2Int> newWp;
    public Queue<Vector2Int> frontier;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator GetWaypoints(Vector2 origin, Vector2 target, MonsterMove monster) {
        newWp = new List<Vector2Int>();
        frontier = new Queue<Vector2Int>();
        Vector2Int endPos = Vector2Int.FloorToInt(target);
        frontier.Enqueue(Vector2Int.FloorToInt(origin));

        Dictionary<Vector2Int, Vector2Int> wp = new Dictionary<Vector2Int, Vector2Int>();
        wp.Add(Vector2Int.FloorToInt(origin), Vector2Int.FloorToInt(origin));
        while (frontier.Count >= 1) {
            Vector2Int current = frontier.Dequeue();
            foreach (Vector2Int next in getNeighbors(Vector2Int.FloorToInt(current))) {
                //yield return 0;
                if (!wp.ContainsKey(next)) {
                    frontier.Enqueue(next);
                   //Instantiate(pref, new Vector3(next.x+0.5f, next.y+0.5f, 0f), Quaternion.identity);
                    if (!wp.ContainsKey(next)) {
                        wp.Add(next, current);
                    }
                    if (next == Vector2Int.FloorToInt(target)) {
                       // Debug.Log("Achooo");
                        frontier.Clear();
                        break;
                    }
                }
            }
        }

        Vector2Int newCurrent = Vector2Int.FloorToInt(target);
        

        while (newCurrent != Vector2Int.FloorToInt(origin)) {
            newWp.Add(newCurrent);
            if (!wp.ContainsKey(newCurrent)) {
                break;
            }
            newCurrent = wp[newCurrent];
        }
        newWp.Reverse();
        monster.waypoints.Clear();
        foreach (Vector2Int pos in newWp) {
            monster.waypoints.Enqueue(new Vector3(pos.x+0.5f, pos.y + 0.5f, 0));
        }
        if (monster.waypoints.Count > 0)
        {
            monster.waypoint = monster.waypoints.Dequeue();
        }
        else {
            monster.waypoint = monster.C.transform.position;
        }


        for (int i = 0; i < newWp.Count-1; i++)
        {
            Vector3 ori = new Vector3(newWp[i].x+0.5f, newWp[i].y + 0.5f, 0);
            Vector3 dest = new Vector3(newWp[i+1].x + 0.5f, newWp[i+1].y + 0.5f, 0);
            Debug.DrawLine(ori, dest, Color.red, 3f);
        }

        yield return 0;

        








    }

    public List<Vector2Int> getNeighbors(Vector2Int origin) {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        List<Vector2Int> positions = new List<Vector2Int>();


        positions.Add(new Vector2Int(origin.x - 1, origin.y));
        positions.Add(new Vector2Int(origin.x + 1, origin.y));
        positions.Add(new Vector2Int(origin.x, origin.y -1));
        positions.Add(new Vector2Int(origin.x, origin.y + 1));
        positions.Add(new Vector2Int(origin.x-1, origin.y + 1));
        positions.Add(new Vector2Int(origin.x+1, origin.y + 1));
        positions.Add(new Vector2Int(origin.x+1, origin.y - 1));
        positions.Add(new Vector2Int(origin.x-1, origin.y - 1));
        foreach (Vector2Int tilePos in positions) {
            Vector3Int position = new Vector3Int(tilePos.x, tilePos.y, 0);
            Floor tile = ground.GetTile(position) as Floor;
            if (tile != null && tile.isWalkable) {
                Wall tile2 = walls.GetTile(position) as Wall;
                if (tile2 == null) {
                      neighbors.Add(tilePos);
                    }
            }
        }

        return neighbors;

    }
}
