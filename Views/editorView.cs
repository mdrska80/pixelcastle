using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SdlDotNet.Core;
using SdlDotNet.Graphics;
using SdlDotNet.Input;

namespace Castles
{
    public class editorView : BaseView
    {
        private SdlDotNet.Graphics.Font editorfont;
        private SdlDotNet.Graphics.Font editorfontSmall;

        //surfaces required for this View
        private Surface m_FontSurface;

        public Surface sPlatform { get; set; }
        public Surface sPlatformAlpha { get; set; }
        public Surface sPlatformGreen { get; set; }
        public Surface sPlatformOutline { get; set; }


        public Surface sGem  {get;set;}

        private static Point m_CursorPosition = new Point();

        private Board b1 = new Board();
        private Board b2 = new Board();


        public editorView(Point origin) : base(origin)
        {
            editorfont = new SdlDotNet.Graphics.Font(@"Arial.ttf", 16);
            editorfontSmall = new SdlDotNet.Graphics.Font(@"Arial.ttf", 12);
            Events.MouseMotion += new EventHandler<MouseMotionEventArgs>(ApplicationMouseMotionEventHandler);

            sPlatform = Game.I.resourceManager.Cache_gfx["platform.png"];
            sPlatformAlpha = Game.I.resourceManager.Cache_gfx["platformAlpha.png"];
            sPlatformGreen = Game.I.resourceManager.Cache_gfx["platformGreen.png"];
            sPlatformOutline = Game.I.resourceManager.Cache_gfx["platformOutline.png"];


            sGem = Game.I.resourceManager.Cache_gfx["Gem.png"];
        }

        public override void UpdateView(Surface surf)
        {
            int w = Video.Screen.Width-300;

            Common.Text(OP(w + 10, 10), string.Format("Cursor pos: {0}:{1}", m_CursorPosition.X, m_CursorPosition.Y), editorfont, surf, Color.White);
            Common.Text(OP(w + 10, 30), string.Format("Board origin: {0}:{1}", Game.I.boardOrigin.X, Game.I.boardOrigin.Y), editorfont, surf, Color.White);
            Common.Text(OP(w + 10, 50), string.Format("Layer: {0}", Game.I.editingPlatform.Layer), editorfont, surf, Color.White);
            Common.Text(OP(w + 10, 70), string.Format("Platform coordinates: {0}:{1}", Game.I.editingPlatform.X, Game.I.editingPlatform.Y), editorfont, surf, Color.White);

            Common.Text(OP(w + 10, 90), string.Format("Editing object: {0}", Game.I.editingObject), editorfont, surf, Color.White);
            if (Game.I.player!=null)
                Common.Text(OP(w + 10, 110), string.Format("Gems {0} / {1}", Game.I.player.pickedGems, Game.I.level.activeGems), editorfont, surf, Color.White);
            else
                Common.Text(OP(w + 10, 110), string.Format("Gems Unk / Unk"), editorfont, surf, Color.White);

            Point pppp = GetMouseXYFromPlatformXYFromMouseXY((Game.I.editingPlatform.Layer));
            Common.Text(OP(w + 10, 130), string.Format("Recalculated position: {0}:{1} -> {2}:{3}", pppp.X, pppp.Y,
                Common.GetPlatformXYFromMouseXY(Game.I.editingPlatform.Layer, ref m_CursorPosition).X,
                Common.GetPlatformXYFromMouseXY(Game.I.editingPlatform.Layer, ref m_CursorPosition).Y), editorfont, surf, Color.White);

            Common.Text(OP(w + 10, 150), string.Format("Platforms: {0}", Game.I.level.Platforms.Count), editorfont, surf, Color.White);



            int max = Game.I.editingPlatform.Layer;

            Point px = Common.GetPlatformXYFromMouseXY(Game.I.editingPlatform.Layer, ref m_CursorPosition);

            for (int i = 0; i <= max; i++)
                DrawEditingTool(surf, i, px);

            if (Game.I.editingObject == EditorObjects.gems)
            {
                Point ppp = Common.GetPoint(px.X, px.Y, Game.I.editingPlatform.Layer, Game.I.boardOrigin);
                surf.Blit(sGem, new Point(ppp.X+15, ppp.Y-4));
            }
        }

        private void DrawEditingTool(Surface surf, int editingLayer, Point coord)
        {
            Surface s = sPlatformAlpha;
            if (editingLayer == Game.I.editingPlatform.Layer) s = sPlatform;
            
            if (editingLayer == 0)
            {
                //up
                IGPos igUp = Common.GetDesiredPosition(Direction.up, new IGPos(coord.X, coord.Y, 0));
                Point locUp = Common.GetPoint(igUp.X, igUp.Y, editingLayer, Game.I.boardOrigin);
                surf.Blit(sPlatformAlpha, new Point(locUp.X, locUp.Y));

                // left
                IGPos igLeft = Common.GetDesiredPosition(Direction.left, new IGPos(coord.X, coord.Y, 0));
                Point locLeft = Common.GetPoint(igLeft.X, igLeft.Y, editingLayer, Game.I.boardOrigin);
                surf.Blit(sPlatformAlpha, new Point(locLeft.X, locLeft.Y));
            }

            Point loc = Common.GetPoint(coord.X, coord.Y, editingLayer, Game.I.boardOrigin);
            surf.Blit(s, new Point(loc.X, loc.Y));

            if (editingLayer == 0)
            {
                IGPos i = Common.GetDesiredPosition(Direction.up, new IGPos(coord.X, coord.Y, 0));
                Point l = Common.GetPoint(coord.X, coord.Y, 0, Game.I.boardOrigin);
                surf.Blit(sPlatformGreen, new Point(l.X, l.Y));

                //Point lx = Common.GetPoint(coord.X, coord.Y, 0, Game.I.boardOrigin);
                //surf.Blit(sPlatformOutline, new Point(lx.X-8, lx.Y-8));

                // right
                IGPos igRight = Common.GetDesiredPosition(Direction.right, new IGPos(coord.X, coord.Y, 0));
                Point locRight = Common.GetPoint(igRight.X, igRight.Y, editingLayer, Game.I.boardOrigin);
                surf.Blit(sPlatformAlpha, new Point(locRight.X, locRight.Y));

                //down
                IGPos igDown = Common.GetDesiredPosition(Direction.down, new IGPos(coord.X, coord.Y, 0));
                Point locDown = Common.GetPoint(igDown.X, igDown.Y, editingLayer, Game.I.boardOrigin);
                surf.Blit(sPlatformAlpha, new Point(locDown.X, locDown.Y));
            }








            // je-li to na stejnem layeru tak pak to nakresli non alpha
            if (editingLayer == Game.I.editingPlatform.Layer)
            {
                Common.Text(loc.X+75, loc.Y+5, string.Format("Layer: {0}", editingLayer), editorfontSmall,surf, Color.White);
            }


        }

        private static void ApplicationMouseMotionEventHandler(object sender, MouseMotionEventArgs args)
        {
            m_CursorPosition = args.Position;
        }



        public Point GetMouseXYFromPlatformXYFromMouseXY(int editingLayer)
        {
            Point coord = Common.GetPlatformXYFromMouseXY(editingLayer, ref m_CursorPosition);
            //Point p = new Point(coord.X, coord.Y);

            Point loc = Common.GetPoint(coord.X, coord.Y, editingLayer, origin);
            //OP(p.X * Common.halfwidth, p.X * Common.halfheight + p.Y * Common.stepheight - editingLayer * Common.halfheight);

            return new Point(loc.X + Game.I.boardOrigin.X, loc.Y + Game.I.boardOrigin.Y);
        }
    }
}
