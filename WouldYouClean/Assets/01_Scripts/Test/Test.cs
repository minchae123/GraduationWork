using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    SpriteRenderer sp;

	private void Awake()
	{
		sp = GetComponent<SpriteRenderer>();
	}

	void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if(x <= 0)
            sp.flipX = true;
        else
            sp.flipX = false;

        transform.position += new Vector3(x,y).normalized * 10 * Time.deltaTime;
    }
}
