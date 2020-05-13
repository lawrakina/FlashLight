﻿using System.Collections.Generic;
using System.Linq;
using FpsUnity.Model;
using UnityEngine;


namespace FpsUnity.Helper
{
    public class PoolManager
    {
        private static Dictionary<string, LinkedList<BaseObjectScene>> _poolsDictionary;
        private static Transform _deactivatedObjectsParent;

        public static void Init(Transform pooledObjectsContainer)
        {
            Debug.Log($"PoolManager.Init; _poolsDictionary Create new Dictionary<string, LinkedList<BaseObjectScene>>()");
            _deactivatedObjectsParent = pooledObjectsContainer;
            _poolsDictionary = new Dictionary<string, LinkedList<BaseObjectScene>>();
        }

        public static BaseObjectScene GetFromPool(BaseObjectScene prefab)
        {
            //Debug.Log($"_poolsDictionary: {_poolsDictionary.Count}");
            if (!_poolsDictionary.ContainsKey(prefab.name))
            {
                Debug.Log($"Create new LinkedList<BaseObjectScene>");
                _poolsDictionary[prefab.name] = new LinkedList<BaseObjectScene>();
            }

            BaseObjectScene result;

            if (_poolsDictionary[prefab.name].Count > 0)
            {
                Debug.Log($"_poolsDictionary[{prefab.name}].Count = {_poolsDictionary.Count}");
                result = _poolsDictionary[prefab.name].First.Value;
                _poolsDictionary[prefab.name].RemoveFirst();
                result.SetDefault();
                result.SetActive(true);
                //result.SetActivateChildren(result.gameObject, true);
                Debug.Log($"return {result.name}");
                return result;
            }

            Debug.Log($"Create new BaseObjectScene.Instantiate(prefab): {prefab.name}");
            result = BaseObjectScene.Instantiate(prefab);
            result.name = prefab.name;

            return result;
        }

        public static void PutToPool(BaseObjectScene target)
        {
            _poolsDictionary[target.name].AddFirst(target);
            target.transform.parent = _deactivatedObjectsParent;
            target.SetActive(false);
            //target.SetActivateChildren(target.gameObject, false);
            Debug.Log($"PoolManager.PutToPool; pool.Count: {_poolsDictionary[target.name].Count}");
        }
    } 
}

