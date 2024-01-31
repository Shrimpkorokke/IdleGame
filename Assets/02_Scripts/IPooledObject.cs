
public interface IPooledObject
{
    void OnObjectSpawn(); // 오브젝트가 풀에서 꺼내질 때 호출
    void OnObjectReturn(); // 오브젝트가 풀로 반환될 때 호출
}
