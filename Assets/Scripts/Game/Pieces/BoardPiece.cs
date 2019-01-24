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