using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject cannonBall, frontalCannon, explode;
    [SerializeField] private GameObject[] sideCannons;
    [SerializeField] private float frontalCDTime, sideCDTime, ballForce;
    private float frontalCD, sideCD;
    public static float health = 3;
    public static bool isDead = false;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //PLAY AGAIN
        if (MenuManager.playAgain == true)
        {
            explode.SetActive(false);
        }

        //HEALTH UI
        anim.SetInteger("Health", (int)health);

        //ATTACK CD
        if(frontalCD > 0)
        {
            frontalCD -= Time.deltaTime;
        }
        if (sideCD > 0)
        {
            sideCD -= Time.deltaTime;
        }

        if (frontalCD <= 0)
        {
            frontalCD = 0;
        }
        if (sideCD <= 0)
        {
            sideCD = 0;
        }

        //ATTACK INPUT
        if(health > 0 && GameManager.survived == false)
        {
            if(Input.GetButtonDown("Fire1") && frontalCD == 0)
            {
                FrontalShot();
            }
            else if (Input.GetButtonDown("Fire2") && sideCD == 0)
            {
                SideShot();
            }
        }
    }

    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        health = 0;
        explode.SetActive(true);
        yield return new WaitForSeconds(2);
        isDead = true;
    }

    public void FrontalShot()
    {
        GameObject ball = Instantiate(cannonBall, frontalCannon.transform.position, this.gameObject.transform.rotation);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce((frontalCannon.transform.up * ballForce) * -1, ForceMode2D.Impulse);
        frontalCD = frontalCDTime;
    }

    public void SideShot()
    {
        GameObject ball0 = Instantiate(cannonBall, sideCannons[0].transform.position, this.gameObject.transform.rotation);
        Rigidbody2D rb0 = ball0.GetComponent<Rigidbody2D>();
        rb0.AddForce((sideCannons[0].transform.up * ballForce) * -1, ForceMode2D.Impulse);

        GameObject ball1 = Instantiate(cannonBall, sideCannons[1].transform.position, this.gameObject.transform.rotation);
        Rigidbody2D rb1 = ball1.GetComponent<Rigidbody2D>();
        rb1.AddForce((sideCannons[1].transform.up * ballForce) * -1, ForceMode2D.Impulse);

        GameObject ball2 = Instantiate(cannonBall, sideCannons[2].transform.position, this.gameObject.transform.rotation);
        Rigidbody2D rb2 = ball2.GetComponent<Rigidbody2D>();
        rb2.AddForce((sideCannons[2].transform.up * ballForce) * -1, ForceMode2D.Impulse);

        GameObject ball3 = Instantiate(cannonBall, sideCannons[3].transform.position, this.gameObject.transform.rotation);
        Rigidbody2D rb3 = ball3.GetComponent<Rigidbody2D>();
        rb3.AddForce((sideCannons[3].transform.up * ballForce), ForceMode2D.Impulse);

        GameObject ball4 = Instantiate(cannonBall, sideCannons[4].transform.position, this.gameObject.transform.rotation);
        Rigidbody2D rb4 = ball4.GetComponent<Rigidbody2D>();
        rb4.AddForce((sideCannons[4].transform.up * ballForce), ForceMode2D.Impulse);

        GameObject ball5 = Instantiate(cannonBall, sideCannons[5].transform.position, this.gameObject.transform.rotation);
        Rigidbody2D rb5 = ball5.GetComponent<Rigidbody2D>();
        rb5.AddForce((sideCannons[5].transform.up * ballForce), ForceMode2D.Impulse);

        sideCD = sideCDTime;
    }
}
