using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GameObject image0;
	public GameObject image1;
	public GameObject image2;
	public GameObject image3;
	public GameObject image4;
	public GameObject image5;

	public GameObject shadow1;
	public GameObject shadow2;
	public GameObject shadow3;
	public GameObject shadow4;
	public GameObject shadow5;

	private GameObject[] image;
	private GameObject[] shadow;
	private int[] matrOrder = new int[]  { 0, 1, 2, 3, 4, 5 };
	private int[] shadowOrder = new int[] { 0, 5, 4, 3, 2, 1 };
	private Vector3[] defPos = new Vector3[6];
	private Vector3[] shadPos = new Vector3[6];

	private int firstObj;
	private int secondObj;
	private bool itemDragged;



	void Start () 
	{
		//image0 is invisible image just to fill slot and start main array from 1, not 0
		image = new GameObject[6]  {image0, image1, image2, image3, image4, image5 };
		shadow = new GameObject[6]  {image0, shadow5, shadow4, shadow3, shadow2, shadow1 };
		getImagePos ();
		swapShadowOrder ();
	}
	


	void Update ()
	{
		if (Input.GetMouseButton (0))
		{
			if (itemDragged && firstObj > 0)
				image[firstObj].transform.position =Vector3.MoveTowards (image[firstObj].transform.position, Input.mousePosition, 100f);
		}

		if (Input.GetMouseButtonDown (0))
		{
			firstObj = calculatePosition();
			itemDragged = true;
		}

		if (Input.GetMouseButtonUp (0))
		{
			secondObj = calculatePosition();
			if (secondObj == 0)  //if dragged to nowhere just go back
				secondObj = firstObj;
			swapVector3 (ref defPos [firstObj], ref defPos [secondObj]);
			swapInt(ref matrOrder[firstObj], ref matrOrder[secondObj]);
			itemDragged = false;
		}

		if (!Input.GetMouseButton (0))
		{
			for (int i = 1; i < 6; i++)
				image[i].transform.position = Vector3.MoveTowards ( image[i].transform.position, defPos[i], 10f);
		}

		matchCheck (); //finish if shadowOrder and matrOrder are equal

	}

	void matchCheck ()
	{
		for (int i = 1; i < 6; i++)
			if (matrOrder [i] != shadowOrder [i])
				return;
		//executed if matrOrder == shadowOrder, except for first members
		image[0].transform.position = Vector3.MoveTowards ( image[0].transform.position, defPos[1], 10f);
	}
	
	//compare MousePos with DefPos and return number of DefPos if any nearby
	int calculatePosition ()
	{
		for (int i = 1; i < 6; i++)
			if (Mathf.Abs (Input.mousePosition.x - defPos [i].x) < 50 &&
				Mathf.Abs (Input.mousePosition.y - defPos [i].y) < 50)
				return i;
		return 0;
	}

	void getImagePos()
	{
		for (int i = 0; i < 6; i++) {
			defPos[i] = image[i].transform.position;
			shadPos[i] = shadow[i].transform.position;
		}
	}
	
	void swapShadowOrder ()
	{
		for (int i = 0; i < 10; i++)
		{
			//wtf is that random func?!
			int itemToSwap = Random.Range (1 , 6);
			int itemToSwapWith = Random.Range (1 , 6);
			swapVector3 (ref shadPos[itemToSwap], ref shadPos[itemToSwapWith]);
			swapInt(ref shadowOrder[itemToSwap], ref shadowOrder[itemToSwapWith]);
		}
		for (int i = 1; i < 6; i++) //swap shadows to new positions
			shadow[i].transform.position = Vector3.MoveTowards ( shadow[i].transform.position, shadPos[i], 1000f);
	}

	void swapVector3 (ref Vector3 v1, ref Vector3 v2)
	{
		Vector3 tempswap = v1;  
		v1 = v2;  
		v2 = tempswap; 
	}
	
	void swapInt (ref int x, ref int y)
	{
		int tempswap = x;  
		x = y;  
		y = tempswap;
	}

}