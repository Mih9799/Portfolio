using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public float lifetime;
    private float startlife;
    public float drag;
    public Vector3 scale;
    public bool loaded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (loaded == true)
        {
            if (startlife == 0)
            {
                startlife = lifetime;
            }
            if (lifetime > 0)
            {
                Rigidbody2D physics = gameObject.GetComponent<Rigidbody2D>();
                physics.velocity = (physics.velocity * (1 - drag * Time.deltaTime));
                physics.angularVelocity = (physics.angularVelocity * (1 - drag * Time.deltaTime));
                lifetime -= Time.deltaTime;
                gameObject.transform.localScale = scale * (lifetime / startlife);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
