namespace Castles
{
    /// <summary>
    /// Dead tree will plant new gems on floor. It is very slow monster.
    /// </summary>
    public class DeadTree : Monster
    {
        public DeadTree() : base()
        {
            type = EntityType.TreeDead;
            speed = (int)MonsterSpeed.VerySlow;

            // specific animation collection for this entity
            aColl.Add(Game.I.resourceManager.GetSprite("TreeDead"), 200);

            // add animation to sprite.
            sprite.Animations.Add("walk", aColl);
            sprite.CurrentAnimation = "walk";
            sprite.Animate = true;
        }

        /// <summary>
        /// provede aktualni presun z jednoho policka na druhe,
        /// provede kontroly ze se presunout muze
        /// provede sber drahokamu apod...
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public override Platform Move(Direction dir)
        {
            Platform p = CanMove(dir);

            if (p != null)
            {
                position.X = p.x;
                position.Y = p.y;
                position.Layer = p.layer;
                facing = dir;

                // put gem on flooor
                if (p.item == null)
                {
                    p.item = new Gem();
                    Game.I.level.activeGems++;
                }

            }

            return p;
        }
    }
}
