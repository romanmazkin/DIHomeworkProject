namespace Assets.CourseGame.Develop.CommonServices.LoadingScreen
{
    public interface ILoadingCurrtain
    {
        bool IsShown { get; }

        void Show();

        void Hide();
    }
}
