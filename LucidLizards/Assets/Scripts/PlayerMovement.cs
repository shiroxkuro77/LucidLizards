
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
  private Rigidbody2D body;
  [SerializeField] private float speed;
  [SerializeField] private float jumpSpeed;
  private Animator anim;
  private bool grounded;

  private void Awake()
  {
    //Grab references for rigidbody and animator from obj
      body = GetComponent<Rigidbody2D>();
      anim  = GetComponent<Animator>();
  }

  private void Update()
  {
      float horizontalInput = Input.GetAxis("Horizontal");
      body.velocity = new Vector2(horizontalInput * speed,body.velocity.y);



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
            Die();
        }

    if (collision.gameObject.CompareTag("Goal"))
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

  }

   private void Die() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }


}
