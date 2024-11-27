namespace Assets.CourseGame.Develop.CommonServices.DataManagement
{
    public interface IDataSerializer
    {
        string Serialize<TData>(TData data);
        TData Deserialize<TData>(string serializedData);
    }
}
