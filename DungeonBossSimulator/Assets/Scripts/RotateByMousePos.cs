using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 public class RotateByMousePos : MonoBehaviour
 {

     private Transform _bullet;
     private Vector2 _screenSize;
     private float _interval = 10;

     private void Start()
     {
         _bullet = transform;
         _screenSize = new Vector2(Screen.width, Screen.height);
     }

     private void LateUpdate()
     {
         // Check for every 10 frames, reduce _interval if you want to check more often.
         if (Time.frameCount % _interval == 0)
         {
             // Use debug to learn mouse position, you can disable this if you want.
             Debug.Log(Input.mousePosition);

             // Mouse on Top Right screen
             _bullet.localEulerAngles = Vector3.zero;

             // Mouse on Bottom Right, else Mouse on Bottom Left else Mouse on Top Left
             if (Input.mousePosition.x > _screenSize.x / 2 && Input.mousePosition.y < _screenSize.y / 2)
                 _bullet.Rotate(Vector3.forward, -90, Space.Self);
             else if (Input.mousePosition.x < _screenSize.x / 2 && Input.mousePosition.y < _screenSize.y / 2)
                 _bullet.Rotate(Vector3.forward, -180, Space.Self);
             else if (Input.mousePosition.x < _screenSize.x / 2 && Input.mousePosition.y > _screenSize.y / 2)
                 _bullet.Rotate(Vector3.forward, 90, Space.Self);
         }
     }
 }
