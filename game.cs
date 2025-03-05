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
        rb.gravityScale = 1; // �������� ���������� �� ���������
        rb.isKinematic = false; // ������ isKinematic �� ���������

        originalZ = transform.position.z; // ��������� �������������� ��������� Z, ����� ��� �� �������� ��� ��������������
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // ��������� ���������� � ������ kinematic �� ����� ��������������
        rb.gravityScale = 0;
        rb.isKinematic = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
        // �������� ���������� ����� ����������
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
            // ������������� ��������, ���� ����������� � ������
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            rb.isKinematic = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.parent != null && other.transform.parent.name == "Shelves")
        {
            // �������� ����������, ���� �������� �����
            rb.gravityScale = 1;
            rb.isKinematic = false;
        }
    }
}
