// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class BoardPiece : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    public float circleRadius = .1f;
    public float movementSpeed = 1f; // units/s
    public AnimationCurve movementCurve;


    // private
    Vector3 endPos;


    // references
	
	
	// --------------------- BASE METHODS ------------------
	void Start () {
        
	}
	
	void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void MoveTo(string cityNid) {
        MoveTo(City.Get(cityNid).Position);
    }
    public void MoveTo(City city) {
        MoveTo(city.Position);
    }
    public void MoveTo(Vector3 p) {
        endPos = p + Utility.OnUnitCircle().To3() * circleRadius;
        StopCoroutine("MoveToRoutine");
        StartCoroutine("MoveToRoutine");
    }



    // queries



    // other
    IEnumerator MoveToRoutine() {
        float percent = 0;
        float dist = Vector3.Distance(transform.position, endPos);
        Vector3 startPos = transform.position;
        while (percent < 1) {
            percent += Time.deltaTime * movementSpeed / dist;
            transform.position = Vector3.Lerp(startPos, endPos, movementCurve.Evaluate(percent));
            yield return null;
        }
    }

}