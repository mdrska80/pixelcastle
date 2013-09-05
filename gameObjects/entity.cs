using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Castles.Conf;

namespace Castles
{
    public partial class Entity
    {
        // pathfinding part
        private int MAX = 221;
        public string id = Guid.NewGuid().ToString();
        protected List<Platform> highlightedPlatforms = new List<Platform>();

        // entity properties part
        public IGPos chasingTarget { get; set; }
        public Direction facing {get;set;}
        public EntityType type { get; set; }
        public int lives { get; set; }
        public IGPos position { get; set; }

        public IGPos positionOriginal { get; set; }

        // helpers
        private Random r {get;set;}
        public int speed { get; set; }
        public int sspeeed { get; set; }

        /// <summary>
        /// What kind of skills player have.
        /// </summary>
        public List<ISkill> Skills { get; set; }

        /// <summary>
        /// Experience gained by gaming
        /// </summary>
        public Experience Experience { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public Entity()
        {
            r = new Random();
            lives = 1;
            speed = 5;

            chasingTarget = new IGPos(0,0,0);

            Game.I.eventManager.OnTurnEnd += eventManager_OnTurnEnd;

            Skills = new List<ISkill>();

            InitGfx();
            AssignSkills();
        }

        public virtual void AssignSkills()
        {
        }

        /// <summary>
        /// Metoda provede kontrolu zda hrac ma skill pod danym kodem.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ISkill CheckSkill(string code)
        {
            var y = (from x in Skills
                     where x.Code == code
                     select x).FirstOrDefault();

            return y;
        }

        void eventManager_OnTurnEnd()
        {
            Update();
        }

        public bool isAlive
        {
            get { return lives > 0; }
        }

        #region update

        /// <summary>
        /// Gets a value indicating whether this <see cref="Castles.Entity"/> time to update.
        /// </summary>
        /// <value><c>true</c> if time to update; otherwise, <c>false</c>.</value>
        public bool timeToUpdate
        {
            get
            {
                sspeeed++;
                bool toReturn = (sspeeed >= speed);

                if (toReturn)
                    sspeeed = 0;

                return toReturn;
            }
        }
        #endregion


        public virtual void Update()
        {
            //ClearHighlightedPath(highlightedPlatforms);
            highlightedPlatforms = HighlightPath(position, chasingTarget);
        }

        public virtual Platform CanMove(Direction dir)
        {
            if (CheckSkill("SKILL_MOVE")!=null)
            {
                return Level.CheckPlatform(Common.GetDesiredPosition(dir, position), position);
            }

            return null;
        }

        public virtual Platform CanMove(Direction dir, IGPos pos)
        {
            if (CheckSkill("SKILL_MOVE") != null)
            {
                return Level.CheckPlatform(Common.GetDesiredPosition(dir, pos), pos);
            }

            return null;
        }

        public Platform CanMove(Direction dir, Platform p)
        {
            if (CheckSkill("SKILL_MOVE") != null)
            {
                IGPos pos = new IGPos(p.x, p.y, p.layer);
                return Level.CheckPlatform(Common.GetDesiredPosition(dir, pos), pos);
            }

            return null;
        }

        /// <summary>
        /// provede aktualni presun z jednoho policka na druhe,
        /// provede kontroly ze se presunout muze
        /// provede sber drahokamu apod...
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public virtual Platform Move(Direction dir)
        {
            Platform p = CanMove(dir);

            if (p!=null)
            {
                position.X = p.x;
                position.Y = p.y;
                position.Layer = p.layer + (p.elevator != null ? p.elevator.Current : 0);
                facing = dir;

                if (p.action != null)
                    p.action.Execute(p, this);

                    Pickup(p);
            }

            return p;
        }

        public static Entity CreateEntity(EntityDef md)
        {
            //tohle je tu nejake divne....
            if (md.ET == EntityType.Player)
                md.Class = "Castles.Player";
            
            string cls = CastlesConfigurationReader.GetConfiguration().GetEntityByType(md.ET).Class;
            try
            {
                Type t = Assembly.GetExecutingAssembly().GetType(cls);

                Object oh = Activator.CreateInstance(t);
                Entity m = oh as Entity;

                if (m != null)
                {
                    m.position = new IGPos(md.X, md.Y, md.Layer);
                    m.positionOriginal = new IGPos(md.X, md.Y, md.Layer);

                    return m;

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Cannot create entity. Something went wrong:", ex);
            }

            return null;
        }

        /// <summary>
        /// Any living entity can die. This method will do that
        /// </summary>
        public virtual void Die()
        {
            lives--;
        }

        public virtual bool Pickup(Platform p)
        {
            if (p.item is Gem)
            {
                Gem g = p.item as Gem;

                if (!g.picked)
                {
                    Game.I.level.activeGems--;
                    bool pxxx = p.item.TryToPickup();
                }

                // last gem?
                return Game.I.level.activeGems==0;
            }

            return false;
        }

        public Direction GetDirection(IGPos iGPos, Platform px)
        {
            if (iGPos.X+1 == px.x && iGPos.Y-1 == px.y) return Direction.UP;
            if (iGPos.X-1 == px.x && iGPos.Y+1 == px.y) return Direction.DOWN;
            if (iGPos.X-1 == px.x && iGPos.Y == px.y) return Direction.LEFT;
            if (iGPos.X+1 == px.x && iGPos.Y == px.y) return Direction.RIGHT;

            return Direction.UP;
        }


        #region Pathfinding

        /// <summary>
        /// Clears the highlighted path.
        /// </summary>
        /// <param name="pth">Pth.</param>
        public void ClearHighlightedPath(List<Platform> pth)
        {
            if (pth != null)
            {
                foreach (var platform in pth)
                {
                    if (platform!=null)
                    platform.isHighLighted = false;
                }
            }
        }

        /// <summary>
        /// Highlights the path.
        /// </summary>
        /// <returns>The path.</returns>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public List<Platform> HighlightPath(IGPos from, IGPos to)
        {
            ClearHighlightedPath(highlightedPlatforms);

            List<Platform> pth = FindPath(from, to);

            if (pth != null)
            {
                foreach (var platform in pth)
                {
                    if (platform!=null)
                        platform.isHighLighted = true;
                }
            }
            else
            {
                Console.WriteLine("Impossible to highlight path for: {0} at position {1}", type, position);
            }

            return pth;

        }

        /// <summary>
        /// Finds the path.
        /// </summary>
        /// <returns>The path.</returns>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public List<Platform> FindPath(IGPos from, IGPos to)
        {
            // 
            // http://www.devbook.cz/algoritmus-sireni-do-sirky-vlna-hledani-cesty-v-bludisti

            Dictionary<int, List<Platform>> qTodo = new Dictionary<int, List<Platform>>();


            List<Platform> lp = new List<Platform>();
            Platform ppp = Game.I.level.GetPlatform(from);
            if (ppp == null)
                return null;

            lp.Add(ppp);
            qTodo.Add(1, lp);

            //List<  t<int, List<Platform>> qTodo = new 

            //Queue<Tuple<int, Platform>> qTodo = new Queue<Tuple<int, Platform>>();
            //qTodo.Enqueue(new Tuple<int, Platform>(0,Game.I.level.GetPlatform(from)));
            Queue<Tuple<int, Platform>> qProcessed = new Queue<Tuple<int, Platform>>();
            int depth = 0;
            int maxValue = CheckSurroundings(qTodo, qProcessed, to, ++depth);

            List<Tuple<int, Platform>> processedList = qProcessed.ToList();
            List<Platform> lPlatformsOnPath = new List<Platform>();

            lPlatformsOnPath.Add(Game.I.level.GetPlatform(to));

            // ...dokud není kolem políčka, které jsme sebrali z fronty, políčko F. 
            // Teď je krásně vidět sestupně očíslovaná cesta od políčka F k políčku S. 
            // Nyní stačí jen projít se od F k S technikou: jestli je nahoře nebo vlevo nebo dole nebo 
            // vpravo číslo o jednu menší, než to, na kterém stojím. Čísla si ukládám obráceně do fronty, 
            // abych měl souřadnici políčka F na konci. 
            //
            // Tak a je to, ve frontě máme zapsanou cestu bod po bodu, jak se dostat od bodu S do bodu F.
            int processingValue = maxValue+1;
            while (true)
            {
                processingValue--;
                Platform previousPlatform = lPlatformsOnPath[lPlatformsOnPath.Count - 1];

                if (previousPlatform != null)
                {
                    var xUp = (from i in processedList
                               where i.Item1 == processingValue &&
                                     i.Item2.x == previousPlatform.x + 1 &&
                                     i.Item2.y == previousPlatform.y - 1
                               select i
                              ).FirstOrDefault();



                    var xDown = (from i in processedList
                                 where i.Item1 == processingValue &&
                                       i.Item2.x == previousPlatform.x - 1 &&
                                       i.Item2.y == previousPlatform.y + 1
                                 select i
                                ).FirstOrDefault();

                    var xLeft = (from i in processedList
                                 where i.Item1 == processingValue &&
                                       i.Item2.x == previousPlatform.x - 1 &&
                                       i.Item2.y == previousPlatform.y
                                 select i
                                ).FirstOrDefault();

                    var xRight = (from i in processedList
                                  where i.Item1 == processingValue &&
                                        i.Item2.x == previousPlatform.x + 1 &&
                                        i.Item2.y == previousPlatform.y
                                  select i
                                 ).FirstOrDefault();

                    if (xUp != null) lPlatformsOnPath.Add(xUp.Item2);
                    else if (xDown != null) lPlatformsOnPath.Add(xDown.Item2);
                    else if (xLeft != null) lPlatformsOnPath.Add(xLeft.Item2);
                    else if (xRight != null) lPlatformsOnPath.Add(xRight.Item2);
                }

                if (processingValue == 0) break;
            }

            return lPlatformsOnPath;
        }

        /// <summary>
        /// Checks the surroundings.
        /// </summary>
        /// <returns>The surroundings.</returns>
        /// <param name="qTodo">Q todo.</param>
        /// <param name="qProcessed">Q processed.</param>
        /// <param name="to">To.</param>
        /// <param name="value">Value.</param>
        private int CheckSurroundings(Dictionary<int, List<Platform>> qTodo, Queue<Tuple<int, Platform>> qProcessed, IGPos to, int value)
        {
            //add new layer
            qTodo.Add(value+1, new List<Platform>());

            // Dejme tomu, že se potřebujeme dostat z bodu S do bodu F. Do fronty tedy zapíšeme bod S a dáme mu hodnotu 0.
            // Fronta : [4;7]
            List<Platform> lp = qTodo[value];
            foreach (var platform in lp)
            {
                if (platform == null)
                    Console.WriteLine("problem");
                // mark as processed
                qProcessed.Enqueue(new Tuple<int, Platform>(value, platform));

                if (platform != null)
                {
                    platform.pathfindingValues[id] = value;

                    // Vyjmeme 1. bod ve frontě (tedy [4;7]) a pokud je v jednom ze 4 směrů volno, 
                    // uděláme tam hodnotu o jednu větší, než je bod sám (bod S je nula, takže tam napíšeme jedničku). 
                    // Já jsem si zvolil, že první udělám tu nahoře, pak nalevo, dole a poslední tu vpravo. 
                    // Políčka, na které jsme položili jedničky, si zapíšeme do fronty.
                    // Fronta : [4;6][3;7][4;­8][5;7]

                    Platform pUp = CanMove(Direction.UP, platform);
                    Platform pDown = CanMove(Direction.DOWN, platform);
                    Platform pRight = CanMove(Direction.RIGHT, platform);
                    Platform pLeft = CanMove(Direction.LEFT, platform);

                    Platform pSelected = null;
                    if ((pUp != null) && (pUp.GetPathFindingValue(id) == -1))
                    {
                        pUp.pathfindingValues[id] = value;
                        qTodo[value + 1].Add(pUp);
                        pSelected = pUp;
                        if (pSelected.x == to.X &&
                            pSelected.y == to.Y &&
                            pSelected.layer == to.Layer)
                        {
                            return value;
                        }
                    }
                    if ((pDown != null) && (pDown.GetPathFindingValue(id) == -1))
                    {
                        pDown.pathfindingValues[id] = value;
                        qTodo[value + 1].Add(pDown);
                        pSelected = pDown;
                        if (pSelected.x == to.X &&
                            pSelected.y == to.Y &&
                            pSelected.layer == to.Layer)
                        {
                            return value;
                        }
                    }
                    if ((pRight != null) && (pRight.GetPathFindingValue(id) == -1))
                    {
                        pRight.pathfindingValues[id] = value;

                        qTodo[value + 1].Add(pRight);

                        pSelected = pRight;
                        if (pSelected.x == to.X &&
                            pSelected.y == to.Y &&
                            pSelected.layer == to.Layer)
                        {
                            return value;
                        }
                    }
                    if ((pLeft != null) && (pLeft.GetPathFindingValue(id) == -1))
                    {
                        pLeft.pathfindingValues[id] = value;

                        qTodo[value + 1].Add(pLeft);

                        pSelected = pLeft;
                        if (pSelected.x == to.X &&
                            pSelected.y == to.Y &&
                            pSelected.layer == to.Layer)
                        {
                            return value;
                        }
                    }
                }
            }

            // for safety
            if (value >= MAX)
                return value;

            if (qTodo.Count != 0)
                return CheckSurroundings(qTodo, qProcessed, to, ++value);



            // Opět vyjmeme 1. bod ve frontě ([4;6]) a systémem nahoru, doleva, dolů, doprava napíšeme do políček, 
            // které jsou prázdné, číslo, které je zase o jednu větší, než to z fronty (takže 2). Body opět přidám do fronty.
            // Fronta : [3;7][4;8][5;­7][5;6]

            // Toto se stále opakuje...



            return value;
        }

        #endregion
    }

}
