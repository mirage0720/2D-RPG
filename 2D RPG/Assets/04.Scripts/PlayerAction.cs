using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float Speed;
    public GameManager manager;
    Rigidbody2D rigid;
    Animator anim;
    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirvec;
    GameObject scanObject;

    

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    
    void Update()
    {
        //move value
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        //check button Up&Down
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

        //check Horizontal move
        if(hDown)
            isHorizonMove = true;
        else if(vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;

        //Animation
        if (anim.GetInteger("hAxisRaw") !=h ){
            anim.SetBool("isChange", true );
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v){
            anim.SetBool("isChange", true );
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChange", false );

        //Direction
        if(vDown && v == 1){
            dirvec = Vector3.up;
        } else if(vDown && v == -1){
            dirvec = Vector3.down;
        } else if(hDown && h == -1){
            dirvec = Vector3.left;
        } else if(hDown && h == 1){
            dirvec = Vector3.right;
        }

        //Scan Object
        if(Input.GetButtonDown("Jump") && scanObject != null){
            manager.Action(scanObject);
        }
        
        
    }

    void FixedUpdate()
    {
        //Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h,0) : new Vector2(0,v);
        rigid.velocity = moveVec * Speed;

        //Ray
        Debug.DrawRay(rigid.position, dirvec * 0.7f, new Color(0,1,0));
        RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, dirvec, 0.7f, LayerMask.GetMask("Object"));

        if(rayhit.collider != null){
            scanObject = rayhit.collider.gameObject;
        } else
            scanObject = null;

    }
}
