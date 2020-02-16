using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Rigidbody2D))]
public class Movimenti : MonoBehaviour
{

    Rigidbody2D body;
    Animator anim;

    //Variabili Migliorabili
    [SerializeField]
    float speed = 10;
    [SerializeField]
    float jump = 40f;

    bool staSaltando = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movimenti();
        salto();
    }

    void movimenti()
    {
        float movimentoOrizzontale = Input.GetAxis("Horizontal");
        Vector2 direzione = new Vector2(0, 0);

        if(movimentoOrizzontale != 0)
        {
            direzione.x = movimentoOrizzontale; 
        }
        transform.Translate(direzione * (speed * Time.deltaTime));
    
        if(direzione.x >= 0)
        {
            body.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            body.transform.localScale = new Vector3(-1, 1, 1);
        }

        anim.SetFloat("siMuove", Mathf.Abs(movimentoOrizzontale));
    }

    void salto()
    {
        float saltoVerticale = Input.GetAxis("Jump");
        if (staSaltando)
        {
            anim.SetFloat("siMuove", Mathf.Abs(0));
            if (body.velocity.y == 0)
            {
                anim.SetBool("jumping", false);
                staSaltando = false;
            }
        }
        else
        {
            if(saltoVerticale > 0)
            {
                anim.SetBool("jumping", true);
                body.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
                staSaltando = true;
            }
        }
    }
}
