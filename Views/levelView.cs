using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Castles.Conf;
using Castles.gameObjects;
using Castles.gameObjects.items;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Sprites;

using Castles.Tools;

namespace Castles.Views
{
    public class levelView : BaseView
    {
        public const int PLATFORM_HEIGHT = 30;

        public Surface screen { get; set; }
        public Random r { get; set; }
        //public Surface sPlatform  {get;set;}
        public Surface sPlatformSpecial { get; set; }

        public Surface sGem  {get;set;}
        public Surface sCauldron  {get;set;}
        public Surface sBarrel  {get;set;}
        public Surface sBox  {get;set;}
        public Surface sBucket { get; set; }
        public Surface sBooobakItem { get; set; }

        /// <summary>
        /// index is height
        /// </summary>
        public List<Surface> sColumns {get;set;}
        public List<Surface> sColumnsSpecial { get; set; }
        public List<Surface> sColumnsPits { get; set; }
        public List<Surface> sColumnsBlocks { get; set; }
        public List<Surface> sColumnsTeleports { get; set; }
        public List<Surface> sColumnsPressures { get; set; }

        public Theme t { get; set; }

        public levelView(Point origin) : base(origin)
        {
            r = new Random();
            //sPlatform = resourceManager.GetGfx(CastlesConfigurationReader.GetConfiguration().Editor.DefaultPlatform.Gfx);
            sPlatformSpecial = Game.I.resourceManager.GetGfx(CastlesConfigurationReader.GetConfiguration().Editor.DefaultPlatformSpecial.Gfx);
            sGem = Game.I.resourceManager.GetGfx("GemCrime.png");
            sCauldron = Game.I.resourceManager.GetGfx("Cauldron.png");
            sBarrel = Game.I.resourceManager.GetGfx("Barrel.png");
            sBox = Game.I.resourceManager.GetGfx("Box.png");
            sBucket = Game.I.resourceManager.GetGfx("Bucket.png");
            sBooobakItem = Game.I.resourceManager.GetGfx("BooobakItem.png");

            t = Game.I.themes[Game.I.level.theme];
        }


        public void PrepareColumns(Surface surf)
        {
            sColumns = new List<Surface>();
            sColumnsSpecial = new List<Surface>();
            sColumnsPits = new List<Surface>();
            sColumnsBlocks = new List<Surface>();
            sColumnsTeleports = new List<Surface>();
            sColumnsPressures = new List<Surface>();


            //get max layer used in this level
            int maxLayer = 30;// Game.I.level.GetMaxLayer() + 1;
            Level l = Game.I.level;
            Surface sTheme = Game.I.resourceManager.GetGfx(l.theme);

            if (sTheme == null)
                sTheme = Game.I.resourceManager.GetGfx(CastlesConfigurationReader.GetConfiguration().Editor.DefaultPlatform.Gfx);

//            Theme t = null;
  //          if  ((!string.IsNullOrEmpty(l.theme))  && (Game.I.themes.ContainsKey(l.theme)))
    //            t = Game.I.themes[l.theme];

            for(int i = 0;i<=maxLayer;i++)
            {
                Surface sx = surf.CreateCompatibleSurface(48, i * Common.halfheight + PLATFORM_HEIGHT, true);
                sx.Transparent = true;

                Surface sxSpecial = surf.CreateCompatibleSurface(48, i * Common.halfheight + PLATFORM_HEIGHT, true);
                sxSpecial.Transparent = true;

                Surface sxPit = surf.CreateCompatibleSurface(48, i * Common.halfheight + PLATFORM_HEIGHT, true);
                sxPit.Transparent = true;

                Surface sxBlock = surf.CreateCompatibleSurface(48, i * Common.halfheight+42, true);
                sxBlock.Transparent = true;

                Surface sxTeleport = surf.CreateCompatibleSurface(48, i * Common.halfheight + PLATFORM_HEIGHT, true);
                sxTeleport.Transparent = true;

                Surface sxPressure = surf.CreateCompatibleSurface(48, i * Common.halfheight + PLATFORM_HEIGHT, true);
                sxPressure.Transparent = true;                

                Surface sxDarken = surf.CreateCompatibleSurface(48, i * Common.halfheight + PLATFORM_HEIGHT, true);
                sxDarken.Transparent = true;

                for(int j = i; j>=0; j--)
                {
                    if (t != null)
                    {
                        sTheme = t.GetRandomPlatform();

                        sxPit.Blit(t.PitPlatform, new Point(0, j * Common.stepheight));
                        sxBlock.Blit(t.BlockPlatform, new Point(0, j * Common.stepheight));
                        sxTeleport.Blit(t.TeleportPlatform, new Point(0, j * Common.stepheight));
                        sxPressure.Blit(t.PressurePlatePlatform, new Point(0, j * Common.stepheight));

                    }

                    sx.Blit(sTheme, new Point(0, j*Common.stepheight));

                    if (j == 0)
                        sxSpecial.Blit(sPlatformSpecial, new Point(0, j * Common.stepheight));
                    else
                        sxSpecial.Blit(sTheme, new Point(0, j * Common.stepheight));

                }

                sColumns.Add(sx);
                sColumnsSpecial.Add(sxSpecial);
                sColumnsPits.Add(sxPit);
                sColumnsBlocks.Add(sxBlock);
                sColumnsTeleports.Add(sxTeleport);
                sColumnsPressures.Add(sxPressure);

            }
        }



        public override void UpdateView(Surface surf)
        {
            if (CastlesConfigurationReader.GetConfiguration().PermaLevelReloading)
                if (Game.I.level != null)
                    Game.I.level = Game.I.level.Reload();
                else
                    Game.I.level = Level.Load(1);

            if (sColumns==null)
            {
                PrepareColumns(surf);
            }

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
