using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiming : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer,ground;
    [SerializeField] Transform player;
    [SerializeField] float distanceToStop,enemydistance,speed,fireRate,testFloat;
    [SerializeField] int weaponDmg;
    CharacterController controller;
    float angle,fireTime;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Start()
    {
        fireTime = fireRate;
    }
    void aim()
    {

        angle = Vector3.SignedAngle(player.position - transform.position, Vector3.forward, Vector3.up);
        transform.rotation = Quaternion.Euler(0, -angle, 0);

    }
    void shoot()
    {
        fireTime = fireRate;
        
        Ray ray;
        Vector3 direction = player.position - transform.position;
        ray = new Ray(transform.position, direction);
        
        StartCoroutine(attack(ray));
    }

    IEnumerator attack(Ray ray)
    {
        RaycastHit hit;
        yield return new WaitForSeconds(0.1f);
        if (!Physics.Raycast(ray, enemydistance, ground))
        {
            if (Physics.Raycast(ray, out hit, enemydistance, playerLayer))
            {
                hit.collider.transform.GetComponent<playerHp>().hp -= weaponDmg;
            }
        }

    }
    void followWithObstacles()
    {
        aim();
        Vector3 d = Physics.Raycast(transform.position, transform.right, enemydistance, ground) ? transform.right * speed * Time.deltaTime * -1f : transform.right * speed * Time.deltaTime;
        Vector3 plus = !Physics.Raycast(transform.position, transform.forward, 7f, ground) ? d + transform.forward * speed * Time.deltaTime : d ;
        controller.Move(plus);
    }
    void followplayer()
    {
        Ray ray;
        Vector3 direction = player.position - transform.position;
        ray = new Ray(transform.position, transform.forward.normalized);

        if (Vector3.Distance(player.position, transform.position) > distanceToStop)
        {
            if (!Physics.Raycast(ray, enemydistance, ground))
            {
                aim();
                transform.forward = player.position - transform.position;
                //controller.Move((player.position - transform.position).normalized * speed * Time.deltaTime);
                controller.Move(transform.forward * speed * Time.deltaTime);
            }
            else
                followWithObstacles();
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, testFloat, playerLayer))
            {
                if (hit.collider.gameObject.layer != ground)
                {
                    aim();

                    if (fireTime >= 0f)
                        fireTime -= 0.1f;

                    if (fireTime <= 0f)
                        shoot();
                }
                else
                    followWithObstacles();
                
            }
            else
                followWithObstacles();
            


        }
    }

    void Update()
    {

        if (Vector3.Distance(player.position, transform.position) < enemydistance)
            followplayer();
    }
}
