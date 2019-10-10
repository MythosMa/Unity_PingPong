using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public float speed;
    public float xMax;
    public float xMin;

	
	public AudioSource itemAudio ;

    public GameObject ballPrefab;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPassedLevel)
        {
            return;
        }
        //获取横向的移动位[-1, 1]
        float xx = Input.GetAxisRaw("Horizontal");
        if (xx != 0)
        {
            Vector3 pos = transform.position;
            pos.x += xx * Time.deltaTime * speed;

            //限定取值范围
            pos.x = Mathf.Clamp(pos.x, xMin, xMax);
            transform.position = pos;
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        Item item = other.gameObject.GetComponent<Item>();
        if (item != null)
        {
            if (GameManager.instance.isPlaying)
            {
                if (item.currentType == Item.Item_Type.Life)
                {
                    GameManager.instance.ChangeLife(1);
                }
                else if (item.currentType == Item.Item_Type.Many)
                {
                    for (int i = 0; i < item.manyNum; i++)
                    {
                        GameObject ball = Instantiate(ballPrefab);
                        Vector3 pos = transform.position;
                        pos.y += 0.5f;
                        ball.transform.position = pos;
                        ball.GetComponent<Ball>().StartMove();
                    }
                }
            }
			itemAudio.Play() ;
            Destroy(other.gameObject);
        }

    }
}
