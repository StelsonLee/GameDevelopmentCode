/*
 * Name : Lee Kong Hue
 * ID   : 1303181
 * 
 * Description
 * this class is written to allow game object to request path finding
 * Once the path has been trace, the game object will move to the target by the traced path
 * Update new path once the target object changed their origin position
 */
using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;

    public Transform target;
    public float speed = 20;
    public float turnDst = 5;
    public float turnSpeed = 3;
    public float distance = 5;

    public Vector3 targetPosOld = Vector3.zero;

    Path path;

    void Start()
    {
        StartCoroutine(UpdatePath());
    }

    public void OnPathFound(Vector3[] wayPoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = new Path(wayPoints, transform.position, turnDst);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    //to update new path for the unit evertime the target change position
    IEnumerator UpdatePath()
    {
        if (Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }

        if (target != null)
        {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        }
        

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;

        if (target != null)
        {
            targetPosOld = target.position;
        }

        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            if (target != null)
            {
                if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
                {
                    PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
                    targetPosOld = target.position;
                }
            }
        }
    }

    //make the unit move toward target position by following the path founded by path finding
    IEnumerator FollowPath()
    {
        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);
        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }

            if (followingPath)
            {
                Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);

                if (Vector3.Distance(this.transform.position, target.position) > distance)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
                }
            }

            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            path.DrawWithGizmos();
         
        }
    }
}
