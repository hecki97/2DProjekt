using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MovingObject {

    public float restartLevelDelay = 1f;
    public int pointsPerFood = 10;
    //public int pointsPerSoda = 20;
    public int pointsPerPizza = 20;
    public int wallDamage = 1;
    public Text foodText;
    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip eatSound1;
    public AudioClip eatSound2;
    public AudioClip coinPickupSound1;
    public AudioClip coinPickupSound2;
    //public AudioClip drinkSound1;
    //public AudioClip drinkSound2;
    public AudioClip gameOverSound;

    public enum Direction { Nord, South, West, East };
    public Direction direction;

    private Vector3 endPos;
    private bool isLerping = false;
    private float timeStartedLerping;
    
    private Transform cam;

    private Animator animator;
    private int food;

    public Text coinText;
    private int coins;

    private Vector2 touchOrigin = -Vector2.one;

	void Start () {
        animator = GetComponent<Animator>();
        food = GameManager.instance.playerFoodPoints;
        //
        direction = Direction.Nord;
        if (GameManager.instance.gameMode == GameMode.ThreeD)
             cam = GameObject.Find("Camera").transform;

        coins = GameManager.instance.playerCoinsCount;
        coinText.text = "C x " + coins;
        foodText.text = "Food: " + food;
        base.Start();
	}

    private void OnDisable()
    {
        GameManager.instance.playerFoodPoints = food;
        GameManager.instance.playerCoinsCount = coins;
    }

	// Update is called once per frame
	void Update () {
        if (!GameManager.instance.playersTurn) return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int) (Input.GetAxisRaw("Horizontal"));
        vertical = (int) (Input.GetAxisRaw("Vertical"));

        if (horizontal != 0)
            vertical = 0;

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

        //Test!!
        if (GameManager.instance.gameMode == GameMode.TwoD)
        {
            if (horizontal != 0 || vertical != 0)
                AttemptMove<Wall>(horizontal, vertical);
        }
        else
        {
            if (horizontal != 0 || vertical != 0 && !isLerping)
            {
                //AttemptMove<Wall>(horizontal, vertical);
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

            cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, Quaternion.Euler(endPos), percentageComplete);

            if (percentageComplete >= .8f)
                isLerping = false;
        }
    }

    IEnumerator SetDirection(Direction _dir)
    {
        yield return new WaitForSeconds(.25f);
        direction = _dir;
    }

    void StartLerping(Vector3 _endPos)
    {
        endPos = _endPos;
        timeStartedLerping = Time.time;
        isLerping = true;
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        food--;
        foodText.text = "Food: " + food;
        base.AttemptMove<T>(xDir, yDir);

        //RaycastHit2D hit;
        //if (Move(xDir, yDir, out hit))
        //    SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Food")
        {
            food += pointsPerFood;
            foodText.text = "Food: " + food + " +" + pointsPerFood;
            SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Pizza")
        {
            food += pointsPerPizza;
            foodText.text = "Food: " + food + " +" + pointsPerPizza;
            SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Coin")
        {
            coins++;
            coinText.text = "C x " + coins;
            SoundManager.instance.RandomizeSfx(coinPickupSound1, coinPickupSound2);
            other.gameObject.SetActive(false);
        }
    }

	public void OnPickup(ItemTypes type, int pointsPerItem) {
		switch (type) {
			case ItemTypes.Exit:
				Invoke("Restart", restartLevelDelay);
				enabled = false;
				break;
			case ItemTypes.Food:
				food += pointsPerItem;
				foodText.text = "Food: " + food + " +" + pointsPerItem;
				break;
		}
	}

    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoseFood(int loss)
    {
        //animator.SetTrigger("playerHit");
        food -= loss;
        foodText.text = "Food: " + food + " -" + loss;
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        if (food <= 0)
        {
            SoundManager.instance.PlaySingle(gameOverSound);
            SoundManager.instance.musicSource.Stop();
            GameManager.instance.GameOver();
        }
    }
}
