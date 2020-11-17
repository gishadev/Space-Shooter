namespace SpaceGame.PowerUp
{
    public class LevelUp : PowerUp
    {
        public override void OnInteract() => Influencer.LevelUp();
    }
}