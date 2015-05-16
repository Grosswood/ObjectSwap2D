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
	private int[] matrOrder = new int[6]  { 0, 1, 2, 3, 4, 5 };
	private int[] shadowOrder = new int[6] { 0, 5, 4, 3, 2, 1 };
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
			swapPoses(firstObj, secondObj);

			if (secondObj == 0)  //if dragged to nowhere just go back
				secondObj = firstObj;

			itemDragged = false;
		}

		if (!Input.GetMouseButton (0))
		{
			for (int i = 1; i < 6; i++)
				image[i].transform.position = Vector3.MoveTowards ( image[i].transform.position, defPos[i], 10f);
		}

		matchCheck (); //finish if shadowOrder and matrOrder are equal

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

	void swapPoses(int v1, int v2)
	{
		defPos[0] = defPos[v1];
		defPos[v1] = defPos[v2];
		defPos[v2] = defPos[0];

		matrOrder[0] = matrOrder[v1];
		matrOrder[v1] = matrOrder[v2];
		matrOrder[v2] = matrOrder[0];
	}

	void swapShadowOrder ()
	{
		for (int i = 0; i < 10; i++)
		{
			int itemToSwap = Random.Range (1 , 6);
			int itemToSwapWith = Random.Range (1 , 6);

			shadPos [0] = shadPos[itemToSwap];
			shadPos [itemToSwap] = shadPos[itemToSwapWith];
			shadPos [itemToSwapWith] = shadPos[0];

			shadowOrder [0] = shadowOrder[itemToSwap];
			shadowOrder [itemToSwap] = shadowOrder[itemToSwapWith];
			shadowOrder [itemToSwapWith] = shadowOrder[0];
		}
		for (int i = 1; i < 6; i++) //swap shadows to new positions
			shadow[i].transform.position = Vector3.MoveTowards ( shadow[i].transform.position, shadPos[i], 1000f);
	}

	void matchCheck ()
	{
		for (int i = 1; i < 6; i++)
			if (matrOrder [i] != shadowOrder [i])
				return;
		image[0].transform.position = Vector3.MoveTowards ( image[0].transform.position, defPos[1], 10f);
	}
}
