using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using SFML.Graphics;
using SFML.Window;

using Castles.Tools;
using Castles.Conf;
using Castles.gameObjects;
using Castles.gameObjects.items;

namespace Castles.Views
{
    public class levelView : BaseView
    {
        public const int PLATFORM_HEIGHT = 30;

        public Surface screen { get; set; }
        public Random r { get; set; }
        //public Surface sPlatform  {get;set;}
        public Surface sPlatformSpecial { get; set; }

        public levelView(Vector2i origin) : base(origin)
        {
            r = new Random();
        }
        
        public override void UpdateView(RenderWindow window)
        {
            Level level = Game.I.level;
            List<Monster> monsters = Game.I.level.Monsters;

            if ((level != null) && (level.Platforms != null))
            {
                foreach (var platform in level.Platforms)
                {
                    // layer shift for elevator.
                    int shift = platform.elevator != null ? platform.elevator.Current : 0;

                    Point p = Common.GetPoint(platform.x, platform.y, platform.layer+shift, origin);

                    Game.I.surfaces++;

                    if (platform.type==PlatformType.Column)
                    {
                        List<Surface> actiSurfaces = sColumns;

                        if (platform.isHighLighted)
                            actiSurfaces = sColumnsSpecial;

                        if (platform.isPit)
                            actiSurfaces = sColumnsPits;

                        if (platform.playerCannotPass)
                        {
                            //Theme t = Game.I.themes[Game.I.level.theme];
                            p.Y -= t.BlockPlatform.Height-PLATFORM_HEIGHT;
                            actiSurfaces = sColumnsBlocks;
                        }

                        if (platform.action!=null)
                        {
                            // TODO
                            // we can have action there anyway, but with this marker we know
                            // we have to show to player
                            actiSurfaces = sColumnsPressures;
                        }

                        // do actually draw it
                        if (platform.ColumnHeight != 0)
                                surf.Blit(actiSurfaces[platform.ColumnHeight + shift - 1], p);
                        else
                            surf.Blit(actiSurfaces[platform.layer + shift], p);
                    }

                    if (platform.type == PlatformType.Custom)
                    {
                        Surface sx = Game.I.resourceManager.GetGfx(platform.gfx ?? "platformError.png"); ;
                        surf.Blit(sx, new Point(p.X + platform.ShiftX, p.Y - sx.Height + platform.ShiftY));
                    }

                    //is there a gem on the platform?
                    if (platform.item is Gem)
                    {
                        Gem g = platform.item as Gem;
                        //draw gem
                        if (!g.picked)
                        {
                            Game.I.surfaces++;
                            surf.Blit(sGem, new Point(p.X + 15, p.Y - 7));
                        }
                    }

                    if (platform.item is HoneyCauldron)
                    {
                        //draw cauldron
                        Game.I.surfaces++;
                        surf.Blit(sCauldron, new Point(p.X + 5, p.Y - 23));

                    }

                    if (platform.item is Barrel)
                    {
                        //draw barrel
                        Game.I.surfaces++;
                        surf.Blit(sBarrel, new Point(p.X + 5, p.Y - 23));
                    }

                    if (platform.item is Box)
                    {
                        //draw box
                        Game.I.surfaces++;
                        surf.Blit(sBox, new Point(p.X + 5, p.Y - 23));
                    }

                    if (platform.item is Bucket)
                    {
                        //draw bucket
                        // what is the point of bucket?
                        Game.I.surfaces++;
                        surf.Blit(sBucket, new Point(p.X + 13, p.Y - sBucket.Height + 14));
                    }

                    if (platform.item is Booobak_item)
                    {
                        //draw bucket
                        // what is the point of bucket?
                        Game.I.surfaces++;
                        surf.Blit(sBooobakItem, new Point(p.X + 7, p.Y - sBucket.Height+1));
                    }
                    
                    //is there a monster on this pos?
                    var mx = (from m in monsters
                        where m.position.X == platform.x &&
                        m.position.Y == platform.y &&
                        m.position.Layer == platform.layer select m).FirstOrDefault();

                    if (mx!=null)
                    {
                        if (mx.isAlive)
                        {
                            Game.I.surfaces++;
                            surf.Blit(mx.sprite, new Point(p.X + mx.ShiftX, p.Y - mx.sprite.Height+mx.ShiftY));
                        }                                
                    }

                    //elevator shift
                    int shft = 0;
                    if (platform.elevator != null)
                        shft = platform.elevator.Current;

                    

                    //draw player
                    if (Game.I.player!=null &&
                        Game.I.player.position.X == platform.x &&
                        Game.I.player.position.Y == platform.y &&
                        Game.I.player.position.Layer == platform.layer+shft
                        )
                    {
                        if (Game.I.player.isAlive)
                        {
                            Game.I.surfaces++;
                            surf.Blit(Game.I.player.sprite, new Point(p.X, p.Y - Game.I.player.sprite.Height + 18));
                        }
                    }
                }
            }
        }
    }
}
