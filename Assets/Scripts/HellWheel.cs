using UnityEngine;
using System.Collections;

public class HellWheel : MonoBehaviour {

	public GameObject fire0;
	public GameObject fire1;
	public GameObject fire2;
	public GameObject fire3;
	public GameObject fire4;
	public GameObject fire5;
	
	public GameObject barrel0;
	public GameObject barrel1;
	public GameObject barrel2;
	public GameObject barrel3;
	public GameObject barrel4;
	public GameObject barrel5;

	public GameObject theThing;
	
	private GameObject[] fire;
	private GameObject[] barrel;
	private Vector3[] allPos = new Vector3[12];
	private int itemNumber = 20;
	
	void Start ()
	{
		fire = new GameObject[6]  {fire0, fire1, fire2, fire3, fire4, fire5 };
		barrel = new GameObject[6]  {barrel0, barrel1, barrel2, barrel3, barrel4, barrel5 };
		getImagePos ();
	}
	

	void Update ()
	{
		if (Input.GetMouseButtonDown (0))
			itemNumber = calculatePosition();

		if (Input.GetMouseButton (0) && itemNumber < 6)	//move fire if dragged one
				fire[itemNumber].transform.position = Vector3.MoveTowards (fire[itemNumber].transform.position, Input.mousePosition, 100f);

		if (Input.GetMouseButtonUp (0) && itemNumber == calculatePosition() - 6)	//move fire to barrel if fit
				allPos[itemNumber] = allPos[calculatePosition ()];
		
		if (!Input.GetMouseButton (0))	//return fire to default position
			for (int i = 0; i < 6; i++)
				fire[i].transform.position = Vector3.MoveTowards ( fire[i].transform.position, allPos[i], 10f);
		moveTheThing ();
	}

	int calculatePosition ()
	{
		for (int i = 0; i < 12; i++)
			if (Mathf.Abs (Input.mousePosition.x - allPos [i].x) < 50 &&
			    Mathf.Abs (Input.mousePosition.y - allPos [i].y) < 50)
				return i;
		return 20;
	}

	void getImagePos()
	{
		for (int i = 0; i < 6; i++) {
			allPos[i] = fire[i].transform.position;
			allPos[i+6] = barrel[i].transform.position;
		}
	}

	void moveTheThing()
	{
		for (int i = 0; i < 6; i++)
			if (allPos [i] != allPos [i + 6])
				return;
		theThing.transform.position = Vector3.MoveTowards (theThing.transform.position, fire [0].transform.position, 20f);
	}
}
