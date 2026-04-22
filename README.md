# Qs Impact Game

![Unity Version](https://img.shields.io/badge/Unity-2022.3%2B-black?style=flat-square&logo=unity)
 _ 
![C#](https://img.shields.io/badge/Language-C%23-blue?style=flat-square&logo=c-sharp)
 _ 
![Visual Scripting](https://img.shields.io/badge/Architecture-Hybrid_Scripting-orange?style=flat-square)

Un frenético juego de plataformas y disparos en 2D (Run and Gun) donde el jugador debe pasar obstáculos, gestionar su salud y eliminar enemigos a través de niveles dinámicos.

## ✨ Características Principales

* **Plataformas y Combate Fluido:** Sistema de movimiento responsivo y mecánicas de disparo integradas para una experiencia arcade clásica.
* **Arquitectura Híbrida (Hybrid Scripting):** * Core del jugador (`PlayerPlatformerController`) y sistemas base desarrollados en **C# tradicional** para máximo rendimiento y control físico.
  * Inteligencia artificial de enemigos (`EnemyController`) e interacciones de colisión desarrolladas utilizando **Unity Visual Scripting (Script Graphs)** para iteración rápida y diseño modular.
* **Sistema de Daño Independiente:** Detección de colisiones precisa mediante físicas 2D (Rigidbodies y Triggers), con soporte para *knockback* y frames de invulnerabilidad.
* **Animaciones Reactivas:** Integración directa con el componente `Animator` para transiciones de estado orgánicas (Idle, Run, Shoot, Die).

## 🛠️ Tecnologías y Herramientas

* **Motor:** Unity 3D (Configurado para entorno 2D).
* **Físicas:** Unity Physics 2D (Rigidbody2D, BoxCollider2D, Object Pooling para proyectiles).
* **Lógica:** C# MonoBehaviours & Unity Visual Scripting.

## 📁 Estructura del Proyecto Híbrido

Este proyecto sirve como demostración técnica de cómo hacer convivir código escrito y nodos visuales:

* `Assets/Scripts/`: Contiene los scripts tradicionales orientados a sistemas críticos y control del jugador, y los *Script Graphs* utilizados para el comportamiento y ciclos de vida de los enemigos.
