using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    private float timeBtwShots;
    public float startTimeBtwShots;
    public Transform firePoint;
    public GameObject bullet;
    public bool isFacingLeft;
    public bool isFacingDown;
    public bool active;

    //private float cooldownTimer;
    // Start is called before the first frame update
    void Start()
    {
        //isFacingLeft = true;
        //isFacingDown = false;
        active = false;
        timeBtwShots = startTimeBtwShots;
        if(transform.rotation.y == 180 && transform.rotation.z == 0){
            isFacingLeft = true;
        }
        else if(transform.rotation.y == 0 && transform.rotation.z == 0){
            isFacingLeft = false;
        }
        else if(transform.rotation.z == -90){
            isFacingDown = true;
            //firePoint.rotation.z = 90;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(active){
            Shoot();
        }
        //cooldownTimer += Time.deltaTime;
        //if(cooldownTimer >= attackCoolDown){
            //Attack();
        //}
    }
    //private void Attack(){
        //cooldownTimer = 0;
        //bullet.transform.position = firePoint.position;

    //}
    public void Shoot()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(bullet, firePoint.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
