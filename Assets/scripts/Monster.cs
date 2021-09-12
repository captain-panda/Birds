using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField] Sprite _deadSprite;
    private bool _hasDied;
    [SerializeField] ParticleSystem _particleSystem;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (ShouldDieFromCollision(other))
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        _hasDied = true;
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    private bool ShouldDieFromCollision(Collision2D collision)
    {
        if (_hasDied)
            return false;

        //Check if collision was with a bird
        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird!=null)
            return true;

        //Contacts is an array of places of contact during collision
        //Normal is a Vector2
        if (collision.contacts[0].normal.y < -0.5)
            return true;

        return false;
    }
}
