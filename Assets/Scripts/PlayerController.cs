using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public Transform HandWithWeapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"),0);
        transform.position += playerInput.normalized * speed * Time.deltaTime;

        //Поворот оружия
        Vector3 displacement = HandWithWeapon.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x)*Mathf.Rad2Deg+180;

        HandWithWeapon.rotation = Quaternion.Euler(0f,0f,angle);
    }
}
