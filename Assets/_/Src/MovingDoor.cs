using System.Collections;
using UnityEngine;

public class MovingDoor : MonoBehaviour
{
    [SerializeField] private Transform startLoc;
    [SerializeField] private Transform endLoc;
    [SerializeField] private float idleTime = 3f;
    
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(idleTime);
            yield return MoveDoor(open: true);
            yield return new WaitForSeconds(idleTime);
            yield return MoveDoor(open: false);
        }
    }

    IEnumerator MoveDoor(bool open)
    {
        float t = 0f;
        
        Vector3 a = open ? startLoc.localPosition : endLoc.localPosition;
        Vector3 b = open ? endLoc.localPosition : startLoc.localPosition;
        
        while (t < 1f)
        {
            t+= Time.deltaTime;
            transform.localPosition = Vector3.Lerp(a, b, t);
            yield return null;
        }
    }
    
}