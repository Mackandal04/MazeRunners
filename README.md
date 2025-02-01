# Maze Runners
    Maze Runners Este proyecto es un juego de laberinto llamado "Maze Runners" desarrollado en C# 
    utilizando la biblioteca Spectre.Console para la interfaz de consola. El juego cuenta con dos jugadores 
    que deben navegar a través de un laberinto generado aleatoriamente.El objetivo es que cada jugador encuentre la salida 
    del laberinto antes que el otro.
    Puede haber obstáculos y caminos bloqueados que los jugadores deben sortear
    El juego termina cuando uno de los jugadores alcanza la salida del laberinto.
    
    ## 🚀Caracteristicas
    -Proyecto hecho en Consola con Spectre.Console
    -Dos players
    -Maze generado aleatoriamente

    ## 🎮Objetos del maze
        #Fichas
        - ⚡️ Jarvis es un NormalToken
        - ☬░ Ultro es un TeleportToken
        -⇶░ Batman es un TrapDeleteToken
        - ⇯░Quasimodo es un ObstacleToken
        -⧖░ Francesco es un FlashToken
        - KK Truck es un WallDestroyerToken

        #Traps
        - ██ en azul representa una TeleportTrap
        -██ en rojo representa una DamageTrap
        -██ en verde representa una HealthTrap
        -██ en amarillo representa una InvalidateTokenSkillTrap
        -██ en morado representa una StuckTrap

        #Cells
        - ██ en blanco representa la ExitCell
        -██ en negro representa un obstaculo
        -██ en plateado representa un muro
        -░░ en cyan representan los caminos o casillas libres