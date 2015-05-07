using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MovingObject {

    public int pointsPerFood = 10;
    public int pointsPerPizza = 20;
    public int wallDamage = 1;
    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip eatSound1;
    public AudioClip eatSound2;
    public AudioClip drinkSound1;
    public AudioClip drinkSound2;
    public AudioClip gameOverSound;

    public float speed;
    private MazeDirection currentDirection;
    private Vector3 endPos;
    //public bool isLerping = false;
    //private float timeStartedLerping;

    private Transform cam;

    private Vector2 touchOrigin = -Vector2.one;

	protected override void Start ()
    {
        if (GameManager.instance.gameMode == GameMode.ThreeD)
             cam = GameObject.Find("Camera").transform;

        ItemGenericCollider.OnPickup += ItemGenericCollider_OnPickup;
        base.Start();
	}

    void OnDisable()
    {
        ItemGenericCollider.OnPickup -= ItemGenericCollider_OnPickup;
    }

    void ItemGenericCollider_OnPickup(ItemType type)
    {
        switch (type)
        {
            case ItemType.Food:
                SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
                break;
        }
    }

	// Update is called once per frame
	void Update () {
        if (!GameManager.instance.playersTurn) return;
        int horizontal = 0;
        int vertical = 0;

        horizontal = (int) (Input.GetAxisRaw("Horizontal"));
        vertical = (int) (Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            endPos.z = ((transform.localEulerAngles.z + 360f - 90f) % 360f);
            currentDirection = currentDirection.GetNextClockWise();
        } 

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            endPos.z = ((transform.localEulerAngles.z + 90f) % 360f);
            currentDirection = currentDirection.GetNextCounterClockWise();
        }
            
        if (endPos.z != transform.localEulerAngles.z)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(endPos), speed * Time.deltaTime);

        if (vertical != 0 && !GameManager.instance.isPaused)
        {
            switch (currentDirection)
            {
                case MazeDirection.North:
                    AttemptMove<Wall>(0, vertical);
                    break;
                case MazeDirection.East:
                    AttemptMove<Wall>(vertical, 0);
                    break;
                case MazeDirection.South:
                    AttemptMove<Wall>(0, -vertical);
                    break;
                case MazeDirection.West:
                    AttemptMove<Wall>(-vertical, 0);
                    break;
            }
        }

        //if (horizontal != 0)
        //    vertical = 0;

		if (Input.touchCount > 0) {

			Touch touch = Input.touches[0];

			if (touch.phase == TouchPhase.Began)
				touchOrigin = touch.position;


			if (touch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
			{
				Vector2 touchEnd = touch.position;
				float x = touchEnd.x - touchOrigin.x;
				float y = touchEnd.y - touchOrigin.y;
				touchOrigin.x = -1;

				if (Mathf.Abs(x) > Mathf.Abs(y))
					horizontal = x > 0 ? 1 : -1;
				else
					vertical = y > 0 ? 1 : -1;
			}
		}

        /*
        //Test!!
        if (GameManager.instance.gameMode == GameMode.TwoD)
        {
            if (horizontal != 0 || vertical != 0)
                AttemptMove<Wall>(horizontal, vertical);
        }
        else
        {

            if (horizontal != 0 || vertical != 0 && !isLerping && !GameManager.instance.isPaused)
            {
                switch (direction)
                {
                    case Direction.Nord:
                        if (horizontal == 1) {
                            StartLerping(new Vector3(0f, 90f, 270f));
                            StartCoroutine(SetDirection(Direction.East));
                        }
                        else if (horizontal == -1) {
                            StartLerping(new Vector3(0f, 270f, 90f));
                            StartCoroutine(SetDirection(Direction.West));
                        }

                        if (vertical != 0)
                            AttemptMove<Wall>(0, vertical);
                        break;
                    case Direction.South:
                        if (horizontal == -1) {
                            StartLerping(new Vector3(0f, 90f, 270f));
                            StartCoroutine(SetDirection(Direction.East));
                        }
                        else if (horizontal == 1) {
                            StartLerping(new Vector3(0f, 270f, 90f));
                            StartCoroutine(SetDirection(Direction.West));
                        }

                        if (vertical != 0)
                            AttemptMove<Wall>(0, -vertical);
                        break;
                    case Direction.West:
                        if (horizontal == 1)
                        {
                            StartLerping(new Vector3(270f, 0f, 0f));
                            StartCoroutine(SetDirection(Direction.Nord));
                        }
                        else if (horizontal == -1) {
                            StartLerping(new Vector3(90f, 180f, 0f));
                            StartCoroutine(SetDirection(Direction.South));
                        }

                        if (vertical != 0)
                            AttemptMove<Wall>(-vertical, 0);
                        break;
                    case Direction.East:
                        if (horizontal == 1) {
                            StartLerping(new Vector3(90f, 180f, 0f));
                            StartCoroutine(SetDirection(Direction.South));
                        }
                        else if (horizontal == -1) {
                            StartLerping(new Vector3(270f, 0f, 0f));
                            StartCoroutine(SetDirection(Direction.Nord));
                        }

                        if (vertical != 0)
                            AttemptMove<Wall>(vertical, 0);
                        break;
                }
            }
        }

        if (isLerping)
        {
            float timeSinceStarted = Time.time - timeStartedLerping;
            float percentageComplete = timeSinceStarted / .5f;

            //cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, Quaternion.Euler(endPos), percentageComplete);
            //transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, endPos, percentageComplete);

            if (percentageComplete >= 1f)
                isLerping = false;
        }
        */ 
    }

    /*
    IEnumerator SetDirection(MazeDirection _dir)
    {
        yield return new WaitForSeconds(.25f);
        currentDirection = _dir;
    }

    void StartLerping(Vector3 _endPos)
    {
        endPos = _endPos;
        timeStartedLerping = Time.time;
        isLerping = true;
    }
    */

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        base.AttemptMove<T>(xDir, yDir);

        RaycastHit hit;
        if (Move(xDir, yDir, out hit))
        {
            SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
            GameManager.instance.foodPoints--;
        }
        CheckIfGameOver();
        GameManager.instance.playersTurn = false;
    }

    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        //animator.SetTrigger("playerChop");

        //Enemy hitEnemy = component as Enemy;
        //hitEnemy.LoseHealth(wallDamage);
        Debug.Log("Hit!");
    }

    public void LoseFood(int loss)
    {
        //animator.SetTrigger("playerHit");
        GameManager.instance.foodPoints -= loss;
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        if (GameManager.instance.foodPoints <= 0)
        {
            SoundManager.instance.PlaySingle(gameOverSound);
            SoundManager.instance.musicSource.Stop();
            GameManager.instance.GameOver();
        }
    }
}
