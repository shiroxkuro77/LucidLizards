
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
  private Rigidbody2D body;
  [SerializeField] private float speed;
  [SerializeField] private float jumpSpeed;
  private Animator anim;
  private bool grounded;
  public Transform checkpoint;
  public GameObject gbCheckpoint;
    public GameObject blood;

    private void Awake()
  {
    //Grab references for rigidbody and animator from obj
      body = GetComponent<Rigidbody2D>();
      anim  = GetComponent<Animator>();
  }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);



        //Flip player when moving right/left
        if (horizontalInput > .01f)
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        else if (horizontalInput < -.01f)
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            jump();

        }


        //set anim paramaters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);

        //Set new checkpoint
        if (Input.GetKeyDown("z") && grounded)
        {
            gbCheckpoint.transform.position = new Vector3(transform.position.x, transform.position.y, gbCheckpoint.transform.position.z);
        }

        if (Input.GetKeyDown("x")) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

  private void jump()
  {
      body.velocity  = new Vector2(body.velocity.x, jumpSpeed);
      anim.SetTrigger("jump");
      grounded = false;
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Ground")
        grounded = true;

    if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Pit")) {
            StartCoroutine(Die());
        }

    if (collision.gameObject.CompareTag("Goal"))
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

  }

   private IEnumerator Die() {

        Vector3 currPostion = transform.position;
        //Destroy(gameObject, 2f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Instantiate(blood, currPostion, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        transform.position = new Vector3(gbCheckpoint.transform.position.x, gbCheckpoint.transform.position.y, transform.position.z);
        body.velocity = new Vector2(body.velocity.x, jumpSpeed/2);

    }


}
