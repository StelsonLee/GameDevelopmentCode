/*
 * Description :
 * 
 * to tell the ai unit what to do according to the situation such as patrol, engage, and search
 * 
 */
using UnityEngine;
using System.Collections;

public class DecisionBH : MonoBehaviour
{
    public enum AISTATE { Patrol, Engage, Search, Predict}
    public PatrolTarget[] patrolTarget;
    //public Transform vision;
    public AISTATE aiState = AISTATE.Patrol;

    private PatrolTarget target;
    private Unit unit;
    private LookAround lookAround;
    private bool reachTarget = false;
    private bool reachLastSeen = false;
    private FieldOfView visionScript;
    private GameObject targetLastSeen;
    private FieldOfView hearingScript;
    private GameObject fireArmCollider;
    private bool isAttacking = false;
    private bool isReloading = false;

    public bool predictionTrigger = false;

    public void Start()
    {
        unit = transform.GetComponent<Unit>();
        lookAround = this.transform.Find("Vision").GetComponent<LookAround>();
        visionScript = this.transform.Find("Vision").GetComponent<FieldOfView>();
        hearingScript = this.transform.Find("Hearing").GetComponent<FieldOfView>();
        fireArmCollider = this.transform.Find("FireArm").Find("HitCollider").gameObject;
        fireArmCollider.SetActive(false);
        PatrolStateAction();
        Debug.Log("look around get : " + lookAround);
    }

    //update ai state according to the situation
    public void Update()
    {
        //change to engage state once target inside ai unit vision
        if ((visionScript.visibleTargets.Count > 0 && aiState != AISTATE.Engage) || (hearingScript.visibleTargets.Count > 0 && aiState != AISTATE.Engage))
        {
            aiState = AISTATE.Engage;
            if (visionScript.visibleTargets.Count > 0)
            {
                EngageStateAction(visionScript.visibleTargets[0]);
            }
            else if (hearingScript.visibleTargets.Count > 0)
            {
                EngageStateAction(hearingScript.visibleTargets[0]);
            }
           
        }
        else if (aiState == AISTATE.Engage && visionScript.visibleTargets.Count <= 0 && hearingScript.visibleTargets.Count <= 0 && predictionTrigger)
        {
            aiState = AISTATE.Predict;
            unit.target = null;
            PredictionStateAction();
        }
        // change state from engage to search once the ai unit lost their target
        else if (aiState == AISTATE.Engage && visionScript.visibleTargets.Count <= 0 && hearingScript.visibleTargets.Count <= 0 && !predictionTrigger)
        {
            aiState = AISTATE.Search;
            unit.target = null;
            SearchStateAction();
        }
        //patrol around the game environment 
        else if (aiState == AISTATE.Patrol)
        {
            if (Vector3.Distance(this.transform.position, target.targetTransform.position) < unit.distance && !reachTarget)
            {
                reachTarget = true;
                StartCoroutine("WaitWhileReachTarget");
            }
        }
        //search the target location where the ai unit last seen the target
        else if (aiState == AISTATE.Search)
        {
            if (Vector3.Distance(this.transform.position, unit.target.position) < unit.distance && !reachLastSeen)
            {
                Debug.Log("inside search state");
                reachLastSeen = true;
                StopCoroutine("ContinuePatrol");
                StartCoroutine("ContinuePatrol");
            }
        }
        //engage once the ai unit found their target and get near to it
        else if (aiState == AISTATE.Engage)
        {
            Debug.Log("in engage");
            if (Vector3.Distance(this.transform.position, unit.target.position) < unit.distance && !isAttacking && !isReloading)
            {
                Debug.Log("in attack range");
                StartCoroutine("Attack");
            }
        }
        else if (aiState == AISTATE.Predict)
        {
            Debug.Log("inside Predict state");
            if (Vector3.Distance(this.transform.position, unit.target.position) < unit.distance && !reachLastSeen)
            {
                Debug.Log("inside Predict state");
                reachLastSeen = true;
                StopCoroutine("ContinuePatrol");
                StartCoroutine("ContinuePatrol");
            }
        }

    }

    public void PredictionStateAction()
    {
        PredictionBH triggerPoints = PredictionBH.TriggerWayPoints.GetComponent<PredictionBH>();
        int selectedIndex = 0;
        int currentCount = triggerPoints.WayPoints[0].Percentages;

        for (int i = 1; i < triggerPoints.WayPoints.Count; i++)
        {
            if (triggerPoints.WayPoints[i].Percentages != 0)
            {
                if (currentCount < triggerPoints.WayPoints[i].Percentages)
                {
                    currentCount = triggerPoints.WayPoints[i].Percentages;
                    selectedIndex = i;
                }
            }
        }

        unit.target = triggerPoints.WayPoints[selectedIndex].PossiblePoints.transform;
        lookAround.StartLookAround();
    }

    //function call while ai change to patrol state to patrol the game environment
    public void PatrolStateAction()
    {
        target = GetRandomPatrolTarget();
        unit.target = target.targetTransform;
        reachTarget = false;
        lookAround.StopLookAround();
        lookAround.StartLookAround();
    }

    //funtion call once the ai saw their target
    public void EngageStateAction(Transform target)
    {
        StopCoroutine("ContinuePatrol");
        Debug.Log("engage" + target);
        lookAround.StopLookAround();
        lookAround.LookMiddle();
        unit.target = target;
    }

    //function call once the ai lost their target
    public void SearchStateAction()
    {
        Debug.Log("Search state");
        if (targetLastSeen != null)
        {
            Destroy(targetLastSeen);
        }
        
        targetLastSeen = new GameObject("TargetLastSeen");
        targetLastSeen.transform.position = unit.targetPosOld;
        unit.target = targetLastSeen.transform;
        lookAround.StartLookAround();
    }

    //ai attack cycle, attach, and reload
    IEnumerator Attack()
    {
        isAttacking = true;
        fireArmCollider.SetActive(true);

        yield return new WaitForSeconds(2);

        fireArmCollider.SetActive(false);
        isAttacking = false;
        isReloading = true;

        yield return new WaitForSeconds(3);
        isReloading = false;
        
    }

    private PatrolTarget GetRandomPatrolTarget()
    {
        float rangeHit = Random.Range(0, 100);
        float accumurate = 0;

        for (int i = 0; i < patrolTarget.Length; i++)
        {
            Debug.Log("range hit : " + rangeHit);
            Debug.Log("percentage : " + (accumurate + (patrolTarget[i].percentage / patrolTarget.Length)));

            if (rangeHit < accumurate + (patrolTarget[i].percentage / patrolTarget.Length))
            {
                //patrolTarget[i].percentage = 0;

                for (int j = 0; j < patrolTarget.Length; j++)
                {
                    if (j != i)
                    {
                        patrolTarget[j].percentage += patrolTarget[i].percentage / (patrolTarget.Length - 1);
                    }
                }

                patrolTarget[i].percentage = 0;

                return patrolTarget[i];
            }
            else
            {
                accumurate += patrolTarget[i].percentage / patrolTarget.Length;
            }
        }

        return null;
    }

    IEnumerator ContinuePatrol()
    {
        yield return new WaitForSeconds(2);

        aiState = AISTATE.Patrol;
        PatrolStateAction();
        reachLastSeen = false;
        reachTarget = false;
    }

    IEnumerator WaitWhileReachTarget()
    {
        yield return new WaitForSeconds(2);
        PatrolStateAction();
    }

    [System.Serializable]
	public class PatrolTarget
    {
        public Transform targetTransform;
        public float percentage;
    }
}
