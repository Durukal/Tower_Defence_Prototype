using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private int poolSize = 10;

    private List<GameObject> _pool;
    private GameObject _poolContainter;

    private void Awake() {
        _pool = new List<GameObject>();
        _poolContainter = new GameObject($"Pool - {prefab.name}");
        CreatePooler();
    }

    private void CreatePooler() {
        for (int index = 0; index < poolSize; index++) {
            _pool.Add(CreateInstance());
        }
    }

    private GameObject CreateInstance() {
        GameObject newInstance = Instantiate(prefab, _poolContainter.transform, true);
        newInstance.SetActive(false);
        return newInstance;
    }

    public GameObject GetInstanceFromPool() {
        for (int index = 0; index < _pool.Count; index++) {
            if (!_pool[index].activeInHierarchy) {
                return _pool[index];
            }
        }

        return CreateInstance();
    }

    public static void ReturnToPool(GameObject instance) {
        instance.SetActive(false);
    }

    public static IEnumerator ReturnToPoolWithDelay(GameObject instance, float delay) {
        yield return new WaitForSeconds(delay);
        instance.SetActive(false);
    }
}