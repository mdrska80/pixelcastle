using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Castles
{
	public partial class Action
	{
		// what action should be executed....
		public string Name {get;set;}

		// parameters of the action
		public string Param1 {get;set;}
		public string Param2 {get;set;}
		public string Param3 {get;set;}
		public string Param4 {get;set;}
		public string Param5 {get;set;}
		public string Param6 {get;set;}
		public string Param7 {get;set;}
		public string Param8 {get;set;}
		public string Param9 {get;set;}
		public string Param10 {get;set;}

		public bool IsOneTimeAction {get;set;}
		public bool IsActive {get;set;}

		public virtual void Execute(Platform p, Entity e)
		{
			if (Name=="teleport")
				Execute_teleport(p,e);

			if (Name=="addLife")
				Execute_addLife(p,e);

			if (Name=="destroyPlatform")
				Execute_destroyPlatform(p,e);

			if (Name=="createPlatform")
				Execute_createPlatform(p,e);


		}
	}
}
