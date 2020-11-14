namespace SpaceGame.Projectile
{
    public static class ProjectileCreator
    {
        public static void CreateProjectile(ProjectileInstantiator instantiator) 
            => instantiator.Instantiate();
    }
}