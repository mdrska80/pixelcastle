using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Castles
{
    public interface IAction
    {
		// what action should be executed....
		string Name {get;set;}

		// parameters of the action
		string Param1 {get;set;}
		string Param2 {get;set;}
		string Param3 {get;set;}
		string Param4 {get;set;}
		string Param5 {get;set;}
		string Param6 {get;set;}
		string Param7 {get;set;}
		string Param8 {get;set;}
		string Param9 {get;set;}
		string Param10 {get;set;}

		bool IsOneTimeAction {get;set;}
		bool IsActive {get;set;}

        void Execute(Tile p, Entity e);
    }
}
