using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//해당 스크립트로 Resources폴더에 접근하여 데이터를 불러옵니다.
public class ResourceManager
{
    //Resources폴더의 path경로의 파일을 로드합니다.
    public T Load<T>(string path) where T : Object
    {
        if(typeof(T) == typeof(GameObject))
        {
            string name = path;
            int idx = name.LastIndexOf('/'); //마지막 / 검색
            if (idx >= 0)
                name = name.Substring(idx + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;

            //풀에 이미 존재하면 해당 오브젝트 반환
        }

        return Resources.Load<T>(path);
    }

    //path경로의 정보로 Object를 생성합니다.
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if(original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;


        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;

        return go;
    }

    public void Destroy(GameObject go)
    {
        if(go == null)
        {
            return;
        }

        Poolable poolable = go.GetComponent<Poolable>();
        if(poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(go);
    }
}
