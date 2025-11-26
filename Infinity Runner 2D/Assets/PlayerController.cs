using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float tempoIma;
        public float tempoBranco;
        public float movingSpeed;
        public float jumpForce;
        public float deltaSpeed = 0.001F;
        private float moveInput;
        private bool doubleJump = true;

        private bool facingRight = false;
        [HideInInspector]
        public bool deathState = false;

        private bool isGrounded;
        public Transform groundCheck;

        private Rigidbody2D rigidbody;
        private Animator animator;
        private GameManager gameManager;

        public Vector2 lightningHeight = new Vector2(0, 5f);

        public GameObject lightning;
        public SpriteRenderer tela_branca;

        Placar placar;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            placar = GameObject.Find("Placar").GetComponent<Placar>();
            tela_branca = GameObject.Find("tela_branca").GetComponent<SpriteRenderer>();
            // gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        private void FixedUpdate()
        {
            CheckGround();
        }

        void Update()
        {
            animator.SetFloat("magnetictime", tempoIma);
            if(tempoIma > 0) {
                tempoIma -= Time.deltaTime;
                float randomNumber = Random.Range(1F, 1000F);
                if(randomNumber < 10F) {
                    GameObject[] moedas = GameObject.FindGameObjectsWithTag("Moeda");
                    float x = transform.position.x;
        foreach (GameObject obj in moedas)
        {
                    Vector2 moedaTransform = obj.transform.position;
            if(Mathf.Abs(moedaTransform.x - x) < 9) {
                Instantiate(lightning,moedaTransform+=lightningHeight, Quaternion.identity);
                obj.GetComponent<Animator>().SetBool("alive", false);
                string name = obj.name;
                if(name == "VirusDefault") {
                    placar.placar +=20;
                }else if(name == "VirusSilver") {
                    placar.placar += 50;
                }else {
                    placar.placar += 125;
                }
            }
            
        }
                }
            }
            if(tempoBranco > 0) {
                tempoBranco -= Time.deltaTime;
                float randomNumber = Random.Range(1F, 1000F);
                if(randomNumber < 20F && tela_branca.enabled == false) {
                    tela_branca.enabled = true;
                } else if(randomNumber < 50F) {
                    tela_branca.enabled = false;
                }
                if(tempoBranco < 1) {
                                        tela_branca.enabled = false;
                                        tempoBranco = 0F;
                }
            } 
           movingSpeed += Time.deltaTime*deltaSpeed;

            moveInput = 1;
            Vector3 direction = transform.right * moveInput;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime);
            animator.SetInteger("playerState", 1); // Turn on run animation

            if (isGrounded) {
                doubleJump = true;
            }

            if (Input.GetKeyDown(KeyCode.Space) || ToqueDePulo() &&
                (isGrounded || (!isGrounded && doubleJump)))
            {
                if (!isGrounded)
                {
                    doubleJump = false;
                    rigidbody.velocity =
                        new Vector2(rigidbody.velocity.x, 0);
                }
                rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
            if (!isGrounded)animator.SetInteger("playerState", 2); // Turn on jump animation

            if(facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if(facingRight == true && moveInput < 0)
            {
                Flip();
            }
        }

        bool ToqueDePulo()
{
    if (Input.GetMouseButtonDown(0))
        return true;

    if (Input.touchCount > 0)
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
                return true;
        }
    }

    return false;
}

        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f);
            isGrounded = colliders.Length > 1;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                deathState = true; // Say to GameManager that player is dead
            }
            else
            {
                deathState = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Coin")
            {
                gameManager.coinsCounter += 1;
                Destroy(other.gameObject);
            }
        }
    }
}
