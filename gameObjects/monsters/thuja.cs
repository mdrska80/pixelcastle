namespace Castles
{
    public class Thuja : Monster
    {
        public Thuja() : base()
        {
            type = EntityType.Thuja;
            speed = (int)MonsterSpeed.SuperFast;

            // specific animation collection for this entity
            aColl.Add(Game.I.resourceManager.GetSprite("Thuja"), 200);

            // add animation to sprite.
            sprite.Animations.Add("walk", aColl);
            sprite.CurrentAnimation = "walk";
            sprite.Animate = true;

        }
    }
}
