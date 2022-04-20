using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class swipDetection : MonoBehaviour
{
    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;
	public Rigidbody2D rb;
    public bool detectSwipeAfterRelease = true;
	private float speed = 140f;
    public float SWIPE_THRESHOLD = 20f;
	public ParticleSystem particle;
	int sceneIndex = 0;
	public Text text;
	// this is for line renderer

	// Start is called before the first frame update
	void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
			text.text = "touch Detected";
			for(int i=0; i<=Input.touchCount; i++)
            {
				Touch touch = Input.GetTouch(i);
				if (touch.phase == TouchPhase.Began)
				{
					fingerUpPos =Camera.main.ScreenToWorldPoint(touch.position) ;
					fingerDownPos = Camera.main.ScreenToWorldPoint(touch.position);
					text.text = "touch Begun";
					// this is for renderer

				}

				//Detects Swipe while finger is still moving on screen
				if (touch.phase == TouchPhase.Moved)
				{
					text.text = "touch moved";
					if (!detectSwipeAfterRelease)
					{
						fingerDownPos = touch.position;
						DetectSwipe();
					}
				}

				//Detects swipe after finger is released from screen
				if (touch.phase == TouchPhase.Ended)
				{
					text.text = "touch ended";
					fingerDownPos = touch.position;
					DetectSwipe();
				}
			}
			
			
		}
		
	}


	void DetectSwipe()
	{

		if (VerticalMoveValue() > SWIPE_THRESHOLD && VerticalMoveValue() > HorizontalMoveValue())
		{
			Debug.Log("Vertical Swipe Detected!");
			if (fingerDownPos.y - fingerUpPos.y > 0)
			{
				OnSwipeUp();
			}
			else if (fingerDownPos.y - fingerUpPos.y < 0)
			{
				OnSwipeDown();
			}
			fingerUpPos = fingerDownPos;

		}
		else if (HorizontalMoveValue() > SWIPE_THRESHOLD && HorizontalMoveValue() > VerticalMoveValue())
		{
			Debug.Log("Horizontal Swipe Detected!");
			if (fingerDownPos.x - fingerUpPos.x > 0)
			{
				OnSwipeRight();
			}
			else if (fingerDownPos.x - fingerUpPos.x < 0)
			{
				OnSwipeLeft();
			}
			fingerUpPos = fingerDownPos;

		}
		else
		{
			Debug.Log("No Swipe Detected!");
		}
	}
	float VerticalMoveValue()
	{
		return Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
	}

	float HorizontalMoveValue()
	{
		return Mathf.Abs(fingerDownPos.x - fingerUpPos.x);
	}

	void OnSwipeUp()
	{
		//Do something when swiped up
		Debug.Log("Player moved up");
		rb.velocity = Vector2.up * speed * Time.deltaTime;
		soundManagerScript.PlaySound("swip");
	
	}

	void OnSwipeDown()
	{
		//Do something when swiped down
		Debug.Log("Player moved down");
		rb.velocity = Vector2.down * speed * Time.deltaTime;
		soundManagerScript.PlaySound("swip");

	}

	void OnSwipeLeft()
	{
		//Do something when swiped left
		Debug.Log("Player moved left");
		rb.velocity = Vector2.left * speed * Time.fixedDeltaTime;
		soundManagerScript.PlaySound("swip");

	}

	void OnSwipeRight()
	{
		//Do something when swiped right
		Debug.Log("Player moved right");
		rb.velocity = Vector2.right * speed * Time.fixedDeltaTime;
		soundManagerScript.PlaySound("swip");

	}

    // this is for trigger enter 
    private void OnTriggerEnter2D(Collider2D collision)
    {
		if(collision.tag== "nextLevel")
        {
			Destroy(collision.gameObject);
			particle.Play();
			soundManagerScript.PlaySound("nextLevel");
			sceneIndex = sceneIndex + 1;
			Invoke("loadNextLeval", 1);
		}
	
	
	}

	// void next level
	void loadNextLeval()
    {
		SceneManager.LoadScene(sceneIndex);
    }
}
