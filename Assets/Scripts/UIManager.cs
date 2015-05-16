using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GameObject image0;
	public GameObject image1;
	public GameObject image2;
	public GameObject image3;
	public GameObject image4;
	public GameObject image5;

	private GameObject[] image;
	
	private Vector3[] defPos = new Vector3[6];
	private int firstObj;
	private int secondObj;
	private bool itemDragged = false;


	void Start () 
	{
		image = new GameObject[6]  {image0, image1, image2, image3, image4, image5 };
		getImagePos ();
	}
	


	void Update ()
	{
		if (Input.GetMouseButton (0))
		{
			if (itemDragged)
			{
				image[firstObj].transform.position =Vector3.MoveTowards (image[firstObj].transform.position, Input.mousePosition, 100f);
			}


			//image1.transform.position = Input.mousePosition;
			//image1.transform.position = defPos[0];
		}

		if (Input.GetMouseButtonDown (0))
		{
			firstObj = calculatePosition();
			itemDragged = true;
		}

		if (Input.GetMouseButtonUp (0))
		{
			secondObj = calculatePosition();

			defPos[0] = defPos[firstObj];
			defPos[firstObj] = defPos[secondObj];
			defPos[secondObj] = defPos[0];

			if (secondObj == 0)
			{
				secondObj = firstObj;
			}

			itemDragged = false;
		}

		if (!Input.GetMouseButton (0))
		{
			image[1].transform.position = Vector3.MoveTowards ( image[1].transform.position, defPos[1], 10f);
			image[2].transform.position = Vector3.MoveTowards ( image[2].transform.position, defPos[2], 10f);
			image[3].transform.position = Vector3.MoveTowards ( image[3].transform.position, defPos[3], 10f);
			image[4].transform.position = Vector3.MoveTowards ( image[4].transform.position, defPos[4], 10f);
			image[5].transform.position = Vector3.MoveTowards ( image[5].transform.position, defPos[5], 10f);

			//image[firstObj].transform.position = Vector3.MoveTowards (image[firstObj].transform.position, defPos[secondObj], 10f);
		}
	}

	//compare MousePos with DefPos and return number of DefPos if any nearby
	int calculatePosition ()
	{
		if (Mathf.Abs (Input.mousePosition.x - defPos [1].x) < 50 &&
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
		
		
		/*if ( Mathf.Abs(Input.mousePosition.x - defPos [0].x) + 50 &&
			Input.mousePosition.x > defPos [0].x - 50 &&
			Input.mousePosition.y < defPos [0].y + 50 &&
			Input.mousePosition.y > defPos [0].y - 50)
			return 1;
		else if (Input.mousePosition.x < defPos [0].x + 50 &&
		         Input.mousePosition.x < defPos [0].x + 50 &&
		         Input.mousePosition.x < defPos [0].x + 50 &&
		         Input.mousePosition.x < defPos [0].x + 50)
			return 1;
		else
			return 0;
*/


	}

	void getImagePos()
	{
		defPos[0] = image[0].transform.position;
		defPos[1] = image[1].transform.position;
		defPos[2] = image[2].transform.position;
		defPos[3] = image[3].transform.position;
		defPos[4] = image[4].transform.position;
		defPos[5] = image[5].transform.position;
	}

}
