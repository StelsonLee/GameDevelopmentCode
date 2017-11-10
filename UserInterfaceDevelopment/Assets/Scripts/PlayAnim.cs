using UnityEngine;
using System.Collections;

public class PlayAnim : MonoBehaviour {

    public Animation anim;
    public AnimationClip anim1;
    public AnimationClip anim2;

    public bool _front = true;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
    }
	
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            anim.Play(anim.clip.name);
            if (_front)
            {
                anim.clip = anim2;
            }
            else
            {
                anim.clip = anim1;
            }
            _front = !_front;
        }
    }
}
