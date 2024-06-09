
public class EmptySingleton<T> where T : class, new()
{
    private static readonly T instance = null;
    static EmptySingleton()    //构造函数
    {
        instance = new T();
    }

    public static T Instance
    {
        get => instance;
    }
}