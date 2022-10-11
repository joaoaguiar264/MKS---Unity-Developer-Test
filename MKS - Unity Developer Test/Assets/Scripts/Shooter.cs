using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject shooter, explode, cannonBall, frontalCannon, hpBar, cannon;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float moveSpeed, frontalCD, ballForce, health = 3;
    private Vector3 localScale;
    [SerializeField] private Animator shooterAnim, explosionAnim;
    [SerializeField] private bool frontalReady = true, inRange = false, fadeOut = false;
    private SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        shooterAnim = this.gameObject.GetComponent<Animator>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        rend = gameObject.GetComponent<SpriteRenderer>();
        localScale = hpBar.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //FADE OUT
        if(fadeOut == true)
        {
            fadeOut = false;
            StartCoroutine(FadeOut());
        }

        //HP BAR
        hpBar.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        localScale.x = health / 3;
        hpBar.transform.localScale = localScale;

        //HP ANIM
        shooterAnim.SetInteger("Health", (int)health);

        //ATTACK TRIGGER
        if (health > 0 && Player.health > 0 && GameManager.survived == false)
        {
            if(inRange == false)
            {
                Vector2 direction = playerTransform.transform.position - transform.position;
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

                transform.position = Vector2.MoveTowards(this.transform.position, playerTransform.transform.position, moveSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }
            else if(inRange == true)
            {
                Vector2 direction = playerTransform.transform.position - transform.position;
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
                if(frontalReady == true)
                {
                    frontalReady = false;
                    StartCoroutine(FrontalShot());
                }
            }
        }
        //DEATH TRIGGER
        else if (health <= 0)
        {
            Death();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }

    IEnumerator FrontalShot()
    {
        GameObject ball = Instantiate(cannonBall, frontalCannon.transform.position, this.gameObject.transform.rotation);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce((frontalCannon.transform.up * ballForce) * -1, ForceMode2D.Impulse);
        yield return new WaitForSeconds(frontalCD);
        frontalReady = true;
    }

    public void TakeDamage()
    {
        health -= 1;
        StartCoroutine(ExplodeAnim());
        if (health <= 0)
        {
            GameManager.score += 1;
            fadeOut = true;
            Death();
        }
    }

    IEnumerator ExplodeAnim()
    {
        explode.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        explode.SetActive(false);
    }

    public void Death()
    {
        cannon.SetActive(false);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        health = 0;
        explode.SetActive(true);
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);
        for (float f = 1f; f>= -0.05f; f -= 0.04f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(shooter);
    }
}
