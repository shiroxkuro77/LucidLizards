
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  private Rigidbody2D body;
  [SerializeField] private float speed;
  [SerializeField] private float jumpSpeed;
  private void Awake()
  {
      body = GetComponent<Rigidbody2D>();
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
     

        if (Input.GetKey(KeyCode.Space))
      {
          body.velocity = new Vector2(body.velocity.x, jumpSpeed);
      }

  }

}
