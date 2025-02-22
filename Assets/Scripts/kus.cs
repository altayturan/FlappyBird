using UnityEngine;
using UnityEngine.SceneManagement;


public class kus : MonoBehaviour
{
    Rigidbody2D rigi;

    public float ziplama_gucu;
    
    public float egilme_hizi;


    void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(0);
    }
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){

            rigi.velocity = Vector2.zero;

            rigi.velocity = Vector2.up * ziplama_gucu * Time.deltaTime;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 60.0f);
            
        } else {

            transform.eulerAngles -= new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, egilme_hizi);
        }
    }
}
