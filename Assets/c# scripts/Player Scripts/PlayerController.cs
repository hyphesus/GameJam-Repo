using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Oyuncu hareket hızı

    private Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;
    public GameObject sword; // Child obje referansı
    public Transform childTransform; // Child obje transform referansı
   

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sword = transform.GetChild(0).gameObject; // İlk child objeyi alıyoruz
    }

    private void Update()
    {
        // Oyuncunun hareket kontrolü
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        movement = new Vector2(moveX, moveY);

        // Oyuncunun animasyon kontrolü
        if(moveX != 0 || moveY != 0)
        {
            animator.SetFloat("Horizontal", moveX);
            animator.SetFloat("Vertical", moveY);
        }
      
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Hiyerarşik çocuğun dönüşünü ayarla
        // float rotationAngle = Mathf.Atan2(moveY, moveX) * Mathf.Rad2Deg;
        // childTransform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);

        // childTransform.GetComponent<BoxCollider2D>().transform.rotation = Quaternion.Euler(0f, 0f, -rotationAngle);

        // Animasyonlar için bool tespiti
        if (moveX != 0 || moveY != 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    private void FixedUpdate()
    {
        // Oyuncunun fiziksel hareketi
        rb.velocity = movement * moveSpeed;

        // Kamerayı takip etmek için kamera scriptini bulma
        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();

        // Eğer kamera scripti bulunduysa ve scriptin "target" değişkeni boş değilse
        if (cameraFollow != null && cameraFollow.target != null)
        {
            // Kameranın takip ettiği hedefi oyuncu olarak ayarlama
            cameraFollow.target = transform;
        }
    }

    // private void RotateChildCollider()
    // {
    //     // Hiyerarşik çocuğun dönüşünü ayarla
    //     float rotationAngle = Mathf.Atan2(moveY, moveX) * Mathf.Rad2Deg;
    //     childTransform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
    // }
}
