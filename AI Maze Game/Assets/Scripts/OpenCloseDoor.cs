using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour
{
 
    private enum DoorState
    {
        Opening,
        Open,
        Closing,
        Close
    }

    private DoorState state;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    public float speed = 5f;
    public Vector3 deltaPosition = new Vector3(0f, -2f, 0f);

    void Start()
    {
        state = DoorState.Close;

        closedPosition = transform.position;
        openPosition = transform.position + deltaPosition;
    }

    void Update()
    {

        switch(state)
        {
            case DoorState.Opening:
                break;
            case DoorState.Open:
                break;
            case DoorState.Closing:
                break;
            case DoorState.Close:
                break;
            
        }

        if (state == DoorState.Close)
        {
            OpenTheDoor(openPosition);

            if (Vector3.Distance(transform.position, openPosition) < 0.01f)
            {
                state = DoorState.Open;
            }
        }

        if (state == DoorState.Open)
        {
            OpenTheDoor(closedPosition);

            if (Vector3.Distance(transform.position, closedPosition) < 0.01f)
            {
                state = DoorState.Close;
            }
        }
    }

    public void OpenTheDoor(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
