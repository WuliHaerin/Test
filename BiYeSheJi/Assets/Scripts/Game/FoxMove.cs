using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoxMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float runspeed;
    public Rigidbody2D rb;
    public float JumpSpeed;
    public Animator ani;
    public LayerMask ground;
    public Collider2D cld;
    public bool isPressedJump;
    public Transform cellingCheck;
    public Collider2D disBoxClose;
    private bool isHurt;
    public int foxLive = 5;
    public GameObject HpValue;
    public Text hpText;
    public GameObject deadPanel;
    public AudioSource hurtSound;

    public static FoxMove _instance;

    

    void Start()
    {
      
    }

    private void Awake()
    {
        _instance = this;
        if (PlayerPrefs.GetInt("flag",1)==1)
        {
            foxLive = PlayerPrefs.GetInt("liveValue", foxLive);
            HpValue.transform.localScale = new Vector3(foxLive / 5f, 1, 1);
            hpText.text = foxLive.ToString() + "/5";
            PlayerPrefs.SetInt("flag", 0);
        }
    }

    void Restart()
    {
        //foxLive -= 1;
        SetHealth(--foxLive);
        PlayerPrefs.SetInt("liveValue", foxLive);
        PlayerPrefs.SetInt("flag", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SetHealth(int live)
    {
        if(live>=1)
        {
            HpValue.transform.localScale = new Vector3(live / 5f, 1, 1);
            hpText.text = live.ToString() + "/5";
        }
        else
        {
            HpValue.transform.localScale = new Vector3(live / 5f, 1, 1);
            hpText.text = live.ToString() + "/5";
            deadPanel.SetActive(true);
            Destroy(this.gameObject);
           
            
        }
    }

    void BackMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 0;
    }

    //Update is called once per frame
    void Update()
    {

        ChangeAni();
        isPressedJump = Input.GetButton("Jump");
        Crouch();

    }

    private void FixedUpdate()
    {
        if (!isHurt)
        {
            Movement();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collection")
        {
            //collision.SendMessage("Die", 0.8f);
            Destroy(collision);
            collision.GetComponent<Animator>().SetTrigger("isGet");
            //GameManager._instance.score = GameManager._instance.score + 1;
        }

        else if (collision.tag == "DeadLine")
        {
            Invoke("Restart", 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            if (ani.GetBool("falling"))
            {
                collision.gameObject.SendMessage("SetDieAnim");
                rb.velocity = new Vector2(rb.velocity.x, JumpSpeed * Time.deltaTime);
                ani.SetBool("jumping", true);
                //GameManager._instance.score = GameManager._instance.score + 2;
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                SetHealth(--foxLive);
                hurtSound.Play();
                rb.velocity = new Vector2(-4, rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                SetHealth(--foxLive);
                hurtSound.Play();
                rb.velocity = new Vector2(4, rb.velocity.y);
                isHurt = true;
            }



        }
    }

    void Movement()
    {
        //获取水平轴信息，h指是一个-1~1的无限区间
        float h = Input.GetAxis("Horizontal");
        //获取水平轴信息，h的值一个只有-1，0，1三个数，停止不动是0
        float faceDirection = Input.GetAxisRaw("Horizontal");

        if (h != 0)
        {
            rb.velocity = new Vector2(h * runspeed * Time.fixedDeltaTime, rb.velocity.y);
            ani.SetFloat("running",/*0.2f*/Mathf.Abs(faceDirection));
        }
        else if (h == 0)
        {
            ani.SetFloat("running", 0);
        }

        if (faceDirection != 0)
        {
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }

        if (/*Input.GetButtonDown("Jump")*/isPressedJump && cld.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpSpeed * Time.fixedDeltaTime);
            ani.SetBool("jumping", true);
        }

    }

    void Crouch()
    {
        if (!Physics2D.OverlapCircle(cellingCheck.position, 0.05f, ground))
        {
            if (Input.GetButton("Crouch"))
            {
                ani.SetBool("crouching", true);
                disBoxClose.enabled = false;
            }
            else
            {
                ani.SetBool("crouching", false);
                disBoxClose.enabled = true;
            }
        }
    }
    void ChangeAni()
    {
        if (ani.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                ani.SetBool("jumping", false);
                ani.SetBool("falling", true);
            }
        }

        else if (isHurt)
        {
            ani.SetBool("hurting", true);
            ani.SetBool("jumping", false);
            ani.SetFloat("running", 0);
            ani.SetBool("idle", false);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                isHurt = false;
                ani.SetBool("idle", true);
                ani.SetBool("hurting", false);
            }
        }

        else if (cld.IsTouchingLayers(ground))
        {
            ani.SetBool("idle", true);
            ani.SetBool("falling", false);
        }

    }

    void Die()
    {
        Destroy(this.gameObject);
    }



}
