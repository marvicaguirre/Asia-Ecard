using Wizardsgroup.Repository;

namespace Pfizer.Repository.Context
{
    public interface IExternalSeeder
    {
        //void Seed();
        void Seed(IContext instance);
    }
}
