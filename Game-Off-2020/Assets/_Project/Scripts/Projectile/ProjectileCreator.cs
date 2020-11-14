namespace SpaceGame.Projectile
{
    public static class ProjectileCreator
    {
        public static void CreateProjectile(ProjectileInstantiator instantiater)
        {
            instantiater.Instantiate();
        }
    }
}