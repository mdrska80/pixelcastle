namespace Castles
{
    public class ToughTree : Monster
    {
        public ToughTree() : base()
        {
            type = EntityType.TreeTough;
            speed = (int)MonsterSpeed.Slow;

            // specific animation collection for this entity
            aColl.Add(Game.I.resourceManager.GetSprite("TreeTough"), 200);

            // add animation to sprite.
            sprite.Animations.Add("walk", aColl);
            sprite.CurrentAnimation = "walk";
            sprite.Animate = true;

        }
    }
}
