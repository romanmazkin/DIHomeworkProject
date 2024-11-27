namespace Assets.CourseGame.Develop.CommonServices.DataManagement
{
    public interface IDataWriter<TData> where TData : ISaveData
    {
        void WriteTo(TData data);
    }
}