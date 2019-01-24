// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// DESCRIPTION //////////

public class BoardPiece : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    float circleRadius = .2f;
    float movementSpeed = 2f; // units/s
    AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    readonly Vector3 restPosition = new Vector3(-5, 0, 0);


    // private
    Vector3 endPos;
    public City CurrentCity { get; private set; }


    // references
	
	
	// --------------------- BASE METHODS ------------------
	void Start () {
        
	}
	
	void Update () {
        
	}

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void MoveTo(string cityNid) {
        MoveTo(City.Get(cityNid));
    }
    public void MoveTo(City city) {
        CurrentCity = city;
        MoveToEffective(city.Position);
    }
    public void MoveTo(Vector3 pos) {
        CurrentCity = null;
        MoveToEffective(pos);
    }
    public void MoveAway() {
        MoveTo(restPosition);
    }


    void MoveToEffective(Vector3 p) {
        endPos = p + Utility.OnUnitCircle().To3() * circleRadius;
        StopCoroutine("MoveToRoutine");
        StartCoroutine("MoveToRoutine");
    }

    public void SetColor(char color) {
        GetComponentInChildren<MeshRenderer>().material = ColorManager.instance.Char2Material(color);
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