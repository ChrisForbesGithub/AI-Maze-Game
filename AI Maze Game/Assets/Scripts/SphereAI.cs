using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SphereAI : MonoBehaviour
{

    public List<Transform> waypoints;
    Transform currentTarget;
    private int _index = 1;

    private NavMeshAgent _agent;
    private Animator _animator;

    private bool _inReverse = false;
    private bool _atEnd = false;
    private bool _moving = true;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        if (_agent == null)
            Debug.LogError("NavMeshAgent missing");

        if (_animator == null)
            Debug.LogError("Animator missing");

        //If there are waypoint and the first waypoint is not null
        if (waypoints.Count > 0 && waypoints[0] != null)
        {
            //Set first target
            currentTarget = waypoints[_index];

            //Start moving the agent towards the first target
            _agent.SetDestination(currentTarget.position);
        }
    }

    IEnumerator MoveToNextWaypoint()
    {
        if (!_inReverse)
        {
            _index++;
        }

        //if not at the last waypoint and not going in reverse
        if (_index < waypoints.Count && !_inReverse)
        {
            //Pause a random amount of time before going to the first point
            if (_index == 1)
                yield return new WaitForSeconds(Random.Range(3f, 6f));

            currentTarget = waypoints[_index];
        }
        else
        {

            //If at the last point pause for a random amount of time
            if (!_atEnd)
            {
                _atEnd = true;
                yield return new WaitForSeconds(Random.Range(3f, 6f));
            }

            _index--;
            _inReverse = true;

            //if next waypoint is the first waypoint reset _inReverse and _atEnd
            if (_index == 0)
            {
                _inReverse = false;
                _atEnd = false;
            }

            currentTarget = waypoints[_index];
        }

        //Move agent to next waypoint
        _agent.SetDestination(currentTarget.position);
        _moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Get current speed percent of agent and set the speed parameter of the animator
        float speedPercent = _agent.velocity.magnitude / _agent.speed;
        _animator.SetFloat("speed", speedPercent);

        if (currentTarget != null)
        {
            //Check if the agent has arrived at the target position
            if((Vector3.Distance(transform.position, currentTarget.position) <= 2f) && _moving)
            {
                //Set moving to false to prevent this if statement from constantly running while at target position
                _moving = false;
                StartCoroutine("MoveToNextWaypoint");
            }
        }
    }
}
