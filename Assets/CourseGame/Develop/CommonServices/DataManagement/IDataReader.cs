namespace Assets.CourseGame.Develop.CommonServices.DataManagement
{
    public interface IDataReader<TData> where TData : ISaveData
    {
        void ReadFrom(TData data);
    }
}
