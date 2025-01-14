using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace MazeRunners
{
    
    public abstract class TrapCell : Cell
    {
        public override void Show()
        {
            AnsiConsole.Markup("##");
            //AnsiConsole.Markup("🪤");
        }

        public abstract void ActivateTrapSkill(Tokens token,Maze maze);
    }

    public class TeleportTrap : TrapCell
    {
        public override void ActivateTrapSkill(Tokens token,Maze maze)
        {
            TeleportToBeginning trapTeleport = new TeleportToBeginning();

            trapTeleport.ActivateSkill(token,maze);
        }
    }

    public class DamageTrap : TrapCell
    {
        public override void ActivateTrapSkill(Tokens token,Maze maze)
        {
            TakeDamage takeDamage = new TakeDamage();

            takeDamage.ActivateSkill(token,maze);
        }
    }

    public class InvalidateTokenSkillTrap : TrapCell
    {
        public override void ActivateTrapSkill(Tokens token, Maze maze)
        {
            EliminateTokenSkill eliminateTokenSkill = new EliminateTokenSkill();

            eliminateTokenSkill.ActivateSkill(token,maze);
        }
    }

    public class StuckTrap : TrapCell
    {
        public override void ActivateTrapSkill(Tokens token, Maze maze)
        {
            StuckToken stuckToken = new StuckToken();

            stuckToken.ActivateSkill(token,maze);
        }
    }

    public class HealthTrap : TrapCell
    {
        public override void ActivateTrapSkill(Tokens token, Maze maze)
        {
            HealingToken healingToken = new HealingToken();

            healingToken.ActivateSkill(token,maze);
        }
    }
}