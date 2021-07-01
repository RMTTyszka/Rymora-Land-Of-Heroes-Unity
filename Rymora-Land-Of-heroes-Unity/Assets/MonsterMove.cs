using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour {

	public float aggroRange;
	public float aggroMaxTime = 5f;
	public LayerMask mask;
	public CombatChar target;
	public List<CombatChar> targets;
	public CombatChar combChar;
	public Character C;
	public CircleCollider2D AggroRange;
	private float minimumRange;
    [SerializeField]
	private float aggroTimer;
    public Pathfinder pathfinder;
    public Queue<Vector3> waypoints;
    public Vector3 waypoint;
    private Vector3 initialPos;
    private float waypointTimer = 0f;
    private bool hasArrived = true;


    // Use this for initialization
    void Start () {
        pathfinder = GameObject.FindGameObjectWithTag("Pathfinder").GetComponent<Pathfinder>();
        targets =new List<CombatChar>();
		combChar = transform.root.GetComponent<CombatChar>();
		C = transform.root.GetComponent<Character>();
		AggroRange = GetComponent<CircleCollider2D>();
		AggroRange.radius = aggroRange;
		minimumRange = combChar.weapons[0].range*0.75f;
		aggroTimer = 0f;
        initialPos = transform.position;
        waypoints = new Queue<Vector3>();
		//minumumRange = combChar.weapons[0].range;
	}

	// Update is called once per frame
	void Update () {
        //se chegou no waypoint, pega o proximo
        if (!combChar.isAlive || !combChar.isActive) {
            return;
        }

        if (Vector3.Distance(transform.position, waypoint) < 0.1f) {
            if (waypoints.Count > 0)
            {
                waypoint = waypoints.Dequeue();
            }
            else {
                hasArrived = true;
            }
        }
        // se ficou sem visao por 5 segundos
        if (aggroTimer >= aggroMaxTime) {
            target = null;
            StartCoroutine(pathfinder.GetWaypoints(transform.position, initialPos, this));
            aggroTimer = 0f;
        }

        // Pega novo caminho para o alvo, e se nao ta perto, se move na diferencao dele
        if (target != null) {
            InLineOfSight(target.transform);
            waypointTimer += Time.deltaTime;
            if (waypointTimer >= 0.2f) {
                StartCoroutine(pathfinder.GetWaypoints(transform.position, target.transform.position, this));
                waypointTimer = 0f;
            }
            if (Vector3.Distance(transform.position, target.transform.position) >= minimumRange || (target != null && !InLineOfSight(target.transform)))
            {
                hasArrived = false;
                MoveToTarget(waypoint);
            }
            else {
                hasArrived = true;
            }
        }

        // Sem alvo e voltando pro inicio
        if (target == null && C.transform.position != initialPos) {
            MoveToTarget(waypoint);
        }




        

	}

    public void MoveToTarget(Vector3 wp) {
        Floor floor = pathfinder.ground.GetTile(Vector3Int.FloorToInt(transform.position)) as Floor;

        C.transform.position = Vector3.MoveTowards(transform.position, wp, C.speed * Time.deltaTime * floor.moveMultiplier);
    }

	void OnTriggerStay2D(Collider2D coll) {
        if (target != null && coll.transform == target.transform) {
            return;
        }

		if (coll.CompareTag("Player")) {
			if (InLineOfSight(coll.transform)){
				CombatChar collCombat = coll.GetComponent<CombatChar>();
				if (target == null) {
					target = collCombat;

                }
				if (collCombat.GetAggro() >= target.GetAggro()){
					target = collCombat;
                }
                StartCoroutine(pathfinder.GetWaypoints(transform.position, target.transform.position, this));
                //Debug.DrawLine(transform.position, coll.transform.position);
            } else {
				//Debug.DrawLine(transform.position, coll.transform.position, Color.red);
			}

        }
    }
	private bool InLineOfSight(Transform target) {
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, target.position-transform.position, aggroRange, mask);
		foreach (RaycastHit2D hit in hits) {
			//Debug.DrawLine(transform.position, hit.transform.position, Color.cyan);
			if (hit.transform.CompareTag("Player")) {
				aggroTimer = 0f;
				return true;
			}
			if (hit.transform.CompareTag("Enemy")) {
				continue;
			} else if (!hit.transform.CompareTag("Player")){
				aggroTimer += Time.deltaTime;
				return false;
			}
		}	
		aggroTimer = 0f;
		return true;
		
	}
}
