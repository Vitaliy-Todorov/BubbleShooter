using Data;

namespace Assets.Scripts.Logic
{
    public class DependencyInjection
    {
        private static DependencyInjection _instance;

        public static DependencyInjection Container =>
            _instance ?? (_instance = new DependencyInjection());

        public void RegisterDependency<TDependency>(TDependency dependency) =>
            Implementation<TDependency>.Instance = dependency;

        public TDependency GetDependency<TDependency>() =>
            Implementation<TDependency>.Instance;

        private static class Implementation<TDependency>
        {
            public static TDependency Instance;
        }
    }
}