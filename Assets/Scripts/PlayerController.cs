using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private Animator anim;

    //private Transform playerPos;
    //[SerializeField] private bool lane1 = false;
    //[SerializeField] private bool lane2 = true;
    //[SerializeField] private bool lane3 = false;

    public float swipeThreshold = 50f; // Adjust this value to set the minimum swipe distance

    public float laneDistance = 1f;

    [SerializeField] private int desiredLane = 1; //0 Kiri, 1 Tengah, 2 Kanan

    private Vector3 direction;
    private CharacterController controller;
    public float forwardSpeed;
    public float maxSpeed;
    private bool isSliding = false;

    public float jumpForce;
    public float gravity = -20;

    public static bool isInvincible;

    public GameObject parent;

    public UIManager uiManager;
    private bool upDone;

    [Header("Player Rotation")]
    private bool isRotating = false;
    public Quaternion initialRotation, targetRotation;
    public float durationRotating;
    public bool isGo = false;


    [Header("Data Kriteria")]
    public int jumlahLompat;
    public int jumlahSlide;
    public int jumlahBerpindah;
    public float kecepatan;
    public void LoadData(GameData data)
    {

    }

    public void SaveData(ref GameData data)
    {
        //data.jumlahLompat = jumlahLompat;
        //data.jumlahSlide = jumlahSlide;
        //data.jumlahBerpindah = jumlahBerpindah;
        //data.kecepatan = kecepatan;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        //playerPos = GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        isInvincible = false;
        transform.rotation = initialRotation;
        upDone = false;
    }
    void Update()
    {
        parent.transform.position = transform.position;
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            GameManager.isPlay = true;
        }
        
        if (GameManager.isPlay)
        {
            if (!isRotating)
            {
                StartCoroutine(RotatePlayer(targetRotation, durationRotating));
            }
        }

        if (GameManager.isStartGame)
        {
            anim.SetBool("StartPlay", true);
            CheckSwipe();
            CheckLane();
            if (forwardSpeed < maxSpeed)
            {
                forwardSpeed += 0.1f * Time.deltaTime * uiManager.statChoose.jKecepatan;
            }

            direction.z = forwardSpeed;

            if (controller.isGrounded == false)
            {
                direction.y += gravity * Time.deltaTime;
            }

            kecepatan = (forwardSpeed / uiManager.statChoose.jKecepatan) * 2;
            
            if(!upDone)
            {
                jumpForce += uiManager.statChoose.jLompat;
                upDone = true;
            }
        }
    }

    IEnumerator RotatePlayer(Quaternion targetRotation, float duration)
    {
        isRotating = true;
        float timer = 0f;
        Quaternion startedRotation = initialRotation;
        while (timer < duration)
        {
            float t = timer / duration;
            transform.rotation = Quaternion.Lerp(startedRotation, targetRotation, t);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    void CheckSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fingerDownPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            fingerUpPosition = Input.mousePosition;
            DetectSwipe();
        }
    }

    void DetectSwipe()
    {
        float swipeDistance = Vector2.Distance(fingerDownPosition, fingerUpPosition);

        if (swipeDistance > swipeThreshold)
        {
            Vector2 swipeDirection = fingerUpPosition - fingerDownPosition;
            swipeDirection.Normalize();

            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                // Horizontal Swipe
                if (swipeDirection.x > 0)
                {
                    //Debug.Log("Swipe Right");
                    // Move player right
                    SwipeRight();
                    jumlahBerpindah += 1;
                }
                else
                {
                   // Debug.Log("Swipe Left");
                    // Move player left
                    SwipeLeft();
                    jumlahBerpindah += 1;
                }
            }
            else
            {
                // Vertical Swipe
                if (swipeDirection.y > 0)
                {
                    //Debug.Log("Swipe Up");
                    // Move player up
                    if (controller.isGrounded)
                    {
                        anim.SetTrigger("Jump");
                        Jump();
                        jumlahLompat += 1;
                    }
                    
                    
                }
                else
                {
                    if (!isSliding)
                    {
                        //Debug.Log("Swipe Down");
                        // Move player down
                        anim.SetTrigger("Slide");
                        StartCoroutine(Slide());
                        jumlahSlide += 1;
                    }
                    
                }
            }
        }
    }

    void SwipeRight()
    {
        //if (lane3 == false && lane1 == true)
        //{
        //    lane2 = true;
        //    lane1 = false;
        //    lane3 = false;
        //}
        //else if (lane2 == true && playerPos.position.x >= -0.2f)
        //{
        //    lane3 = true;
        //    lane2 = false;
        //    lane1 = false;
        //}

        desiredLane++;
        if (desiredLane >= 3)
        {
            desiredLane = 2;
        }
    }

    void SwipeLeft()
    {
        //if (lane1 == false && lane3 == true)
        //{
        //    lane2 = true;
        //    lane3 = false;
        //    lane1 = false;
        //}
        //else if (lane2 == true && playerPos.position.x >= -0.2f)
        //{
        //    lane1 = true;
        //    lane2 = false;
        //    lane3 = false;
        //}

        desiredLane--;
        if (desiredLane <= -1)
        {
            desiredLane = 0;
        }
    }

    void CheckLane()
    {
        //if (lane3 == true && playerPos.position.x < 0.5f)
        //{
        //    playerPos.position += new Vector3(1f, 0, 0);
        //}
        //else if (lane1 == true && playerPos.position.x > -0.5f)
        //{
        //    playerPos.position += new Vector3(-1f, 0, 0);
        //}
        //else if (lane2 == true && playerPos.position.x <= -0.1f)
        //{
        //    playerPos.position += new Vector3(1f, 0, 0);
        //}
        //else if (lane2 == true && playerPos.position.x >= 0.1f)
        //{
        //    playerPos.position += new Vector3(-1f, 0, 0);
        //}

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        // transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.fixedDeltaTime);
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }
       
    }

    void Jump()
    {
        direction.y = jumpForce;
    }

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.transform.tag == "Obstacle")
    //    {
    //        if (!isInvincible)
    //        {
    //            FindObjectOfType<AudioManager>().StopSound("MainTheme");
    //            FindObjectOfType<AudioManager>().PlaySound("GameOver");
    //            PlayerManager.gameOver = true;
    //            Debug.Log("test");
    //        }

    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (!isInvincible)
            {
                //FindObjectOfType<AudioManager>().StopSound("MainTheme");
                //FindObjectOfType<AudioManager>().PlaySound("GameOver");
                PlayerManager.gameOver = true;
            }
        }
    }


    IEnumerator Slide()
    {
        isSliding = true;
        controller.center = new Vector3(0, 0.3f, 0);
        controller.radius = 0.1f;
        controller.height = 0.1f;

        yield return new WaitForSeconds(1f);

        isSliding = false;
        controller.center = new Vector3(0, 0.7f, 0);
        controller.radius = 0.47f;
        controller.height = 1.4f;
    }
}
