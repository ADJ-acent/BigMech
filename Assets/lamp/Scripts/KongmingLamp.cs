using UnityEngine;
using UnityEngine.Rendering;

public class KongmingLamp : MonoBehaviour
{
    private Transform trans;
    private Vector3 speed;
    private bool moveUp;
    private int count;

    void Start()
    {
        moveUp = false;
        trans = transform;
        // RANDOM VELOCITY
        speed = new Vector3(Random.Range(-0.5f, 0.5f), 0.5f, Random.Range(-0.5f, 0.5f));

        count = 0;
    }


    void Update()
    {
        if (moveUp == false)
        {
            //GOING DOWN
            trans.position -= speed * Time.deltaTime;
            count++;
            if(count == 400)
            {
                moveUp = true;
                count = 0;
            }  
   
          
        }
        else
        {
            // GOING UP
            trans.position += speed * Time.deltaTime ;
            count++;
            if (count == 400)
            {
                moveUp = false;
                count = 0;
            }
        }
        

      

    }
}
