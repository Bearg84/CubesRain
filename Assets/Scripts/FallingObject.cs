using System.Collections;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private bool _colorChanged = false;
    private float _lifeTime;
    private ObjectPool _objectPool;
    private Renderer _renderer;

    public void Initialize(ObjectPool objectPool, float minLifetime, float maxLifetime)
    {
        _objectPool = objectPool;
        _lifeTime = Random.Range(minLifetime, maxLifetime);
    }

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Platform>() != null)
        {
            if (!_colorChanged)
            {
                _renderer.material = new Material(Shader.Find("Standard"));
                _renderer.material.color = new Color(Random.value, Random.value, Random.value);

                StartCoroutine(DestroyAfterTime(_lifeTime));

                _colorChanged = true;
            }
        }
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (_objectPool != null)
        {
            _objectPool.ReturnObject(gameObject);
        }
    }
}