using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PredictionBH : MonoBehaviour
{
    public List<PossibleWayPoints> WayPoints;
    public static GameObject TriggerWayPoints = null;

    private DecisionBH decision;
    private int TriggerCount = 0;
    private bool isTriggerPredict = false;

    void Start()
    {
        decision = GameObject.Find("Seeker").transform.GetComponent<DecisionBH>();
    }

	void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (TriggerWayPoints == null)
            {
                Debug.Log("inside way piints");
                isTriggerPredict = true;
                decision.predictionTrigger = true;
                TriggerCount++;
                TriggerWayPoints = this.gameObject;
            }
            else if (TriggerWayPoints.transform.GetComponent<PredictionBH>().isTriggerPredict)
            {
                PredictionBH prediction = TriggerWayPoints.transform.GetComponent<PredictionBH>();

                for (int i = 0; i < prediction.WayPoints.Count; i++)
                {
                    if (this.gameObject != TriggerWayPoints && this.gameObject == prediction.WayPoints[i].PossiblePoints)
                    {
                        prediction.WayPoints[i].Percentages++;
                    }
                }

                isTriggerPredict = false;
                TriggerWayPoints = null;
                decision.predictionTrigger = false;
            }
            
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Exit way piints");
            //decision.predictionTrigger = false;
        }
    }

    [System.Serializable]
    public class PossibleWayPoints
    {
        public GameObject PossiblePoints;
        public int Percentages;
    }
}
