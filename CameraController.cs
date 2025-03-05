using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderPercentage = 1.2f; // Процент экрана, определяющий "край"
    public Vector2 panLimit;

    private float screenWidth;

    void Start()
    {
        screenWidth = Screen.width;
    }

    void Update()
    {
        Vector3 pos = transform.position;

#if UNITY_ANDROID

        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0); // Обрабатываем только первое касание

            if (touch.position.x < screenWidth * panBorderPercentage)
            {
                pos.x -= panSpeed * Time.deltaTime;  // Движение влево
            }
            if (touch.position.x > screenWidth * (1 - panBorderPercentage))
            {
                pos.x += panSpeed * Time.deltaTime;  // Движение вправо
            }
        }

#else // Для редактора 

        if (Input.mousePosition.x < screenWidth * panBorderPercentage)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x > screenWidth * (1 - panBorderPercentage))
        {
            pos.x += panSpeed * Time.deltaTime;
        }
#endif

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        transform.position = pos;
    }
}
