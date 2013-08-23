using System.Linq;

namespace Castles
{
    public class Booobak : Monster
    {
        public bool isUltra {get;set;}
        public Booobak(bool isUltra)
            : base()
        {
            type = EntityType.Booobak;
            speed = (int)MonsterSpeed.Slow;
            this.isUltra = isUltra;

            // specific animation collection for this entity
            aColl.Add(Game.I.resourceManager.GetSprite("BooobakMonster"), 200);

            // add animation to sprite.
            sprite.Animations.Add("walk", aColl);
            sprite.CurrentAnimation = "walk";
            sprite.Animate = true;
        }

        public override void Update()
        {
            if (timeToUpdate)
            {
                // this entity is chasing tough tree
                ToughTree tt = FindToughTree();

                if (tt != null)
                {
                    // ok we found tought tree, chase him...
                    chasingTarget = tt.position;

                    // find path
                    highlightedPlatforms = HighlightPath(position, chasingTarget);

                    if (highlightedPlatforms != null && highlightedPlatforms.Count > 1)
                    {
                        Platform px = highlightedPlatforms[highlightedPlatforms.Count - 2];

                        // px je kterym smerem od mista kde stojim?
                        Direction dir = GetDirection(this.position, px);
                        Move(dir);
                    }
                }
            }
        }

        private ToughTree FindToughTree()
        {
            if ((Game.I.level.Monsters != null) && (Game.I.level.Monsters.Count > 0))
            {
                return (from i in Game.I.level.Monsters
                         where i.type == EntityType.TreeTough
                         select i).FirstOrDefault() as ToughTree;
            }

            return null;
        }

        public override Platform Move(Direction dir)
        {
            // do classic movement
            Platform p = base.Move(dir);

            if (p != null)
            {
                //do i step on monster?
                Monster m = Level.GetMonsterOnPlatform(p.x, p.y, p.layer);

                if (m != null && m is ToughTree)
                {
                    m.Die();

                    //only ultra booobak can remain longer....
                    if (!isUltra)
                        Die();
                }
            }

            return p;
        }
    }
}
