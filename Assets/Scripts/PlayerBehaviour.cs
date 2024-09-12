using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    
    [SerializeField] private GameObject[] _pointPrefab;
    [SerializeField] private Vector3[] _vectors;
    [SerializeField] private GameObject point; 
    [Header("Movement"), Range(3,20)]
    [SerializeField] float _speed =5f;
    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _pointPrefab = new GameObject[_vectors.Length];
        for (int i =0; i<_vectors.Length;i++)
        {
            _pointPrefab[i] = Instantiate(point, _vectors[i], Quaternion.identity);
            _pointPrefab[i].name = $"Clone of point {i}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

    }

    private void PlayerMove()
    {
        float forward = Input.GetAxis("Horizontal");
        float back = Input.GetAxis("Vertical");
        Vector3 playerInput = new Vector3(forward, 0f, back);
        _rigidbody.velocity = playerInput * _speed;

        for (int index=0; index<_pointPrefab.Length;index++)
        {
            float dotProduct = Vector3.Dot(transform.forward, Vector3.Normalize(_pointPrefab[index].transform.position - transform.position));
            Debug.DrawLine(transform.position, _pointPrefab[index].transform.position, (dotProduct < 0.20) ? Color.white : Color.black);
        }
    }
}
