namespace Wizardsgroup.Repository
{
    public enum DatabaseInitializerMode
    {
        DropThenCreate = 0,
        Reseed = 1,
        DoNotChange = 2,
        Migration = 3,
    }
}
