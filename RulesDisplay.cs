using System;
using Spectre.Console;

namespace MazeRunners
{
    public class RulesDisplay
    {
        // Método para mostrar las reglas del juego
        public void ShowRules()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var title = "[bold cyan]Reglas del Juego[/]";

            var rules = new[]
            {
                "[bold yellow]Maze Runners[/]",
                "Este proyecto es un juego de laberinto desarrollado en [blue]C#[/], \n" + "utilizando la biblioteca de [green]Spectre.Console[/] para la interfaz gráfica en consola.\n",

                "[bold yellow]🎯 Objetivo del Juego[/]",
                "Dos jugadores navegan por un laberinto generado aleatoriamente.",
                "El primero en llevar todas sus fichas a la salida gana la partida.",
                "Habrá obstáculos, trampas y caminos bloqueados que los jugadores deben sortear.\n",

                "[bold yellow]🛠 Características del Juego[/]",
                "- Interfaz en consola con [green]Spectre.Console[/]",
                "- Dos jugadores",
                "- La partida termina cuando un jugador lleva todas sus fichas a la salida.",
                "- Laberinto generado aleatoriamente\n",

                "[bold yellow]🎮 Leyenda del Laberinto[/]\n",

                "[bold green]🔹 Fichas:[/]",
                "[cyan]- ⚡️[/] Jarvis (NormalToken) → Icono más fachero del juego.",
                "[cyan]- ☬░[/] Ultron (TeleportToken) → Puede teletransportarse a un lugar cercano.",
                "[cyan]- ⇶░[/] Batman (TrapDeleteToken) → Inutiliza trampas en su camino.",
                "[cyan]- ⇯░[/] Quasimodo (ObstacleToken) → Coloca obstáculos en el camino.",
                "[red]- ⧖░[/] Francesco (FlashToken) → Se mueve grandes distancias rápidamente.",
                "[red]- KK[/] Optimus Prime (WallDestroyerToken) → Puede destruir muros.\n",

                "[bold red]⚠️ Trampas:[/]",
                "[blue]- ██[/] TeleportTrap → Envía al token a un punto de inicio del laberinto.",
                "[red]- ██[/] DamageTrap → Resta 3 puntos de vida.",
                "[green]- ██[/] HealthTrap → Restaura puntos de vida.",
                "[yellow]- ██[/] InvalidateTokenSkillTrap → Inutiliza la habilidad del token.",
                "[purple]- ██[/] StuckTrap → Deja al token atrapado por varios turnos.\n",

                "[bold silver]🛤️ Tipos de Celdas:[/]",
                "- [bold silver]██[/] ExitCell → Representa la meta del laberinto.Solo se encuentra en el medio de este",
                "- [bold black]██[/] Obstáculo → Bloquea el paso.",
                "- [bold silver]██[/] Muro → Forma el laberinto.",
                "- [bold cyan]░░[/] Caminos → Espacios transitables.\n",
            };

            var panel = new Panel(string.Join("\n", rules))
                .Header(title)
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.Cyan1);

            AnsiConsole.Write(panel);
        }
    }
}