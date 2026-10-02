# Pong Aula

Juego Pong en 2D hecho con Unity como proyecto de aula. Dos jugadores se enfrentan en un campo de fútbol: el primero en llegar a 9 puntos gana.

## Integrantes

- Jose Escarzo
- Kathlen Escalante

## Versión de Unity

Unity **6000.3.16f1** (Unity 6.3 LTS)

## Cómo se juega

| Jugador | Paleta | Subir | Bajar |
|---|---|---|---|
| Jugador 1 | Azul | W | S |
| Jugador 2 | Roja | Flecha arriba | Flecha abajo |

- La pelota rebota en las paletas y en las paredes de arriba y abajo.
- Si la pelota toca una de las barreras laterales, se suma un punto al rival y la pelota vuelve al centro.
- Gana quien llegue primero a 9 puntos. Aparece el cartel del ganador y al presionar "R" se vuelve a iniciar.

## Escenas

1. `Menu`: título y botones Jugar y Salir.
2. `Juego`: partida de Pong con marcador.

## Organización del trabajo (ramas)

| Rama | Contenido |
|---|---|
| `main` | Versión estable y entregable |
| `feature/menu` | Escena de menú |
| `feature/bola` | Bola, rebotes, gol, marcador y victoria |
| `feature/sonidos` | Efectos de sonido |

Cada rama se fusionó a `main` mediante Pull Request.

## Créditos y referencias

- Los sprites, sonidos y scripts base se tomaron como referencia de un proyecto Pong publicado en [itch.io](https://itch.io), adaptados a Unity 6 por el equipo.

## Descarga
