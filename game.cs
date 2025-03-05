using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody2D rb;
    private float originalZ;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 1; // Включить гравитацию по умолчанию
        rb.isKinematic = false; // Убрать isKinematic по умолчанию

        originalZ = transform.position.z; // Сохраняем первоначальное положение Z, чтобы оно не менялось при перетаскивании
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Отключаем гравитацию и делаем kinematic во время перетаскивания
        rb.gravityScale = 0;
        rb.isKinematic = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
        // Включаем гравитацию после отпускания
        rb.gravityScale = 1;
        rb.isKinematic = false;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, originalZ); 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent != null && other.transform.parent.name == "Shelves")
        {
            // Останавливаем движение, если столкнулись с полкой
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            rb.isKinematic = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.parent != null && other.transform.parent.name == "Shelves")
        {
            // Включаем гравитацию, если покинули полку
            rb.gravityScale = 1;
            rb.isKinematic = false;
        }
    }
}
