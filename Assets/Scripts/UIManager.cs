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
	private bool itemDragged = false;



	void Start () 
	{
		//image0 is invisible image just to fill slot and start main array from 1, not 0
		image = new GameObject[6]  {image0, image1, image2, image3, image4, image5 };
		shadow = new GameObject[6]  {image0, shadow1, shadow2, shadow3, shadow4, shadow5 };
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
			image[1].transform.position = Vector3.MoveTowards ( image[1].transform.position, defPos[1], 10f);
			image[2].transform.position = Vector3.MoveTowards ( image[2].transform.position, defPos[2], 10f);
			image[3].transform.position = Vector3.MoveTowards ( image[3].transform.position, defPos[3], 10f);
			image[4].transform.position = Vector3.MoveTowards ( image[4].transform.position, defPos[4], 10f);
			image[5].transform.position = Vector3.MoveTowards ( image[5].transform.position, defPos[5], 10f);
		}

		matchCheck ();

	}

	//compare MousePos with DefPos and return number of DefPos if any nearby
	int calculatePosition ()
	{
		if 		(Mathf.Abs (Input.mousePosition.x - defPos [1].x) < 50 &&
		      	 Mathf.Abs (Input.mousePosition.y - defPos [1].y) < 50)
			return 1;
		else if (Mathf.Abs (Input.mousePosition.x - defPos [2].x) < 50 &&
		         Mathf.Abs (Input.mousePosition.y - defPos [2].y) < 50)
			return 2;
		else if (Mathf.Abs (Input.mousePosition.x - defPos [3].x) < 50 &&
		         Mathf.Abs (Input.mousePosition.y - defPos [3].y) < 50)
			return 3;
		else if (Mathf.Abs (Input.mousePosition.x - defPos [4].x) < 50 &&
		         Mathf.Abs (Input.mousePosition.y - defPos [4].y) < 50)
			return 4;
		else if (Mathf.Abs (Input.mousePosition.x - defPos [5].x) < 50 &&
		         Mathf.Abs (Input.mousePosition.y - defPos [5].y) < 50)
			return 5;
		else
			return 0;

	}

	void getImagePos()
	{
		defPos[0] = image[0].transform.position;
		defPos[1] = image[1].transform.position;
		defPos[2] = image[2].transform.position;
		defPos[3] = image[3].transform.position;
		defPos[4] = image[4].transform.position;
		defPos[5] = image[5].transform.position;

		shadPos[0] = shadow[0].transform.position;
		shadPos[1] = shadow[1].transform.position;
		shadPos[2] = shadow[2].transform.position;
		shadPos[3] = shadow[3].transform.position;
		shadPos[4] = shadow[4].transform.position;
		shadPos[5] = shadow[5].transform.position;
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

			// order must be counted from other side AND I DONT KNOW WHY
			itemToSwap = 6 - itemToSwap;
			itemToSwapWith = 6 - itemToSwapWith;

			shadowOrder [0] = shadowOrder[itemToSwap];
			shadowOrder [itemToSwap] = shadowOrder[itemToSwapWith];
			shadowOrder [itemToSwapWith] = shadowOrder[0];
		}
		shadow[1].transform.position = Vector3.MoveTowards ( shadow[1].transform.position, shadPos[1], 1000f);
		shadow[2].transform.position = Vector3.MoveTowards ( shadow[2].transform.position, shadPos[2], 1000f);
		shadow[3].transform.position = Vector3.MoveTowards ( shadow[3].transform.position, shadPos[3], 1000f);
		shadow[4].transform.position = Vector3.MoveTowards ( shadow[4].transform.position, shadPos[4], 1000f);
		shadow[5].transform.position = Vector3.MoveTowards ( shadow[5].transform.position, shadPos[5], 1000f);

	}

	void matchCheck ()
	{
		if (matrOrder [1] == shadowOrder [1] &&
		    matrOrder [2] == shadowOrder [2] &&
		    matrOrder [3] == shadowOrder [3] &&
		    matrOrder [4] == shadowOrder [4] &&
		    matrOrder [5] == shadowOrder [5])
			image[0].transform.position = Vector3.MoveTowards ( image[0].transform.position, defPos[1], 10f);
	}

}
