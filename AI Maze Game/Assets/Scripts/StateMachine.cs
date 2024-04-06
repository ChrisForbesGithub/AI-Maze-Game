using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class StateMachine : MonoBehaviour
{
    public Transform player;
    public enum States
    {
        Run,
        Patrol,
        Idle,
    }

    public States state = States.Run;

    void Start()
    {
        NextState();
    }

    void NextState()
    {
        switch (state)
        {
            case States.Run:
                StartCoroutine(RunState());
                break;
            case States.Patrol:
                StartCoroutine(PatrolState());
                break;
            case States.Idle:
                StartCoroutine(IdleState());
                break;
            default:
                Debug.LogError("State not included in NextState");
                break;
        }
    }

    IEnumerator PatrolState()
    {
        Debug.Log("Entering Patrol State...");
        while (state == States.Patrol)
        {
            transform.rotation *= Quaternion.Euler(0f, 50f * Time.deltaTime, 0f);
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.Normalize();
            //-1 to 1
            float result = Vector3.Dot(transform.right, directionToPlayer);
            if (result > 0.95)
            {
                state = States.Run;
            }
            yield return null;
        }
        Debug.Log("Exiting Patrol State...");
        NextState();
    }

    IEnumerator RunState()
    {
        float startTime = Time.time;
        while (state == States.Run)
        {
            float wave = Mathf.Sin(Time.time * 30f) * 0.1f + 1f;
            float wave2 = Mathf.Cos(Time.time * 30f) * 0.1f + 1f;
            transform.localScale = new Vector3(wave, wave2, wave);
            float shimmy = Mathf.Sin(Time.time * 30f) * 0.9f + 0.3f;
            transform.position += transform.right * shimmy * Time.deltaTime;

            if (Time.time - startTime > 3f)
            {
                state = States.Idle;
            }
            yield return null;
        }

        NextState();
    }

    IEnumerator IdleState()
    {
        float startTime = Time.time;
        while (state == States.Idle)
        {
            yield return new WaitForSeconds(0.1f);
            if (Time.time - startTime > 3f)
            {
                state = States.Patrol;
            }
        }
        NextState();
    }
}
